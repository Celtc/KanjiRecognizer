using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HopfieldNeuralNetwork
{
    /// <summary>
    /// Represents the method that will handle an event that rise when Energy of Hopfield Neural Network changes.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">An <typeparamref name="EnergyEventArgs"/> that contains value of Energy</param>
    /// <seealso cref="HopfieldNeuralNetwork.Neuron"/>
    /// <seealso cref="HopfieldNeuralNetwork.NeuralNetwork"/>
    public delegate void EnergyChangedHandler(object sender, EnergyEventArgs e);
    
    /// <summary>
    /// Defines the class for Hopfield Neural Network
    /// </summary>
    /// <seealso cref="HopfieldNeuralNetwork.Neuron"/>
    public class NeuralNetwork
    {

        /// <summary>
        /// Number of neurons in neural network
        /// </summary>
        /// <value>An <typeparamref name="Int32"/> representing a number of neurons</value>
        public int NeuronsCount { get; private set; }

        /// <summary>
        /// Number of patterns currently stored in interconnection matrix
        /// </summary>
        /// <value>An <typeparamref name="Int32"/> representing a number of patterns</value>
        /// <remarks>The value of M increases every time when new pattern added via AddPattern or AddRandomPattern</remarks>
        public int PatternsCount { get; private set; }
        
        /// <summary>
        /// The value of Neural network energy
        /// </summary>
        public double Energy { get; private set; }

        /// <summary>
        /// The interonnection matrix of Neural network
        /// </summary>
        public int[,] Matrix  { get; private set; }

        /// <summary>
        /// Current Neural Network state.
        /// </summary>
        public List<Neuron> Neurons { get; private set; }

        /// <summary>
        /// Calculate the energy of currente network.
        /// </summary>
        private void CalculateEnergy()
        {
            double tempE = 0;
            for (int i = 0; i < NeuronsCount; i++)
                for (int j = 0; j < NeuronsCount; j++)
                    if (i != j)
                        tempE += Matrix[i, j] * Neurons[i].State * Neurons[j].State;
            Energy = -1 * tempE / 2;
        }
        
        /// <summary>
        /// Initializes a new instance of the <seealso cref="NeuralNetwork"/> class
        /// </summary>
        /// <param name="n">Number of neurons</param>
        public NeuralNetwork(int n)
        {
            this.NeuronsCount = n;
            Neurons = new List<Neuron>(n);
            for (int i = 0; i< n; i++)
            {
                Neuron neuron = new Neuron();
                neuron.State = 0;
                Neurons.Add(neuron);
            }
            
            Matrix = new int[n, n];
            PatternsCount = 0;
            
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Matrix[i, j] = 0;
                }
        }

        /// <summary>
        /// Adds a random pattern to interconnection matrix
        /// </summary>
        public void AddRandomPattern()
        {
            List<Neuron> randomPattern = new List<HopfieldNeuralNetwork.Neuron>(NeuronsCount);
            Random rnd = new Random();
            for (int i = 0; i < NeuronsCount; i++)
            {
                Neuron neuron = new Neuron();
                randomPattern.Add(neuron);
                
                int bit;
                bit = rnd.Next(2);
                if (bit == 0) randomPattern[i].State = NeuronStates.AlongField; 
                else
                if (bit == 1) randomPattern[i].State = NeuronStates.AgainstField;
            }
            for (int i = 0; i < NeuronsCount; i++)
                for (int j = 0; j < NeuronsCount; j++)
                {
                    if (i == j) Matrix[i, j] = 0;
                    else Matrix[i, j] += randomPattern[i].State * randomPattern[j].State;
                }
            PatternsCount++;
        }

        /// <summary>
        /// Adds specified pattern to intercconnection matrix
        /// </summary>
        /// <param name="Pattern">A list of neurons</param>
        public void AddPattern(List<Neuron> Pattern)
        {
            for (int i = 0; i < NeuronsCount; i++)
                for (int j = 0; j < NeuronsCount; j++)
                {
                    if (i == j) Matrix[i, j] = 0;
                    else Matrix[i, j] += (Pattern[i].State * Pattern[j].State);
                }
            PatternsCount++;
        }
        
        /// <summary>
        /// Clears values of interconnection matrix.
        /// </summary>
        public void FreeMatrix()
        {
            for (int i = 0; i < NeuronsCount; i++)
                for (int j = 0; j < NeuronsCount; j++)
                    Matrix[i, j] = 0;
        }

        /// <summary>
        /// Sets specified initial state and runs networks dynamics
        /// </summary>
        /// <param name="initialState">A list of neurons which determines an initional state</param>
        public void Run(List<Neuron> initialState, bool triggerEnergyChange)
        {
            this.Neurons = initialState;
            int k = 1;
            int h = 0;
                while(k != 0)
                {
                    k = 0;
                    for (int i = 0; i < NeuronsCount; i++)
                    {
                        h = 0;
                        for (int j = 0; j < NeuronsCount; j++)                        
                          h += Matrix[i, j] * (Neurons[j].State);
                        
                        if (Neurons[i].ChangeState(h))
                        {
                            k++;
                            if (triggerEnergyChange)
                            {
                                CalculateEnergy();
                                OnEnergyChanged(new EnergyEventArgs(Energy, i));
                            }
                        }
                    }
                }
                CalculateEnergy();             
        }

        /// <summary>
        /// Occurs when the energy of neural network changes
        /// </summary>
        public event EnergyChangedHandler EnergyChanged;
        
        /// <summary>
        /// Rises the <seealso cref="EnergyChanged"/> event
        /// </summary>
        /// <param name="e">An <typeparamref name="EnergyEventArgs"/> that contains value of Energy and index of neuron that couses energy change</param>
        protected virtual void OnEnergyChanged(EnergyEventArgs e)
        {
            if (EnergyChanged != null)
                EnergyChanged(this, e);
        }
    }
}
