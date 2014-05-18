using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using KanjiRecognizer.Source;

namespace KanjiRecognizer
{
    public partial class frmCreanteNN : Form
    {
        //Builder
        public frmCreanteNN()
        {
            InitializeComponent();
        }
        
        //OnLoad
        private void frmCreanteNN_Load(object sender, EventArgs e)
        {
            //Carga el valor por defecto
            threshold = 0.8f;
            nCount = int.Parse(KanjiRecognizer.Properties.Resources.DefaultNumberOfNeurons);
            textBox_nCount.Text = KanjiRecognizer.Properties.Resources.DefaultNumberOfNeurons;
            comboBox_gMethod.DataSource = Enum.GetValues(typeof(NeuralNetworkAPI.GenerationMethod)).Cast<NeuralNetworkAPI.GenerationMethod>().Take(2).ToArray();
            comboBox_gMethod.SelectedIndex = 0;
            comboBox_updSequence.DataSource = Enum.GetValues(typeof(HopfieldNeuralNetwork.UpdateSequence));
            comboBox_updSequence.SelectedIndex = 0;
        }

        //Cambio del metodo de generación de patrón
        private void comboBox_gMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            NeuralNetworkAPI.GenerationMethod gMethod;
            Enum.TryParse<NeuralNetworkAPI.GenerationMethod>(comboBox_gMethod.SelectedValue.ToString(), out gMethod);
            if (gMethod == NeuralNetworkAPI.GenerationMethod.Hashing)
                nudThreshold.Enabled = false;
            else
                nudThreshold.Enabled = true;
        }

        //Evento de cambio en el valor del slide
        private void nudThreshold_ValueChanged(object sender, EventArgs e)
        {
            threshold = (float)nudThreshold.Value / 100;
        }

        //Evento de KeyDown en el textbox de cantidad de neuronas
        private void textBox_nCount_KeyDown(object sender, KeyEventArgs e)
        {
            //Ignora los ENTER para el textbox
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        //Evento de Click en el btn de crear
        private void button_createNN_Click(object sender, EventArgs e)
        {
            //Crea la red
            try
            {
                //Metodo de generacion de patrones
                NeuralNetworkAPI.GenerationMethod gMethod;
                Enum.TryParse<NeuralNetworkAPI.GenerationMethod>(comboBox_gMethod.SelectedValue.ToString(), out gMethod);

                //Secuencia de actualizado
                HopfieldNeuralNetwork.UpdateSequence updSequence;
                Enum.TryParse<HopfieldNeuralNetwork.UpdateSequence>(comboBox_updSequence.SelectedValue.ToString(), out updSequence); 

                //Instancia y crea la red
                var nnAPI = new NeuralNetworkAPI();
                nnAPI.CreateNN(nCount, threshold, null, gMethod, updSequence);

                this.ReturnNN = nnAPI;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (System.OutOfMemoryException ex)
            {
                MessageBox.Show("La red neuronal es demasiado grande.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Evento de validación del textbox de cantidad de neuronas
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
                errorMsg = "La raiz del numero debe ser un valor entero. Ej.: 20x20, 40x40, 64x64 etc.";
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

        //Evento de post validación del textbox de cantidad de neuronas
        private void textBox_nCount_Validated(object sender, EventArgs e)
        {
            try
            {
                //Parsea el valor
                var newValue = textBox_nCount.Text;
                nCount = int.Parse(newValue);
            }
            catch
            {
                MessageBox.Show("Ocurrio un error inesperado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_nCount.Text = nCount.ToString();
            }
        }

        //Variables
        private int nCount { get; set; }
        private float threshold { get; set; }
        public NeuralNetworkAPI ReturnNN { get; private set; }
    }
}
