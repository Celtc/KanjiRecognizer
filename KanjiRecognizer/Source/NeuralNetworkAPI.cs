using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows;

using HopfieldNeuralNetwork;
using System.Collections;
using System.Drawing.Imaging;

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
                case GenerationMethod.Heightmap:
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
            //Convierte la imagen a bitmap
            Bitmap sourceBitmap = ImageAPI.AlltoBMP(sourceImage);

            //Extrae el patron y su bitmap
            Bitmap processedBitmap;
            pattern = patternFromBitmap(sourceBitmap, 0.8f, out processedBitmap);

            //Genera el hash a partir del bitmap de salida
            accessHash = ImageAPI.GenerateSHA1HashFromImage(processedBitmap);

            return;
        }

        /// <summary>
        /// Genera el patrón a través del metodo habitual, y luego crea el heightmap del patron generado.
        /// </summary>
        /// <param name="sourceImage">Imagen a partir de la cual se generara el patrón</param>
        /// <param name="pattern">Patrón resultante</param>
        /// <param name="heightmap">Heightmap del patrón generado</param>
        /// <param name="accesHash">Hash que identifica unívocamente este patrón</param>
        private void generatePattern_Heightmap(Image sourceImage, out List<Neuron> pattern, out List<double> heightmap, out string accessHash)
        {
            generatePattern_Normal(sourceImage, out pattern, out accessHash);

            heightmap = calculateHeightmap(bitmapFromPattern(pattern));

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
        /// <param name="iterations">Cantidad de intentos de reconocer la imagen</param>
        /// <param name="resultBitmap">Imagen devuelta por la red luego del analisis</param>
        public Kanji RecognizeKanji(Image sourceImage, out Bitmap resultBitmap)
        {
            //Dependiendo del modo obj de aprendizaje
            List<double> heightmap = null;
            List<Neuron> initialState = null;
            string accessHash = string.Empty;
            switch (generationMethod)
            {
                case GenerationMethod.Normal:
                    generatePattern_Normal(sourceImage, out initialState, out accessHash);
                    break;

                case GenerationMethod.Hashing:
                    generatePattern_Hashing(sourceImage, out initialState, out accessHash);
                    break;

                case GenerationMethod.Heightmap:
                    generatePattern_Heightmap(sourceImage, out initialState, out heightmap, out accessHash);
                    break;
            }  
            
            //Diagnostica el patron
            var returnedPattern = recognizePattern(initialState, heightmap);

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
        
        /// <summary>
        /// Diagnostica un patrón tantas veces como iteraciones se especifiquen.
        /// Devuelve el patrón resultante.
        /// </summary>
        /// <param name="inPattern">Patrón inicial</param>
        /// <param name="heightmap">Heightmap utilizado para el diagnostico, puede ser null</param>
        /// <param name="iterations">Cantidad de intentos de reconocer la imagen</param>
        private List<Neuron> recognizePattern(List<Neuron> inPattern, List<double> heightmap)
        {
            bool heightmapMethod = generationMethod == GenerationMethod.Heightmap;
            List<Neuron> outPattern = inPattern;
            List<Neuron> currentState;

            do
            {
                NeuralNetwork.Run(outPattern, heightmap, updSequence);
                currentState = NeuralNetwork.Neurons;
                if (heightmapMethod && currentState == outPattern)
                    break;
                else
                    outPattern = currentState;
            } while (heightmapMethod);

            return outPattern;
        }

        /// <summary>
        /// Crea la red neuronal artificial con la cantidad de neuronas especificadas.
        /// </summary>
        /// <param name="neurons">Cantidad de neuronas con que sera creada la red</param>
        /// <param name="energyHandle">Handler para el evento de cambio de energia del estado de la red</param>
        /// <param name="method">Método que se utilizara para generar los patrones</param>
        public void CreateNN(int neurons, EnergyChangedHandler energyHandle = null, GenerationMethod method = GenerationMethod.Normal, UpdateSequence updSequence = UpdateSequence.PseudoRandom)
        {
            //Crea la red
            NeuralNetwork = new NeuralNetwork(neurons);
            if (energyHandle != null)
                NeuralNetwork.EnergyChanged += new EnergyChangedHandler(energyHandle);

            //Instancia las variables
            this.learnedKanjis = new Dictionary<string, Kanji>();
            this.generationMethod = method;
            this.updSequence = updSequence;
        }

        /// <summary>
        /// Extrae el patrón de una imagen, con la cantidad de componentes igual a la cantidad de neuronas en la red.
        /// </summary>
        /// <param name="sourceBitmap">Imagen a partir de la cual se extrae el patrón</param>
        /// <param name="activationValue">Valor utilizado para establer si un pixel es orientado o no en la matriz de pesos</param>
        /// <param name="outBitmap">Imagen resultante a partir de la cual se extrajo el patron</param>
        private List<Neuron> patternFromBitmap(Bitmap sourceBitmap, float threshold, out Bitmap outBitmap)
        {
            //El patron esta formado por un conjunto de estados de las neuronas
            List<Neuron> pattern = new List<Neuron>(NeuralNetwork.NeuronsCount);

            //Escala la imagen para que tenga tanto pixeles como neuronas y la pasa a blanco y negro
            sourceBitmap = ImageAPI.ResizeBitmap(sourceBitmap, sqrtNeuronsCount, sqrtNeuronsCount);            
            sourceBitmap = ImageAPI.BitmapToMonochrome(sourceBitmap, threshold);

            //Establece los estados de las neuronas para cada pixel
            for (int i = 0; i < sqrtNeuronsCount; i++)
            {
                for (int j = 0; j < sqrtNeuronsCount; j++) //La matriz es cuadrada
                {
                    Neuron neuron = new Neuron();
                    int pixelValue = Math.Abs(sourceBitmap.GetPixel(i, j).ToArgb());
                    if (pixelValue == 1)
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
                for (int j = 0; j < resultBitmap.Height; j++)
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
        /// Calcula el heightmap a partir de un bitmap monocromo que representa el patrón relacionado.
        /// </summary>
        /// <param name="pattern">Bitmap a partir del cual se calculan las alturas</param>
        private List<double> calculateHeightmap(Bitmap patternBitmap)
        {
            int width = patternBitmap.Width;
            int height = patternBitmap.Height;
            int blackColor = Color.Black.ToArgb();
            var pixelActivated = new Func<Color, bool>(i => { return i.ToArgb() == blackColor; });

            //Matriz de alturas a calcular
            int[,] heightmapMatrix = new int[patternBitmap.Width, patternBitmap.Height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    heightmapMatrix[x, y] = 0;

            //Completo el nivel 1 (los contornos)
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    //Estado del pixel (true -> activado, false -> descativado)
                    bool activated = patternBitmap.GetPixel(x, y).ToArgb() == blackColor;

                    //Variable que indica si un pixel contiguo tiene un estado opuesto
                    bool border = false;

                    //Verifica los pixeles contiguos
                    if (x > 0 && pixelActivated(patternBitmap.GetPixel(x - 1, y)) != activated)
                        border = true;
                    else if (x < width - 1 && pixelActivated(patternBitmap.GetPixel(x + 1, y)) != activated)
                        border = true;
                    else if (y > 0 && pixelActivated(patternBitmap.GetPixel(x, y - 1)) != activated)
                        border = true;
                    else if (y < height - 1 && pixelActivated(patternBitmap.GetPixel(x, y + 1)) != activated)
                        border = true;

                    if (border)
                        heightmapMatrix[x, y] = activated? -1 : 1;
                }

            //Itero por cada pixel del bitmap y por nivel hasta calcular todas las alturas
            int prevLevel = 1;
            bool isCalculating = true;
            while (isCalculating)
            {
                isCalculating = false;
                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                    {
                        if (heightmapMatrix[x, y] == 0)
                        {
                            bool negSign = false;
                            bool lvlBorder = false;
                            if (x > 0 && Math.Abs(heightmapMatrix[x - 1, y]) == prevLevel)
                            {
                                lvlBorder = true;
                                negSign = heightmapMatrix[x - 1, y] < 0;
                            }
                            else if (x < width - 1 && Math.Abs(heightmapMatrix[x + 1, y]) == prevLevel)
                            {
                                lvlBorder = true;
                                negSign = heightmapMatrix[x + 1, y] < 0;
                            }
                            else if (y > 0 && Math.Abs(heightmapMatrix[x, y - 1]) == prevLevel)
                            {
                                lvlBorder = true;
                                negSign = heightmapMatrix[x, y - 1] < 0;
                            }
                            else if (y < height - 1 && Math.Abs(heightmapMatrix[x, y + 1]) == prevLevel)
                            {
                                lvlBorder = true;
                                negSign = heightmapMatrix[x, y + 1] < 0;
                            }

                            if (lvlBorder)
                            {
                                heightmapMatrix[x, y] = negSign ? -(prevLevel + 1) : (prevLevel + 1);
                                isCalculating = true;
                            }
                        }
                    }
                prevLevel++;
            }

            //Convierte la matriz en una lista, y los niveles en coeficientes dimensionales
            List<double> heightmap = new List<double>(patternBitmap.Width * patternBitmap.Height);
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    int sign = heightmapMatrix[x, y] < 0 ? -2 : 1;
                    double value = Math.Min(Int32.MaxValue, Math.Pow(10, Math.Abs(heightmapMatrix[x, y])));
                    heightmap.Add(value * sign);
                }

            return heightmap;
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
            Hashing = 1,
            Heightmap = 2
        }

        //Variables        
        private Dictionary<string, Kanji> learnedKanjis;
        public NeuralNetwork NeuralNetwork { get; private set; }
        public UpdateSequence updSequence { get; private set; }
        public GenerationMethod generationMethod { get; private set; }
    }
}
