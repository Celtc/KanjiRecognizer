using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KanjiRecognizer.Source
{
    public class Pattern
    {
        #region Variables (private)

        private int size;
        private int[] values;

        #endregion

        #region Propiedades (public)

        public int this[int x, int y]
        {
            get { return this.values[TwoToOneIndex(x, y)]; }
            set { this.values[TwoToOneIndex(x, y)] = value; }
        }

        public int this[int i]
        {
            get { return this.values[i]; }
            set { this.values[i] = value; }
        }

        /// <summary>
        /// Devuelve la cantidad de elementos que posee el patron
        /// </summary>
        public double Count
        {
            get { return this.values.Length; }
        }

        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size">Longitud en una direccion del patron, la cantidad de elementos totales sera size * size</param>
        public Pattern(int size)
        {
            this.size = size;
            this.values = new int[size * size];
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="elements">Cantidad total de elementos</param>
        public Pattern(double elements)
        {
            var sqrt = Math.Sqrt(elements);
            if (sqrt % 1 != 0) throw new NotSupportedException();

            this.size = (int)sqrt;
            this.values = new int[size * size];
        }

        /// <summary>
        /// Devuelve una lista conteniendo todos los elementos del patron
        /// </summary>
        public List<int> ToList()
        {
            return values.ToList();
        }

        #endregion

        #region Metodos Privados

        private int TwoToOneIndex(int x, int y)
        {
            return x * size + y;
        }

        #endregion
    }
}
