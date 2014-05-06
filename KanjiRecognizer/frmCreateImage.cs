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
    public partial class frmCreateImage : Form
    {
        //Builder
        public frmCreateImage()
        {
            InitializeComponent();
        }
        
        //Awake
        private void frmCreanteImage_Load(object sender, EventArgs e)
        {
            this.width = 400;
            this.height = 400;
            this.textBox_width.Text = "400";
            this.textBox_height.Text = "400";
        }
        
        //Evento de validación del textbox de cantidad de neuronas
        private void textBox_dim_Validating(object sender, CancelEventArgs e)
        {
            bool error = false;

            //Valida el vacio
            var txtBox = sender as TextBox;
            if (txtBox.Text == string.Empty)
            {
                txtBox.Text = "0";
                return;
            }

            //Valida el tipo de dato
            int number;
            string errorMsg = string.Empty;
            if (!int.TryParse(txtBox.Text, out number))
            {
                error = true;
                errorMsg = "El valor no es un entero.";
            }
            else if (number < 1)
            {
                error = true;
                errorMsg = "El valor debe ser mayor a 0.";
            }

            //Muestra el resultado de las validaciones
            if (error)
            {
                e.Cancel = true;
                txtBox.Select(0, txtBox.Text.Length);
                this.errorProvider_frmCreateNN.SetError(txtBox, errorMsg);
            }

            return;
        }

        //Evento de post validación del textbox de cantidad de neuronas
        private void textBox_dim_Validated(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            try
            {
                //Parsea el valor
                var newValue = txtBox.Text;
                if (txtBox.Name == "textBox_width")
                    width = int.Parse(newValue);
                else if (txtBox.Name == "textBox_height")
                    height = int.Parse(newValue);
            }
            catch
            {
                MessageBox.Show("Ocurrio un error inesperado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            }
        }

        //Variables
        public int width { get; private set; }
        public int height { get; private set; }
    }
}
