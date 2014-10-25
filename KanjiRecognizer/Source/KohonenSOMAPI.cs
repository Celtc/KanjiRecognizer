using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

using AForge;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace KanjiRecognizer.Source
{
    public class KohonenSOMAPI : NeuralNetworkAPI
    {
        #region Variables (privadas)

        private DistanceNetwork somNN;

        #endregion

        #region Propiedas (publicas)

        public override string ModelName
        {
            get { return "Kohonen SOM"; }
        }

        public override bool IsNetworkCreated
        {
            get { return somNN != null; }
        }

        public override int NeuronsCount
        {
            get { return InputSize + somNN.Layers[0].Neurons.ToArray().Length; }
        }

        public int OutputSize { get; private set; }

        public int LearningIterations { get; private set; }

        public double LearningInitialRadius { get; private set; }

        public double LearningEndingRadius { get; private set; }

        public double LearningInitialRate { get; private set; }

        public double LearningEndingRate { get; private set; }

        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Crea la red neuronal artificial.
        /// </summary>
        public override void CreateANN()
        {
            base.CreateANN();

            // Crea un diccionario vacio
            this.learnedKanjis = new Dictionary<string, Kanji>();

            // Crea la red
            somNN = new DistanceNetwork(InputSize, OutputSize);
        }

        /// <summary>
        /// Enseña una lista de patrones a la red, los cuales seran generados dependiendo el método elegido.
        /// </summary>
        /// <param name="kanjis">Lista de kanjis a aprender</param>
        public override void TeachKanjis(List<Kanji> kanjis)
        {
            // Regenera una lista con los kanjis aprendidos y por aprender
            var allKanjis = learnedKanjis.Values.ToList().Concat(kanjis).ToList();

            // Recrea la red neuronal
            CreateANN();

            // Genera una lista de patrones, una por cada kanji
            var allPatterns = new List<Pattern>(allKanjis.Count);
            foreach(var kanji in allKanjis)
            {
                //Dependiendo del modo obj de aprendizaje genera el patron
                string imageHash = string.Empty;
                Pattern pattern = null;
                switch (Method)
                {
                    case GenerationMethod.Normal:
                    case GenerationMethod.Heightmap:
                        generatePattern_Normal(kanji.sourceImage, out pattern, out imageHash);
                        break;

                    case GenerationMethod.Hashing:
                        generatePattern_Hashing(kanji.sourceImage, out pattern, out imageHash);
                        break;
                }

                // Lo agrega a la lista de patrones a aprender
                allPatterns.Add(pattern);
            }

            // Aprende los patrones
            var classes = teachPatterns(allPatterns);
            if (classes.GroupBy(i => i).Where(g => g.Count() > 1).ToList().Count > 0)
                throw new Exception("No se pudo distinguir la diferencia de clase entre dos kanjis.");

            // Para cada patron busca la neurona representante
            for (int i = 0; i < allKanjis.Count; i++)
            {
                // Guarda el kanji en el diccionario
                learnedKanjis.Add(classes[i].ToString(), allKanjis[i]);
            }
        }

        /// <summary>
        /// Intenta reconocer la imagen dada comparandola contra algunos de los kanjis aprendidos.
        /// En caso de reconocer la imagen, devuelve el kanji correspondiente, sino devuelve null.
        /// </summary>
        /// <param name="sourceImage">Imagen a reconocer</param>
        /// <param name="iterations">Cantidad de intentos de reconocer la imagen</param>
        /// <param name="resultBitmap">Imagen devuelta por la red luego del analisis</param>
        public override Kanji RecognizeKanji(Image sourceImage, out Bitmap resultBitmap)
        {
            // Dependiendo del modo obj de aprendizaje
            Pattern initialPattern = null;
            string accessHash = string.Empty;
            switch (Method)
            {
                case GenerationMethod.Normal:
                case GenerationMethod.Heightmap:
                    generatePattern_Normal(sourceImage, out initialPattern, out accessHash);
                    break;

                case GenerationMethod.Hashing:
                    generatePattern_Hashing(sourceImage, out initialPattern, out accessHash);
                    break;
            }
            
            // Clasifica el patron
            var patternClass = classifyPattern(initialPattern);

            // Busca la clase en el diccionario
            Kanji recognizedKanji = null;
            try
            {
                recognizedKanji = learnedKanjis[patternClass.ToString()];
            }
            catch { }

            // El bitmap de salida sera igual a la imagen original
            resultBitmap = bitmapFromPattern(initialPattern);

            return recognizedKanji;
        }
        
        /// <summary>
        /// Establece el tamaño de la segunda capa de neuronas de SOM.
        /// </summary>
        /// <param name="outputSize">Tamaño</param>
        public void SetOutputSize(int outputSize)
        {
            OutputSize = outputSize;
        }

        /// <summary>
        /// Establece el numero de iteraciones a realizar en cada aprendizaje
        /// </summary>
        /// <param name="iterations">Cantidad de iteraciones</param>
        public void SetLearningIterations(int iterations)
        {
            LearningIterations = iterations;
        }

        /// <summary>
        /// Establece el radio de aprendizaje incial
        /// </summary>
        /// <param name="initialRadius">Radio</param>
        public void SetLearningInitialRadius(double initialRadius)
        {
            LearningInitialRadius = initialRadius;
        }

        /// <summary>
        /// Establece el radio de aprendizaje final
        /// </summary>
        /// <param name="endingRadius">Radio</param>
        public void SetLearningEndingRadius(double endingRadius)
        {
            LearningEndingRadius = endingRadius;
        }

        /// <summary>
        /// Establece el ratio de aprendizaje con el cual comenzaran las iteraciones
        /// </summary>
        /// <param name="initialRate">Ratio inicial</param>
        public void SetLearningInitialRate(double initialRate)
        {
            LearningInitialRate = initialRate;
        }

        /// <summary>
        /// Establece el ratio de aprendizaje con el cual terminara al finalizar las iteraciones
        /// </summary>
        /// <param name="endingRate">Ratio final</param>
        public void SetLearningEndingRate(double endingRate)
        {
            LearningEndingRate = endingRate;
        }

        #endregion

        #region Metodos Privados

        /// <summary>
        /// Enseña un patron a la red. Devuelve una lista con el numero de neurona representante de cada uno
        /// </summary>
        /// <param name="pattern">Patron a aprender</param>
        /// <returns>Lista de ints que representan los numeros de neuronas representantes para cada clase o patron</returns>
        private List<int> teachPatterns(List<Pattern> patterns)
        {
            // Crea un entrenador
            var sqrtOutputSize = (int)Math.Sqrt(OutputSize);
            SOMLearning trainer = new SOMLearning(somNN, sqrtOutputSize, sqrtOutputSize);

            // Crea el set de datos a entrenar
            var trainingSet = generateInputSet(patterns);

            // Iteraciones de aprendizaje
            for (int i = 0; i < LearningIterations; i++)
            {
                // Establece los valores de aprendizaje y radio para la corrida actual
                var completedRatio = i / (LearningIterations - 1);
                trainer.LearningRate = completedRatio * LearningEndingRate + (1 - completedRatio) * LearningInitialRate;
                trainer.LearningRadius = completedRatio * LearningEndingRadius + (1 - completedRatio) * LearningInitialRadius;
                
                // Ejecuta la corrida
                trainer.RunEpoch(trainingSet);
            }

            // Genera la lista de clases para cada patron aprendido
            var classes = new List<int>(patterns.Count);
            foreach(var pattern in patterns)
                classes.Add(classifyPattern(pattern));

            return classes;
        }

        /// <summary>
        /// Clasifica un patron devolviendo el numero de neurona mas cercano (menor distancia)
        /// </summary>
        /// <param name="pattern">Patron a clasificar</param>
        /// <returns>Numero de neurona representante de una clase</returns>
        private int classifyPattern(Pattern pattern)
        {
            // Computa el patorn en la red y solicita la neurona ganadora
            somNN.Compute(generateInput(pattern));
            return somNN.GetWinner();
        }

        /// <summary>
        /// Genera un input set a partir de una lista de patrones graficos
        /// </summary>
        /// <param name="patterns">Lista de patrones graficos</param>
        /// <returns>Input set valido para SOM</returns>
        private double[][] generateInputSet(List<Pattern> patterns)
        {
            double[][] inputSet = new double[patterns.Count][];
            for (int i = 0; i < patterns.Count; i++)
                inputSet[i] = generateInput(patterns[i]);

            return inputSet;
        }

        /// <summary>
        /// Genera un input valido para la SOM
        /// </summary>
        /// <param name="pattern">Patron a partir del cual se genera el input</param>
        /// <returns>Input</returns>
        private double[] generateInput(Pattern pattern)
        {
            return pattern.ToList().Select(x => (double)x).ToArray();
        }
        
        #endregion
    }
}
