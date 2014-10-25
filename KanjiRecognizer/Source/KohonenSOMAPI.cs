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
            get { return somNN.Layers[0].Neurons.ToArray().Length + somNN.Layers[1].Neurons.ToArray().Length; }
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
                string accessHash = string.Empty;
                Pattern pattern = null;
                switch (Method)
                {
                    case GenerationMethod.Normal:
                    case GenerationMethod.Heightmap:
                        generatePattern_Normal(kanji.sourceImage, out pattern, out accessHash);
                        break;

                    case GenerationMethod.Hashing:
                        generatePattern_Hashing(kanji.sourceImage, out pattern, out accessHash);
                        break;
                }

                // Lo agrega a la lista de patrones a aprender
                allPatterns.Add(pattern);

                // Guarda el kanji en el diccionario
                learnedKanjis.Add(accessHash, kanji);
            }

            // Aprende los patrones
            teachPatterns(allPatterns);            
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
            //Dependiendo del modo obj de aprendizaje
            List<double> heightmap = null;
            Pattern initialPattern = null;
            string accessHash = string.Empty;
            switch (Method)
            {
                case GenerationMethod.Normal:
                    generatePattern_Normal(sourceImage, out initialPattern, out accessHash);
                    break;

                case GenerationMethod.Hashing:
                    generatePattern_Hashing(sourceImage, out initialPattern, out accessHash);
                    break;

                case GenerationMethod.Heightmap:
                    generatePattern_Heightmap(sourceImage, out initialPattern, out heightmap, out accessHash);
                    break;
            }
            
            //Diagnostica el patron
            var returnedPattern = recognizePattern(initialPattern, heightmap);

            //Busca si el resultado es un patron aprendido o un minimo de energia local no esperado
            //Para esto extrae el bitmap del patron resultante y lo busca en los aprendidos a traves de su hash
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
        /// Enseña un patron a la red.
        /// </summary>
        /// <param name="pattern">Patron a aprender</param>
        private void teachPatterns(List<Pattern> patterns)
        {
            // Crea un entrenador
            SOMLearning trainer = new SOMLearning(somNN, SqrtInputSize, SqrtInputSize);

            // Crea el set de datos a entrenar
            var trainingSet = generateTrainingSet(patterns);

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
        }

        /// <summary>
        /// Diagnostica un patrón tantas veces como iteraciones se especifiquen.
        /// Devuelve el patrón resultante.
        /// </summary>
        /// <param name="inPattern">Patrón inicial</param>
        /// <param name="heightmap">Heightmap utilizado para el diagnostico, puede ser null</param>
        private Pattern recognizePattern(Pattern inPattern, List<double> heightmap)
        {
            throw new NotImplementedException();
        //    bool heightmapMethod = Method == GenerationMethod.Heightmap;
        //    List<Neuron> outputNeurons = patternToNeurons(inPattern);
        //    List<Neuron> currentState;

        //    do
        //    {
        //        hopfieldNN.Run(outputNeurons, heightmap, UpdSequence);
        //        currentState = hopfieldNN.Neurons;
        //        if (heightmapMethod && currentState == outputNeurons)
        //            break;
        //        else
        //            outputNeurons = currentState;
        //    } while (heightmapMethod);

        //    return neuronsToPattern(outputNeurons);
        }

        /// <summary>
        /// Genera un training set a partir de una lista de patrones graficos
        /// </summary>
        /// <param name="patterns">Lista de patrones graficos</param>
        /// <returns>Training set valido para SOM</returns>
        private double[][] generateTrainingSet(List<Pattern> patterns)
        {
            double[][] trainingSet = new double[patterns.Count][];
            for (int i = 0; i < patterns.Count; i++)
                trainingSet[i] = patterns[i].ToList().Select(x => (double)x).ToArray();

            return trainingSet;
        }
        
        #endregion
    }
}
