using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using KanjiRecognizer.Source;

namespace KanjiRecognizer
{
    public partial class frmAddNoise : Form
    {
        //Buidler
        public frmAddNoise(Image sourceImage)
        {
            InitializeComponent();
            this.image = sourceImage;
        }

        //Awake
        private void frmAddDistortion_Load(object sender, EventArgs e)
        {
            distortionLevel = 10f;
        }

        //Evento de cambio en el valor del slide
        private void nudDistortionLevel_ValueChanged(object sender, EventArgs e)
        {
            distortionLevel = (float) nudDistortionLevel.Value;
        }

        //Evento de Click en el btn de aceptar
        private void button_accept_Click(object sender, EventArgs e)
        {
            //Para agregar distorsion pasa la imagen a bitmap
            Bitmap bitmap = ImageAPI.AlltoBMP(image);

            //Itera tantas veces como el porcentaje de pixels a distorsionar
            int pixelX = 0;
            int pixelY = 0;
            Color pixelValue;
            int iterations = (int) distortionLevel * bitmap.Width * bitmap.Height / 100;
            for (int i = 0; i < iterations; i++)
            {
                pixelX = this.rand.Next(bitmap.Width);
                pixelY = this.rand.Next(bitmap.Height);
                pixelValue = GetRandomColour(checkBox_monochrome.Checked);
                bitmap.SetPixel(pixelX, pixelY, pixelValue);                
            }

            //Almacena el resultado
            image = bitmap;
        }

        //Genera un color random
        private Color GetRandomColour(bool monochrome)
        {
            Color resultColor;
            if (monochrome)
            {
                //Monocromo
                int value = this.rand.Next(2);
                if (value == 1)
                    resultColor = Color.Black;
                else
                    resultColor = Color.White;
            }
            else
            {
                //Color ARGB
                resultColor = Color.FromArgb(this.rand.Next(256), this.rand.Next(256), this.rand.Next(256));
            }

            return resultColor;
        }

        //Variables
        public Image image { get; private set; }
        private float distortionLevel;
        private readonly Random rand = new Random();
    }
}