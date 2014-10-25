using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;

namespace KanjiRecognizer.Source
{
    public abstract class NeuralNetworkAPI
    {
        #region Variables (privadas)

        protected Dictionary<string, Kanji> learnedKanjis;

        #endregion

        #region Propiedas (publicas)

        /// <summary>
        /// Nombre del modelo en cual se basa la RNA
        /// </summary>
        public abstract string ModelName { get; }

        /// <summary>
        /// Informa si la red neuronal se encuentra creada
        /// </summary>
        public abstract bool IsNetworkCreated { get; }

        public abstract int NeuronsCount { get; }

        public virtual float Threshold { get; protected set; }

        public virtual int InputSize { get; protected set; }

        public virtual int SqrtInputSize { get; protected set; }

        public virtual GenerationMethod Method { get; protected set; }

        public virtual ReadOnlyCollection<Kanji> PatternsLearned { get { return learnedKanjis.Values.ToList().AsReadOnly(); } }

        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Crea la red neuronal artificial.
        /// </summary>
        public virtual void CreateANN()
        {
            if (InputSize == 0 || SqrtInputSize == 0 || Threshold == .0f)
                throw new Exception("Parametros necesarios para la creación de la red no establecidos.");
        }
        
        /// <summary>
        /// Enseña una lista de patrones a la red, los cuales seran generados dependiendo el método elegido.
        /// </summary>
        /// <param name="kanjis">Lista de kanjis a aprender</param>
        public abstract void TeachKanjis(List<Kanji> kanjis);

        /// <summary>
        /// Intenta reconocer la imagen dada comparandola contra algunos de los kanjis aprendidos.
        /// En caso de reconocer la imagen, devuelve el kanji correspondiente, sino devuelve null.
        /// </summary>
        /// <param name="sourceImage">Imagen a reconocer</param>
        /// <param name="resultBitmap">Imagen devuelta por la red luego del analisis</param>
        public abstract Kanji RecognizeKanji(Image sourceImage, out Bitmap resultBitmap);

        /// <summary>
        /// Tamaño del input de la red (unidimensional)
        /// </summary>
        /// <param name="inputSize">Tamaño</param>
        public virtual void SetInputSize(int inputSize)
        {
            InputSize = inputSize;
            SqrtInputSize = (int)Math.Sqrt(inputSize);
        }

        /// <summary>
        /// Establece el valor del threshold.
        /// </summary>
        /// <param name="value">Nuevo valor</param>
        public virtual void SetThreshold(float value)
        {
            Threshold = value;
        }

        /// <summary>
        /// Establece el metodo de generacion de patrones.
        /// No puede cambiarse una vez que se ha comenzado a enseñar a la red.
        /// </summary>
        /// <param name="method">Metodo a utilizar</param>
        public virtual void SetMethod(GenerationMethod method)
        {
            if (IsNetworkCreated && PatternsLearned.Count > 0)
                throw new ApplicationException("Imposible cambiar el metodo de generación de patrones una vez que ya ha comenzado a enseñarse a la red.");

            Method = method;
        }

        #endregion

        #region Metodos Privados
                
        /// <summary>
        /// Genera el patrón a través del metodo habitual.
        /// </summary>
        /// <param name="sourceImage">Imagen a partir de la cual se generara el patrón</param>
        /// <param name="pattern">Patrón resultante</param>
        /// <param name="patternHash">Hash que identifica unívocamente este patrón</param>
        protected virtual void generatePattern_Normal(Image sourceImage, out Pattern pattern, out string patternHash)
        {
            //Convierte la imagen a bitmap
            Bitmap sourceBitmap = ImageAPI.AlltoBMP(sourceImage);

            //Extrae el patron y su bitmap
            Bitmap processedBitmap;
            pattern = patternFromBitmap(sourceBitmap, Threshold, out processedBitmap);

            //Genera el hash a partir del bitmap de salida
            patternHash = ImageAPI.GenerateSHA1HashFromImage(processedBitmap);

            return;
        }

        /// <summary>
        /// Genera el patrón a través del metodo habitual, y luego crea el heightmap del patron generado.
        /// </summary>
        /// <param name="sourceImage">Imagen a partir de la cual se generara el patrón</param>
        /// <param name="pattern">Patrón resultante</param>
        /// <param name="heightmap">Heightmap del patrón generado</param>
        /// <param name="patternHash">Hash que identifica unívocamente este patrón</param>
        protected virtual void generatePattern_Heightmap(Image sourceImage, out Pattern pattern, out List<double> heightmap, out string patternHash)
        {
            generatePattern_Normal(sourceImage, out pattern, out patternHash);

            heightmap = calculateHeightmap(bitmapFromPattern(pattern));

            return;
        }

        /// <summary>
        /// Genera el patrón a través del metodo de hashing, aumentando la ortoganalidad de los patrones generados
        /// pero limitando el reconocimiento a patrones idénticos.
        /// </summary>
        /// <param name="sourceImage">Imagen a partir de la cual se generara el patrón</param>
        /// <param name="pattern">Patrón resultante</param>
        /// <param name="patternHash">Hash que identifica unívocamente este patrón</param>
        protected virtual void generatePattern_Hashing(Image sourceImage, out Pattern pattern, out string patternHash)
        {
            //Extrae un hash de tantos bits como neuronas a partir de la imagen
            BitArray bitHash = ImageAPI.GenerateBitHashFromImage(sourceImage, InputSize);

            //Extrae el patron y su bitmap
            Bitmap processedBitmap;
            pattern = patternFromBithash(bitHash, out processedBitmap);

            //Genera el hash a partir del bitmap de salida
            patternHash = ImageAPI.GenerateSHA1HashFromImage(processedBitmap);

            return;
        }

        /// <summary>
        /// Extrae el patrón de una imagen, con la cantidad de componentes igual a la cantidad de neuronas en la red.
        /// </summary>
        /// <param name="sourceBitmap">Imagen a partir de la cual se extrae el patrón</param>
        /// <param name="activationValue">Valor utilizado para establer si un pixel es orientado o no en la matriz de pesos</param>
        /// <param name="outBitmap">Imagen resultante a partir de la cual se extrajo el patron</param>
        protected virtual Pattern patternFromBitmap(Bitmap sourceBitmap, float threshold, out Bitmap outBitmap)
        {
            //El patron esta formado por un conjunto de estados de las neuronas
            Pattern pattern = new Pattern((double)InputSize);

            //Escala la imagen para que tenga tanto pixeles como neuronas y la pasa a blanco y negro
            sourceBitmap = ImageAPI.ResizeBitmap(sourceBitmap, SqrtInputSize, SqrtInputSize);
            sourceBitmap = ImageAPI.BitmapToMonochrome(sourceBitmap, threshold);

            //Establece los estados de las neuronas para cada pixel
            for (int i = 0; i < SqrtInputSize; i++)
            {
                for (int j = 0; j < SqrtInputSize; j++) //La matriz es cuadrada
                {
                    int pixelValue = Math.Abs(sourceBitmap.GetPixel(i, j).ToArgb());
                    if (pixelValue == 1)
                    {
                        sourceBitmap.SetPixel(i, j, Color.White);
                        pattern[i, j] = 0;
                    }
                    else
                    {
                        sourceBitmap.SetPixel(i, j, Color.Black);
                        pattern[i, j] = 1;
                    }
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
        protected virtual Pattern patternFromBithash(BitArray bithash, out Bitmap outBitmap)
        {
            //Valida la cantidad de bits iniciales
            if (bithash.Count < InputSize)
            {
                outBitmap = null;
                return null;
            }

            //El patron esta formado por un conjunto de estados de las neuronas
            Pattern pattern = new Pattern((double)InputSize);

            //Crea el bitmap que se devolvera
            Bitmap processedBitmap = new Bitmap(SqrtInputSize, SqrtInputSize);

            //Establece los estados de las neuronas para cada bit
            for (int i = 0; i < InputSize; i++)
            {
                if (bithash.Get(i))
                {
                    processedBitmap.SetPixel(i / SqrtInputSize, i % SqrtInputSize, Color.Black);
                    pattern[i] = 1;
                }
                else
                {
                    processedBitmap.SetPixel(i / SqrtInputSize, i % SqrtInputSize, Color.White);
                    pattern[i] = 0;
                }
            }
            outBitmap = processedBitmap;
            return pattern;
        }

        /// <summary>
        /// Crea un bitmap a partir de un patrón. Esta será una imagen monocromo.
        /// </summary>
        /// <param name="pattern">Patrón utilizado para generar el bitmap</param>
        protected virtual Bitmap bitmapFromPattern(Pattern pattern)
        {
            Bitmap resultBitmap = new Bitmap(SqrtInputSize, SqrtInputSize);
            for (int i = 0; i < resultBitmap.Width; i++)
            {
                for (int j = 0; j < resultBitmap.Height; j++)
                {
                    if (pattern[i, j] == 1)
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
        protected virtual List<double> calculateHeightmap(Bitmap patternBitmap)
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
                        heightmapMatrix[x, y] = activated ? -1 : 1;
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

        #endregion

        //Modos de generación de patrones para el aprendizaje de los mismos
        public enum GenerationMethod
        {
            Normal = 0,
            Hashing = 1,
            Heightmap = 2
        }
    }
}
