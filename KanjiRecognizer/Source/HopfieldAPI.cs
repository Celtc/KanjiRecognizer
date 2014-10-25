using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;

using HopfieldNeuralNetwork;

namespace KanjiRecognizer.Source
{
    public class HopfieldAPI : NeuralNetworkAPI
    {
        #region Variables (privadas)

        private NeuralNetwork hopfieldNN;

        #endregion

        #region Propiedas (publicas)

        public override string ModelName
        {
            get { return "Hopfield"; }
        }

        public override bool IsNetworkCreated
        {
            get { return hopfieldNN != null; }
        }

        public override int NeuronsCount
        {
            get { return hopfieldNN.NeuronsCount; }
        }

        public UpdateSequence UpdSequence { get; private set; }

        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Crea la red neuronal.
        /// </summary>
        public override void CreateANN()
        {
            base.CreateANN();

            // Crea un diccionario vacio
            this.learnedKanjis = new Dictionary<string, Kanji>();

            // Crea la red
            hopfieldNN = new NeuralNetwork(InputSize);  
        }

        /// <summary>
        /// Enseña una lista de patrones a la red, los cuales seran generados dependiendo el método elegido.
        /// </summary>
        /// <param name="kanjis">Lista de kanjis a aprender</param>
        public override void TeachKanjis(List<Kanji> kanjis)
        {
            // Comienza el worker
            Error = null;
            Progress = 0;
            needToStop = false;
            backgroundRecognizerWorker.RunWorkerAsync(kanjis);  
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
        /// Establece el handler receptor de los eventos de cambio de energia de la red
        /// </summary>
        /// <param name="energyHandler">Handler</param>
        public void SetEnergyHandler(EnergyChangedHandler energyHandler)
        {
            hopfieldNN.EnergyChanged += new EnergyChangedHandler(energyHandler);
        }

        /// <summary>
        /// Establece el metodo de actualización de las neuronas 
        /// </summary>
        /// <param name="updSequence">Metodo a utilizar</param>
        public void SetUpdSequence(UpdateSequence updSequence)
        {
            UpdSequence = updSequence;
        }

        #endregion

        #region Metodos Privados
        
        /// <summary>
        /// Enseña patrones a la red en un hilo secundario.
        /// </summary>
        protected override void teachingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Extrae los kanjis
            var kanjis = (List<Kanji>)e.Argument;

            // El aprendizaje de hopfield es unico por patron
            for (int i = 0; i < kanjis.Count; i++)
            {
                // Kanji actual
                var currKanji = kanjis[i];

                // Dependiendo del modo obj de aprendizaje genera el patron
                string accessHash = string.Empty;
                Pattern pattern = null;
                switch (Method)
                {
                    case GenerationMethod.Normal:
                    case GenerationMethod.Heightmap:
                        generatePattern_Normal(currKanji.sourceImage, out pattern, out accessHash);
                        break;

                    case GenerationMethod.Hashing:
                        generatePattern_Hashing(currKanji.sourceImage, out pattern, out accessHash);
                        break;
                }

                // Revisa que no exisitiera el patron ya en la base de datos
                if (!learnedKanjis.ContainsKey(accessHash))
                {
                    // Guarda el nuevo kanji
                    learnedKanjis.Add(accessHash, currKanji);

                    // Enseña el patron
                    teachPattern(pattern);
                }

                // Informa progreso
                var completedRatio = (float)i / (kanjis.Count - 1);
                ((BackgroundWorker)sender).ReportProgress((int)(completedRatio * 100 * .99f));

                // Revisa la solicitud de cancelar
                if (needToStop)
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Reconoce un patron en un hilo secundario.
        /// </summary>
        protected override void recognizerWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        /// <summary>
        /// Enseña un patron a la red.
        /// </summary>
        /// <param name="pattern">Patron a aprender</param>
        private void teachPattern(Pattern pattern)
        {
            var neuronPattern = patternToNeurons(pattern);         
            hopfieldNN.AddPattern(neuronPattern);
        }

        /// <summary>
        /// Diagnostica un patrón tantas veces como iteraciones se especifiquen.
        /// Devuelve el patrón resultante.
        /// </summary>
        /// <param name="inPattern">Patrón inicial</param>
        /// <param name="heightmap">Heightmap utilizado para el diagnostico, puede ser null</param>
        private Pattern recognizePattern(Pattern inPattern, List<double> heightmap)
        {
            bool heightmapMethod = Method == GenerationMethod.Heightmap;
            List<Neuron> outputNeurons = patternToNeurons(inPattern);
            List<Neuron> currentState;

            do
            {
                hopfieldNN.Run(outputNeurons, heightmap, UpdSequence);
                currentState = hopfieldNN.Neurons;
                if (heightmapMethod && currentState == outputNeurons)
                    break;
                else
                    outputNeurons = currentState;
            } while (heightmapMethod);

            return neuronsToPattern(outputNeurons);
        }

        /// <summary>
        /// Realiza el pasaje del patron grafico a su interpretacion valida para la NNA de Hopfield
        /// </summary>
        /// <param name="pattern">Patron grafico</param>
        /// <returns>Patron neuronal valido para hopfield</returns>
        private List<Neuron> patternToNeurons(Pattern pattern)
        {
            var result = new List<Neuron>();
            for(int i = 0; i < pattern.Count; i++)
            {
                var neuron = new Neuron();
                neuron.State = pattern[i] == 1 ? NeuronStates.AgainstField : NeuronStates.AlongField;
                result.Add(neuron);
            }

            return result;
        }

        /// <summary>
        /// Realiza el pasaje del patron neuronal de hopfield a su equivalente grafica monocromatica
        /// </summary>
        /// <param name="neurons">Patron neuronal de hopfield</param>
        /// <returns>Patron grafico</returns>
        private Pattern neuronsToPattern(List<Neuron> neurons)
        {
            var pattern = new Pattern((double)neurons.Count);
            for (int i = 0; i < neurons.Count; i++)
                pattern[i] = neurons[i].State == NeuronStates.AgainstField ? 1 : 0;

            return pattern;
        }

        #endregion
    }
}
