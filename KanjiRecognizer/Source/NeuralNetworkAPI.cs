using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows;

using HopfieldNeuralNetwork;

namespace KanjiRecognizer.Source
{
    class NeuralNetworkAPI
    {
        public void TeachKanji(Image sourceImage, string name, string description)
        {
            this.TeachKanji(new Kanji(sourceImage, name, description));
        }
        public void TeachKanji(Kanji kanji)
        {
            //Convierte la imagen a bitmap
            Bitmap sourceBitmap = ImageAPI.AlltoBMP(kanji.sourceImage);

            //Establece el valor de activación de las neuronas
            int activationValue = Math.Abs((int)(Color.Black.ToArgb() / 2));

            //Extrae el patron y su bitmap
            Bitmap processedBitmap;
            List<Neuron> pattern = patternFromBitmap(sourceBitmap, activationValue, out processedBitmap);

            //Guarda el kanji en la lista de aprendidos si no existe
            try
            {
                learnedKanjis.Add(ImageAPI.GenerateHashFromImage(processedBitmap), kanji);
            }
            catch { }

            //Enseña el patron
            NeuralNetwork.AddPattern(pattern);
        }


        public Kanji RecognizeKanji(Image sourceImage, out Bitmap resultBitmap)
        {
            //Convierte la imagen a bitmap
            Bitmap sourceBitmap = ImageAPI.AlltoBMP(sourceImage);

            //Establece el valor de activación de las neuronas
            int activationValue = Math.Abs((int)(Color.Black.ToArgb() / 2));

            //Extrae el patron a analizar, que sera el estado inicial
            Bitmap outBitmap;
            List<Neuron> initialState = patternFromBitmap(sourceBitmap, activationValue, out outBitmap);
            
            //Diagnostica el patron
            NeuralNetwork.Run(initialState, false);

            //Busca si el resultado es un patron aprendido un minimo de energia local no esperado
            //Para esto extrae el bitmap del patron resultante y lo busca en los aprendidos
            resultBitmap = bitmapFromPattern(NeuralNetwork.Neurons);
            Kanji recognizedKanji = null;
            try
            {
                string learnedHash = learnedKanjis.Keys.ElementAt(0);
                string resultingHash = ImageAPI.GenerateHashFromImage(resultBitmap);
                recognizedKanji = learnedKanjis[resultingHash];
            }
            catch { }

            return recognizedKanji;
        }

        //Crea la red con la cantidad de neuronas especificadas
        public void CreateNN(int neurons, EnergyChangedHandler energyHandle = null)
        {
            //Crea la red
            NeuralNetwork = new NeuralNetwork(neurons);
            if (energyHandle != null)
                NeuralNetwork.EnergyChanged += new EnergyChangedHandler(energyHandle);

            //Crea una lista vacia
            learnedKanjis = new Dictionary<string, Kanji>();
        }

        //Establece una red neuronal externa
        public void SetNN(NeuralNetwork neuralNetwork, EnergyChangedHandler energyHandle = null)
        {
            //Establece la red
            this.NeuralNetwork = neuralNetwork;
            if (energyHandle != null)
                neuralNetwork.EnergyChanged += new EnergyChangedHandler(energyHandle);

            //Crea una lista vacia
            learnedKanjis = new Dictionary<string, Kanji>();
        }

        //Crea el patron segun la cantidad de neuronas de la red y usando el valor de activacion calculado
        private List<Neuron> patternFromBitmap(Bitmap sourceBitmap, int activationValue, out Bitmap outBitmap)
        {
            //El patron esta formado por un conjunto de estados de las neuronas
            List<Neuron> pattern = new List<Neuron>(NeuralNetwork.NeuronsCount);

            //Escala la imagen para que tenga tanto pixeles como neuronas
            sourceBitmap = ImageAPI.ResizeBitmap(sourceBitmap, matrixWidth, matrixWidth);

            //Establece los estados de las neuronas para cada pixel
            for (int i = 0; i < matrixWidth; i++)
            {
                for (int j = 0; j < matrixWidth; j++) //La matriz es cuadrada
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

        //Crea un bitmap a partir de un patron. Esta sera una imagen en blanco y negro
        private Bitmap bitmapFromPattern(List<Neuron> pattern)
        {
            Bitmap resultBitmap = new Bitmap(matrixWidth, matrixWidth);
            for (int i = 0; i < resultBitmap.Width; i++)
            {
                for (int j = 0; j < resultBitmap.Height; j++) //La matriz es cuadrada
                {
                    int currIndex = matrixWidth * i + j;
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

        //Devuelve el ancho de la matriz cuadrada 
        private int matrixWidth 
        {
            get { return (int) Math.Sqrt(NeuralNetwork.NeuronsCount); }
        }

        //Variables
        private Dictionary<string, Kanji> learnedKanjis;
        public NeuralNetwork NeuralNetwork { get; private set; }
    }
}
