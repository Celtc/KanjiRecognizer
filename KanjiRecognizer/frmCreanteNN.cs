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
        #region Variables (privadas)

        #region Tab1

        private int tab1_nCount { get; set; }
        private float tab1_threshold { get; set; }

        #endregion

        #region Tab2

        private int tab2_n1Count { get; set; }
        private int tab2_n2Count { get; set; }
        private int tab2_iterations { get; set; }
        private float tab2_initialRate { get; set; }
        private float tab2_endingRate { get; set; }
        private float tab2_initialRadius { get; set; }
        private float tab2_endingRadius { get; set; }
        private float tab2_threshold { get; set; }

        #endregion

        public NeuralNetworkAPI ReturnNN { get; private set; }

        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Cosntructor
        /// </summary>
        public frmCreanteNN()
        {
            InitializeComponent();
        }

        #endregion

        #region Metodos Privados
                
        // OnLoad
        private void frmCreanteNN_Load(object sender, EventArgs e)
        {
            // Carga los valores por defecto (Tab 1)
            tab1_nCount = 100;
            tab1_threshold = 0.8f;

            textBox_tab1_nCount.Text = tab1_nCount.ToString();
            comboBox_tab1_gMethod.DataSource = Enum.GetValues(typeof(NeuralNetworkAPI.GenerationMethod)).Cast<NeuralNetworkAPI.GenerationMethod>().Take(2).ToArray();
            comboBox_tab1_gMethod.SelectedIndex = 0;
            comboBox_tab1_updSequence.DataSource = Enum.GetValues(typeof(HopfieldNeuralNetwork.UpdateSequence));
            comboBox_tab1_updSequence.SelectedIndex = 0;

            // Carga los valores por defecto (Tab 2)
            tab2_n1Count = 100;
            tab2_n2Count = 64;
            tab2_iterations = 500;
            tab2_initialRate = .3f;
            tab2_endingRate = 0f;
            tab2_initialRadius = 1.5f;
            tab2_endingRadius = 0f;
            tab2_threshold = 0.8f;

            textBox_tab2_n1Count.Text = tab2_n1Count.ToString();
            textBox_tab2_n2Count.Text = tab2_n2Count.ToString();
            textBox_tab2_iterations.Text = tab2_iterations.ToString();
            textBox_tab2_initialRate.Text = tab2_initialRate.ToString();
            textBox_tab2_endingRate.Text = tab2_endingRate.ToString();
            textBox_tab2_initialRadius.Text = tab2_initialRadius.ToString();
            textBox_tab2_endingRadius.Text = tab2_endingRadius.ToString();
            comboBox_tab2_gMethod.DataSource = Enum.GetValues(typeof(NeuralNetworkAPI.GenerationMethod)).Cast<NeuralNetworkAPI.GenerationMethod>().Take(2).ToArray();
            comboBox_tab2_gMethod.SelectedIndex = 0;
        }

        // Evento de Click en el btn de crear
        private void button_createNN_Click(object sender, EventArgs e)
        {
            //Crea la red
            try
            {
                // Discrimina la pestaña activa
                if (tabControl.SelectedIndex == 0)
                {
                    //Metodo de generacion de patrones
                    NeuralNetworkAPI.GenerationMethod gMethod;
                    Enum.TryParse<NeuralNetworkAPI.GenerationMethod>(comboBox_tab1_gMethod.SelectedValue.ToString(), out gMethod);

                    //Secuencia de actualizado
                    HopfieldNeuralNetwork.UpdateSequence updSequence;
                    Enum.TryParse<HopfieldNeuralNetwork.UpdateSequence>(comboBox_tab1_updSequence.SelectedValue.ToString(), out updSequence);

                    //Instancia y crea la red
                    var nnAPI = new HopfieldAPI();
                    nnAPI.SetInputSize(tab1_nCount);
                    nnAPI.SetThreshold(tab1_threshold);
                    nnAPI.SetMethod(gMethod);
                    nnAPI.SetUpdSequence(updSequence);
                    nnAPI.CreateANN();

                    this.ReturnNN = nnAPI;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else if (tabControl.SelectedIndex == 1)
                {
                    //Metodo de generacion de patrones
                    NeuralNetworkAPI.GenerationMethod gMethod;
                    Enum.TryParse<NeuralNetworkAPI.GenerationMethod>(comboBox_tab2_gMethod.SelectedValue.ToString(), out gMethod);

                    //Instancia y crea la red
                    var nnAPI = new KohonenSOMAPI();
                    nnAPI.SetInputSize(tab2_n1Count);
                    nnAPI.SetOutputSize(tab2_n2Count);
                    nnAPI.SetLearningIterations(tab2_iterations);
                    nnAPI.SetLearningInitialRate(tab2_initialRate);
                    nnAPI.SetLearningEndingRate(tab2_endingRate);
                    nnAPI.SetLearningInitialRadius(tab2_initialRadius);
                    nnAPI.SetLearningEndingRadius(tab2_endingRadius);
                    nnAPI.SetThreshold(tab2_threshold);
                    nnAPI.SetMethod(gMethod);
                    nnAPI.CreateANN();

                    this.ReturnNN = nnAPI;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            catch (System.OutOfMemoryException ex)
            {
                MessageBox.Show("La red neuronal es demasiado grande.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cambio del metodo de generación de patrón
        private void comboBox_gMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            var senderComboBox = (ComboBox)sender;
            NeuralNetworkAPI.GenerationMethod gMethod;
            Enum.TryParse<NeuralNetworkAPI.GenerationMethod>(senderComboBox.SelectedValue.ToString(), out gMethod);

            // Desabilita el nud de threshold si el metodo es Hashing
            var hashingMethod = gMethod == NeuralNetworkAPI.GenerationMethod.Hashing;
            
            if (tabControl.SelectedIndex == 0)
            {
                if (hashingMethod)
                    nud_tab1_threshold.Enabled = false;
                else
                    nud_tab1_threshold.Enabled = true;
            }
            else if (tabControl.SelectedIndex == 1)
            {
                if (hashingMethod)
                    nud_tab2_threshold.Enabled = false;
                else
                    nud_tab2_threshold.Enabled = true;
            }
            
        }

        // Evento de cambio en el valor del slide
        private void nudThreshold_ValueChanged(object sender, EventArgs e)
        {
            var nudSender = (NumericUpDown)sender;
            if (tabControl.SelectedIndex == 0)
                tab1_threshold = (float)nudSender.Value;
            else if (tabControl.SelectedIndex == 1)
                tab2_threshold = (float)nudSender.Value;
        }

        // Evento de KeyDown en el textbox de cantidad de neuronas
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            //Ignora los ENTER para el textbox
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }
        
        // Evento de validación del textbox de cantidad de neuronas
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

        // Evento de post validación del textbox de cantidad de neuronas
        private void textBox_nCount_Validated(object sender, EventArgs e)
        {
            try
            {
                // Parsea el valor
                var textBoxSender = (TextBox)sender;
                var newValue = int.Parse(textBoxSender.Text);

                // Discrimina la pestaña
                if (tabControl.SelectedIndex == 0)
                {
                    // Cantidad de neuronas
                    tab1_nCount = newValue;
                }
                else if (tabControl.SelectedIndex == 1)
                {
                    // Cantindad de neuronas entrada
                    if (textBoxSender.Name == textBox_tab2_n1Count.Name)
                        tab2_n1Count = newValue;

                    // Cantidad de neuronas salida
                    else if (textBoxSender.Name == textBox_tab2_n2Count.Name)
                        tab2_n2Count = newValue;
                }
            }
            catch
            {
                MessageBox.Show("Ocurrio un error inesperado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_tab1_nCount.Text = tab1_nCount.ToString();
                textBox_tab2_n1Count.Text = tab2_n1Count.ToString();
                textBox_tab2_n2Count.Text = tab2_n2Count.ToString();
            }
        }

        // Evento de post validación del textbox de iteraciones (SOM)
        private void textBox_tab2_iterations_Validated(object sender, EventArgs e)
        {
            var textBoxSender = (TextBox)sender;
            try
            {
                // Parsea el valor
                tab2_iterations = int.Parse(textBoxSender.Text);
            }
            catch
            {
                MessageBox.Show("Ocurrio un error inesperado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxSender.Text = tab2_iterations.ToString();
            }
        }

        // Evento de post validación de textbox con valores flotantes
        private void textBox_tab2_initialRate_Validated(object sender, EventArgs e)
        {
            var textBoxSender = (TextBox)sender;
            try
            {
                // Parsea el valor
                tab2_initialRate = float.Parse(textBoxSender.Text);
            }
            catch
            {
                MessageBox.Show("Ocurrio un error inesperado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxSender.Text = tab2_initialRate.ToString();
            }
        }

        // Evento de post validación de textbox con valores flotantes
        private void textBox_tab2_endingRate_Validated(object sender, EventArgs e)
        {
            var textBoxSender = (TextBox)sender;
            try
            {
                // Parsea el valor
                tab2_endingRate = float.Parse(textBoxSender.Text);
            }
            catch
            {
                MessageBox.Show("Ocurrio un error inesperado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxSender.Text = tab2_endingRate.ToString();
            }
        }

        // Evento de post validación de textbox con valores flotantes
        private void textBox_tab2_initialRadius_Validated(object sender, EventArgs e)
        {
            var textBoxSender = (TextBox)sender;
            try
            {
                // Parsea el valor
                tab2_initialRadius = float.Parse(textBoxSender.Text);
            }
            catch
            {
                MessageBox.Show("Ocurrio un error inesperado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxSender.Text = tab2_initialRadius.ToString();
            }
        }

        // Evento de post validación de textbox con valores flotantes
        private void textBox_tab2_endingRadius_Validated(object sender, EventArgs e)
        {
            var textBoxSender = (TextBox)sender;
            try
            {
                // Parsea el valor
                tab2_endingRadius = float.Parse(textBoxSender.Text);
            }
            catch
            {
                MessageBox.Show("Ocurrio un error inesperado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxSender.Text = tab2_endingRadius.ToString();
            }
        }

        #endregion
    }
}
