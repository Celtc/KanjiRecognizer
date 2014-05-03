using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HopfieldNeuralNetwork;

namespace KanjiRecognizer
{
    public partial class frmCreanteNN : Form
    {
        public frmCreanteNN()
        {
            InitializeComponent();
        }
        
        private void frmCreanteNN_Load(object sender, EventArgs e)
        {
            //Carga el valor por defecto
            NCount = int.Parse(KanjiRecognizer.Properties.Resources.DefaultNumberOfNeurons);
            textBox_nCount.Text = KanjiRecognizer.Properties.Resources.DefaultNumberOfNeurons;
        }

        private void textBox_nCount_KeyDown(object sender, KeyEventArgs e)
        {
            //Ignora los ENTER para el textbox
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void button_createNN_Click(object sender, EventArgs e)
        {
            //Valida la cantidad de neuronas
            if (NCount < 1)
            {
                MessageBox.Show("El valor introducido no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Crea la red
            try
            {
                ReturnNN = new NeuralNetwork(NCount);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (System.OutOfMemoryException ex)
            {
                MessageBox.Show("La red neuronal es demasiado grande.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox_nCount_Validating(object sender, CancelEventArgs e)
        {
            bool error = false;

            //Valida el vacio
            var textBox = sender as TextBox;
            if (textBox.Text == string.Empty)
            {
                textBox.Text = "0";
                return;
            }

            //Valida el tipo de dato
            int number;
            string errorMsg = string.Empty;
            if (!int.TryParse(textBox.Text, out number))
            {
                error = true;
                errorMsg = "El valor no es un entero.";
            }
            else if (number < 2)
            {
                error = true;
                errorMsg = "El valor debe ser mayor a 1.";
            }
            else if (Math.Sqrt(number) % 1 != 0)
            {
                error = true;
                errorMsg = "La raiz del numero debe ser un valor entero. Ej.: 20x20, 40x40, 50x50 etc.";
            }

            //Muestra el resultado de las validaciones
            if (error)
            {
                e.Cancel = true;
                textBox.Select(0, textBox.Text.Length);
                this.errorProvider_frmCreateNN.SetError(textBox, errorMsg);
            }

            return;
        }

        private void textBox_nCount_Validated(object sender, EventArgs e)
        {
            try
            {
                //Parsea el valor
                var newValue = textBox_nCount.Text;
                NCount = int.Parse(newValue);
            }
            catch
            {
                MessageBox.Show("Ocurrio un error inesperado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_nCount.Text = NCount.ToString();
            }
        }

        private int NCount { get; set; }
        public NeuralNetwork ReturnNN { get; private set; }
    }
}
