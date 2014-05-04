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
        //Enseña un patron a la red, el cual sera generado dependiendo el metodo elegido
        public void TeachKanji(Image sourceImage, string name, string description)
        {
            this.TeachKanji(new Kanji(sourceImage, name, description));
        }
        public void TeachKanji(Kanji kanji)
        {
            //Dependiendo del modo obj de aprendizaje genera el patron
            string accessHash = string.Empty;
            List<Neuron> pattern = null;
            switch (learningMethod)
            {
                case LearningMethod.Normal:
                    generatePattern_Normal(kanji.sourceImage, out pattern, out accessHash);
                    break;

                case LearningMethod.Hashing:
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

        //Genera el patrón a través del metodo habitual
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

        //Genera el patrón usando un hash generado a partir del contenido de la imagen
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

        //Intenta reconocer la imagen dada a traves de algunos de los kanjis aprendidos
        public Kanji RecognizeKanji(Image sourceImage, out Bitmap resultBitmap)
        {
            //Dependiendo del modo obj de aprendizaje
            string accessHash = string.Empty;
            List<Neuron> initialState = null;
            switch (learningMethod)
            {
                case LearningMethod.Normal:
                    generatePattern_Normal(sourceImage, out initialState, out accessHash);
                    break;

                case LearningMethod.Hashing:
                    generatePattern_Hashing(sourceImage, out initialState, out accessHash);
                    break;
            }  
            
            //Diagnostica el patron
            NeuralNetwork.Run(initialState, false);

            //Busca si el resultado es un patron aprendido un minimo de energia local no esperado
            //Para esto extrae el bitmap del patron resultante y lo busca en los aprendidos
            resultBitmap = bitmapFromPattern(NeuralNetwork.Neurons);
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

        //Crea la red con la cantidad de neuronas especificadas
        public void CreateNN(int neurons, EnergyChangedHandler energyHandle = null, LearningMethod method = LearningMethod.Normal)
        {
            //Crea la red
            NeuralNetwork = new NeuralNetwork(neurons);
            if (energyHandle != null)
                NeuralNetwork.EnergyChanged += new EnergyChangedHandler(energyHandle);

            //Instancia las variables
            this.learnedKanjis = new Dictionary<string, Kanji>();
            this.learningMethod = method;
        }

        //Crea el patron segun la cantidad de neuronas de la red y usando el valor de activacion calculado
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

        //Crea el patron a partir de un bithash. La cantidad de bits debe coincidir con la cantidad de neuronas.
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

        //Crea un bitmap a partir de un patron. Esta sera una imagen en blanco y negro
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

        //Devuelve la raiz cuadrada de la cantidad de neuronas. Este valor sera usado para saber 
        //el ancho y alto de las imagenes cuadradas que seran usdadas para la extraccion de patrones
        private int sqrtNeuronsCount 
        {
            get { return (int) Math.Sqrt(NeuralNetwork.NeuronsCount); }
        }

        //Modos de presentacion de patrones para el aprendizaje de patrones
        public enum LearningMethod
        {
            Normal = 0,
            Hashing = 1
        }

        //Variables        
        private Dictionary<string, Kanji> learnedKanjis;
        public NeuralNetwork NeuralNetwork { get; private set; }
        public LearningMethod learningMethod { get; private set; }
    }
}
