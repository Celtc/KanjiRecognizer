using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HopfieldNeuralNetwork
{
    /// <summary>
    /// Static class, which describes the sequence in wich the states of the neurones are updated in order to be influenced or not by the new uptaded values.
    /// </summary>
    public enum UpdateSequence
    {
        /// <summary>
        /// Updates all nodes at the same time, based on the existing state (Not on the values the nodes are changing to)
        /// </summary>
        Synchronous = 0,
        /// <summary>
        /// Updates all the nodes in steps but in random order. The updated values are influenced by the changing values
        /// </summary>
        PseudoRandom = 1,
        /// <summary>
        /// Updates all the nodes in steps sequentially from neuron 0 to N. The updated values are influenced by the changing values
        /// </summary>
        Sequential = 2   
    }
}
