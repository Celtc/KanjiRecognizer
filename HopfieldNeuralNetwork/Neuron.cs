﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HopfieldNeuralNetwork
{
    /// <summary>
    /// Defines the base class of neuron.
    /// </summary>
    public class Neuron
    {
        /// <summary>
        /// Gets or sets the state of neuron
        /// </summary>
        /// <seealso cref="HopfieldNeuralNetwork.NeuronStates"/>
        public int State { get; set; }

        /// <summary>
        /// Initializes a new instance Neuron class
        /// </summary>
        public Neuron()
        {
            int r = new Random().Next(2);
            switch (r)
            {
                case 0: State = NeuronStates.AlongField; break;
                case 1: State = NeuronStates.AgainstField; break;
            }
        }

        /// <summary>
        /// Calculates necessity, and if so, changes state of neuron
        /// </summary>
        /// <param name="field">Local field actiong on neuron from all other neurons of network</param>
        /// <returns>True if during calculations neuron chages its state, false otherwise</returns>
        public bool ChangeState(Double field)
        {
            bool res = false;
            if (field * this.State < 0)
            {
                this.State = -this.State;
                res = true;
            }
            return res;
        }
    }
}
