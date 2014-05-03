using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HopfieldNeuralNetwork
{
    /// <summary>
    /// Provides data for the <typeparamref name="EnergyChanged"/> event
    /// </summary>
    public class EnergyEventArgs : EventArgs
    {
        /// <summary>
        /// Energy of Neural network
        /// </summary>
        public double Energy { get; private set; }

        /// <summary>
        /// Index of neuron, which state changing led to energy descrease
        /// </summary>
        public int NeuronIndex { get; private set; }        
   
        /// <summary>
        /// Initializes a new instance of the <typeparamref name="EnergyEventArgs"/> class with the specified value of Energy
        /// </summary>
        /// <param name="Energy">The double that represents the value of neural network energy</param>
        /// <param name="NeuronIndex">The index f neuron caused energy cahnge</param>
        public EnergyEventArgs(double Energy, int NeuronIndex)
        {
            this.Energy = Energy;
            this.NeuronIndex = NeuronIndex;
        }
    }
}
