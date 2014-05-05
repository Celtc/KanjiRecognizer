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
    public partial class frmShowKanji : Form
    {
        //Builder
        public frmShowKanji(Kanji kanji)
        {
            InitializeComponent();
            this.showingKanji = kanji;
        }

        //Awake
        private void frmShowKanji_Load(object sender, EventArgs e)
        {
            this.Text = "Kanji: " + showingKanji.name;
            this.textBox_name.Text = showingKanji.name;
            this.richTextBox_desc.Text = showingKanji.description;
            this.pictureBox_kanji.Image = showingKanji.sourceImage;
        }

        //Variables
        private Kanji showingKanji;
    }
}
