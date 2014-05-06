using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using KanjiRecognizer.Source;

namespace KanjiRecognizer
{
    public partial class frmMain : Form
    {
        //Builder
        public frmMain()
        {
            InitializeComponent();
        }

        //Awake
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.updateDisplayData();
        }

        //Crea una nueva instancia de red neuronal
        private void crearNuevaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var createNNForm = new frmCreanteNN())
            {
                var result = createNNForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.nnAPI = createNNForm.ReturnNN;
                    this.updateDisplayData();
                    this.patronesToolStripMenuItem.Enabled = true;
                }
            }            
        }

        //Actualiza la info en el form
        private void updateDisplayData()
        {
            if (nnAPI != null && nnAPI.NeuralNetwork != null)
            {
                this.label_NN_state_data.Text = "Activa";
                this.label_NN_energy_data.Text = nnAPI.NeuralNetwork.Energy.ToString();
                this.label_NN_nCount_data.Text = nnAPI.NeuralNetwork.NeuronsCount.ToString();
                this.label_NN_patterns_data.Text = nnAPI.NeuralNetwork.PatternsCount.ToString();
            }
            else
            {
                this.label_NN_state_data.Text = "Inexistente";
                this.label_NN_energy_data.Text = "-";
                this.label_NN_nCount_data.Text = "-";
                this.label_NN_patterns_data.Text = "-";
            }
        }

        //Carga una imagen que va a ser usada para reconocer
        private void button_loadImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    currentImage = Image.FromFile(openFileDialog.FileName);
                    pictureBox_loadedImage.Image = currentImage;
                }
                catch
                {
                    MessageBox.Show("La imagen no es válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }     
        }

        //Metodo para enseñar un kanji
        private void teachKanjiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Pide la imagen a enseñar
            if (openMultipleFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Carga la/s imagen
                foreach (string filepath in openMultipleFileDialog.FileNames)
                {
                    Image sourceImage;
                    try
                    {
                        sourceImage = Image.FromFile(filepath);
                    }
                    catch
                    {
                        MessageBox.Show("La imagen no es válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //Pide los datos adicionales
                    using (var teachKanjiForm = new frmTeachKanji(sourceImage, Path.GetFileName(filepath)))
                    {
                        var result = teachKanjiForm.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            //Genera el kanji y lo enseña a la base
                            Kanji newKanji = new Kanji(sourceImage, teachKanjiForm.name, teachKanjiForm.description);
                            try
                            {
                                nnAPI.TeachKanji(newKanji);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("No se pudo agregar el kanji.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

                //Actualiza el form
                this.updateDisplayData();
            }
        }

        //Analiza e intenta reconocer la imagen
        private void button_runDynamics_Click(object sender, EventArgs e)
        {
            //Verifica la imagen a analizar
            if (currentImage == null)
            {
                MessageBox.Show("Cargue una imagen a reconocer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Verifica la existencia de una red neuronal
            if (nnAPI == null || nnAPI.NeuralNetwork == null)
            {
                MessageBox.Show("Cree una red neuronal para poder analizar la imagen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Analiza el kanji
            Kanji result;
            Bitmap resultBitmap;
            int iterations = (int) nudIterations.Value;
            result = nnAPI.RecognizeKanji(currentImage, iterations, out resultBitmap);

            //Muestra el resultado si existe, sino notifica
            if (result != null)
            {
                using (var showKanjiForm = new frmShowKanji(result))
                    showKanjiForm.ShowDialog();
            }
            else
            {
                using (var failRecogForm = new frmFailRecog(resultBitmap))
                    failRecogForm.ShowDialog();
            }

            //Actualiza el form
            this.updateDisplayData();
        }

        //Agrega ruido a la imgen
        private void button_addNoise_Click(object sender, EventArgs e)
        {
            using (var addNoiseForm = new frmAddNoise(currentImage))
            {
                var result = addNoiseForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    currentImage = addNoiseForm.image;
                    pictureBox_loadedImage.Image = currentImage;
                }
            }
        }

        //Variables
        private Image currentImage;
        private NeuralNetworkAPI nnAPI;
    }
}
