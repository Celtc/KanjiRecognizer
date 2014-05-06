using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows;

using HopfieldNeuralNetwork;
using System.Collections;

namespace KanjiRecognizer.Source
{
    public class NeuralNetworkAPI
    {
        /// <summary>
        /// Enseña un patrón a la red, el cual será generado dependiendo el método elegido.
        /// </summary>
        /// <param name="sourceImage">Imagen del kanji a aprender</param>
        /// <param name="name">Nombre del kanji</param>  
        /// <param name="description">Descripción del kanji</param>  
        public void TeachKanji(Image sourceImage, string name, string description)
        {
            this.TeachKanji(new Kanji(sourceImage, name, description));
        }

        /// <summary>
        /// Enseña un patrón a la red, el cual será generado dependiendo el método elegido.
        /// </summary>
        /// <param name="kanji">Kanji a aprender</param>
        public void TeachKanji(Kanji kanji)
        {
            //Dependiendo del modo obj de aprendizaje genera el patron
            string accessHash = string.Empty;
            List<Neuron> pattern = null;
            switch (generationMethod)
            {
                case GenerationMethod.Normal:
                    generatePattern_Normal(kanji.sourceImage, out pattern, out accessHash);
                    break;

                case GenerationMethod.Hashing:
                    generatePattern_Hashing(kanji.sourceImage, out pattern, out accessHash);
                    break;
            }          

            //Revisa que no exisitiera el patron ya en la base de datos
            if (!learnedKanjis.ContainsKey(accessHash))
            {
                //Guarda el nuevo kanji
                learnedKanjis.Add(accessHash, kanji);

                //Enseña el patron
                NeuralNetwork.AddPattern(pattern);
            }   
        }

        /// <summary>
        /// Genera el patrón a través del metodo habitual.
        /// </summary>
        /// <param name="sourceImage">Imagen a partir de la cual se generara el patrón</param>
        /// <param name="pattern">Patrón resultante</param>
        /// <param name="accesHash">Hash que identifica unívocamente este patrón</param>
        private void generatePattern_Normal(Image sourceImage, out List<Neuron> pattern, out string accessHash)
        {
            //Establece el valor de activación de las neuronas
            int activationValue = Math.Abs((int)(Color.Black.ToArgb() / 2));

            //Convierte la imagen a bitmap
            Bitmap sourceBitmap = ImageAPI.AlltoBMP(sourceImage);

            //Extrae el patron y su bitmap
            Bitmap processedBitmap;
            pattern = patternFromBitmap(sourceBitmap, activationValue, out processedBitmap);

            //Genera el hash a partir del bitmap de salida
            accessHash = ImageAPI.GenerateSHA1HashFromImage(processedBitmap);

            return;
        }

        /// <summary>
        /// Genera el patrón a través del metodo de hashing, aumentando la ortoganalidad de los patrones generados
        /// pero limitando el reconocimiento a patrones idénticos.
        /// </summary>
        /// <param name="sourceImage">Imagen a partir de la cual se generara el patrón</param>
        /// <param name="pattern">Patrón resultante</param>
        /// <param name="accesHash">Hash que identifica unívocamente este patrón</param>
        private void generatePattern_Hashing(Image sourceImage, out List<Neuron> pattern, out string accessHash)
        {
            //Extrae un hash de tantos bits como neuronas a partir de la imagen
            BitArray bitHash = ImageAPI.GenerateBitHashFromImage(sourceImage, NeuralNetwork.NeuronsCount);

            //Extrae el patron y su bitmap
            Bitmap processedBitmap;
            pattern = patternFromBithash(bitHash, out processedBitmap);

            //Genera el hash a partir del bitmap de salida
            accessHash = ImageAPI.GenerateSHA1HashFromImage(processedBitmap);

            return;
        }

        /// <summary>
        /// Intenta reconocer la imagen dada comparandola contra algunos de los kanjis aprendidos.
        /// En caso de reconocer la imagen, devuelve el kanji correspondiente, sino devuelve null.
        /// </summary>
        /// <param name="sourceImage">Imagen a reconocer</param>
        /// <param name="resultBitmap">Imagen devuelta por la red luego del analisis</param>
        public Kanji RecognizeKanji(Image sourceImage, int iterations, out Bitmap resultBitmap)
        {
            //Dependiendo del modo obj de aprendizaje
            string accessHash = string.Empty;
            List<Neuron> initialState = null;
            switch (generationMethod)
            {
                case GenerationMethod.Normal:
                    generatePattern_Normal(sourceImage, out initialState, out accessHash);
                    break;

                case GenerationMethod.Hashing:
                    generatePattern_Hashing(sourceImage, out initialState, out accessHash);
                    break;
            }  
            
            //Diagnostica el patron
            var returnedPattern = recognizePattern(initialState, iterations);

            //Busca si el resultado es un patron aprendido un minimo de energia local no esperado
            //Para esto extrae el bitmap del patron resultante y lo busca en los aprendidos
            resultBitmap = bitmapFromPattern(returnedPattern);
            Kanji recognizedKanji = null;
            try
            {
                string learnedHash = learnedKanjis.Keys.ElementAt(0);
                string resultingHash = ImageAPI.GenerateSHA1HashFromImage(resultBitmap);
                recognizedKanji = learnedKanjis[resultingHash];
            }
            catch { }

            return recognizedKanji;
        }

        //Diagnostica un patrón tantas veces como iteraciones se especifiquen
        private List<Neuron> recognizePattern(List<Neuron> inPattern, int iterations)
        {
            List<Neuron> outPattern = inPattern;

            for (int i = 0; i < iterations; i++)
            {
                NeuralNetwork.Run(outPattern, false);
                outPattern = NeuralNetwork.Neurons;
            }

            return outPattern;
        }

        /// <summary>
        /// Crea la red neuronal artificial con la cantidad de neuronas especificadas.
        /// </summary>
        /// <param name="neurons">Cantidad de neuronas con que sera creada la red</param>
        /// <param name="energyHandle">Handler para el evento de cambio de energia del estado de la red</param>
        /// <param name="method">Método que se utilizara para generar los patrones</param>
        public void CreateNN(int neurons, EnergyChangedHandler energyHandle = null, GenerationMethod method = GenerationMethod.Normal)
        {
            //Crea la red
            NeuralNetwork = new NeuralNetwork(neurons);
            if (energyHandle != null)
                NeuralNetwork.EnergyChanged += new EnergyChangedHandler(energyHandle);

            //Instancia las variables
            this.learnedKanjis = new Dictionary<string, Kanji>();
            this.generationMethod = method;
        }

        /// <summary>
        /// Extrae el patrón de una imagen, con la cantidad de componentes igual a la cantidad de neuronas en la red.
        /// </summary>
        /// <param name="sourceBitmap">Imagen a partir de la cual se extrae el patrón</param>
        /// <param name="activationValue">Valor utilizado para establer si un pixel es orientado o no en la matriz de pesos</param>
        /// <param name="outBitmap">Imagen resultante a partir de la cual se extrajo el patron</param>
        private List<Neuron> patternFromBitmap(Bitmap sourceBitmap, int activationValue, out Bitmap outBitmap)
        {
            //El patron esta formado por un conjunto de estados de las neuronas
            List<Neuron> pattern = new List<Neuron>(NeuralNetwork.NeuronsCount);

            //Escala la imagen para que tenga tanto pixeles como neuronas
            sourceBitmap = ImageAPI.ResizeBitmap(sourceBitmap, sqrtNeuronsCount, sqrtNeuronsCount);

            //Establece los estados de las neuronas para cada pixel
            for (int i = 0; i < sqrtNeuronsCount; i++)
            {
                for (int j = 0; j < sqrtNeuronsCount; j++) //La matriz es cuadrada
                {
                    Neuron neuron = new Neuron();
                    int pixelValue = Math.Abs(sourceBitmap.GetPixel(i, j).ToArgb());
                    if (pixelValue < activationValue)
                    {
                        sourceBitmap.SetPixel(i, j, Color.White);
                        neuron.State = NeuronStates.AlongField;
                    }
                    else
                    {
                        sourceBitmap.SetPixel(i, j, Color.Black);
                        neuron.State = NeuronStates.AgainstField;
                    }
                    pattern.Add(neuron);
                }
            }
            outBitmap = sourceBitmap;
            return pattern;
        }

        /// <summary>
        /// Convierte un bithash a un patrón. La cantidad de bits debe coincidir con la cantidad de neuronas.
        /// </summary>
        /// <param name="sourceBitmap">Bithash a partir de la cual se extrae el patrón</param>
        /// <param name="outBitmap">Imagen resultante a partir de la cual se extrajo el patron</param>
        private List<Neuron> patternFromBithash(BitArray bithash, out Bitmap outBitmap)
        {
            //Valida la cantidad de bits iniciales
            if (bithash.Count < NeuralNetwork.NeuronsCount)
            {
                outBitmap = null;
                return null;
            }

            //El patron esta formado por un conjunto de estados de las neuronas
            List<Neuron> pattern = new List<Neuron>(NeuralNetwork.NeuronsCount);

            //Crea el bitmap que se devolvera
            Bitmap processedBitmap = new Bitmap(sqrtNeuronsCount, sqrtNeuronsCount);

            //Establece los estados de las neuronas para cada bit
            for (int i = 0; i < NeuralNetwork.NeuronsCount; i++)
            {
                Neuron neuron = new Neuron();
                if (bithash.Get(i))
                {
                    processedBitmap.SetPixel(i / sqrtNeuronsCount, i % sqrtNeuronsCount, Color.Black);
                    neuron.State = NeuronStates.AgainstField;
                }
                else
                {
                    processedBitmap.SetPixel(i / sqrtNeuronsCount, i % sqrtNeuronsCount, Color.White);
                    neuron.State = NeuronStates.AlongField;
                }
                pattern.Add(neuron);
            }
            outBitmap = processedBitmap;
            return pattern;
        }

        /// <summary>
        /// Crea un bitmap a partir de un patrón. Esta será una imagen monocromo.
        /// </summary>
        /// <param name="pattern">Patrón utilizado para generar el bitmap</param>
        private Bitmap bitmapFromPattern(List<Neuron> pattern)
        {
            Bitmap resultBitmap = new Bitmap(sqrtNeuronsCount, sqrtNeuronsCount);
            for (int i = 0; i < resultBitmap.Width; i++)
            {
                for (int j = 0; j < resultBitmap.Height; j++) //La matriz es cuadrada
                {
                    int currIndex = sqrtNeuronsCount * i + j;
                    Neuron currNeuron = pattern[currIndex];
                    if (currNeuron.State == NeuronStates.AgainstField)
                    {
                        resultBitmap.SetPixel(i, j, Color.Black);
                    }
                    else
                    {
                        resultBitmap.SetPixel(i, j, Color.White);
                    }
                }
            }
            return resultBitmap;
        }
        
        /// <summary>
        /// Devuelve la raiz cuadrada de la cantidad de neuronas. Este valor será usado para saber
        /// el ancho y alto de las imagenes cuadradas utilizadas luego para la extraccion de patrones.
        /// </summary>
        private int sqrtNeuronsCount 
        {
            get { return (int) Math.Sqrt(NeuralNetwork.NeuronsCount); }
        }

        //Modos de generación de patrones para el aprendizaje de los mismos
        public enum GenerationMethod
        {
            Normal = 0,
            Hashing = 1
        }

        //Variables        
        private Dictionary<string, Kanji> learnedKanjis;
        public NeuralNetwork NeuralNetwork { get; private set; }
        public GenerationMethod generationMethod { get; private set; }
    }
}
