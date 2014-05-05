using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace KanjiRecognizer
{
    public partial class frmTeachKanji : Form
    {
        //Builder
        public frmTeachKanji(Image sourceImage, string filename)
        {
            InitializeComponent();
            this.sourceImg = sourceImage;
            this.filename = filename;
        }

        //Awake
        private void frmTeachKanji_Load(object sender, EventArgs e)
        {
            this.label_filename.Text = filename;
            this.pictureBox_kanji.Image = sourceImg;
            this.textBox_name.Text = Path.GetFileNameWithoutExtension(filename);
            this.textBox_name.Select(0, this.textBox_name.Text.Length);
        }

        //Getter de la descripción
        public string description
        {
            get { return this.richTextBox_desc.Text; }
        }

        //Getter del nombre
        public string name
        {
            get { return this.textBox_name.Text; }
        }

        //Variables
        private Image sourceImg;
        private string filename;
    }
}
