using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KanjiRecognizer
{
    public partial class frmFailRecog : Form
    {
        public frmFailRecog(Bitmap result)
        {
            InitializeComponent();
            resultBmp = result;
            showResult = false;
        }
        
        private void frmFailRecog_Load(object sender, EventArgs e)
        {
            imNNState.ImageToMagnify = resultBmp;
            imNNState.MagnificationCoefficient = Math.Max((int)(250 / resultBmp.Width), 1);
            imNNState.Visible = false;
            imNNState.Invalidate();

            this.pictureBox_icon.Image = SystemIcons.Exclamation.ToBitmap();
        }

        private void button_toggleResult_Click(object sender, EventArgs e)
        {
            showResult = !showResult;
            if (showResult)
            {
                //Mostrando
                this.Height += 266;
                imNNState.Visible = true;
                this.button_toggleResult.Text = "Ocultar";                
            }
            else
            {
                //Ocultado
                this.Height -= 266;
                imNNState.Visible = false;
                this.button_toggleResult.Text = "Mostrar";
            }
        }

        private Bitmap resultBmp;
        private bool showResult;

    }
}
