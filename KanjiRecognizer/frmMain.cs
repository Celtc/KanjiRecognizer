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
        #region Variables (privadas)

        private Image currentImage;
        private NeuralNetworkAPI nnAPI;

        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Constructor
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Metodos Privados
        
        // Awake
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.updateDisplayData();
        }

        // Crea una nueva instancia de red neuronal
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

        // Actualiza la info en el form
        private void updateDisplayData()
        {
            if (nnAPI != null && nnAPI.IsNetworkCreated)
            {
                // Datos de la RNA
                this.label_NN_state_data.Text = "Activa";
                this.label_NN_modelName_data.Text = nnAPI.ModelName;
                this.label_NN_nCount_data.Text = nnAPI.NeuronsCount.ToString();
                this.label_NN_patterns_data.Text = nnAPI.PatternsLearned.Count.ToString();

                // Datos de configuracion gral
                this.label_conf_method_data.Text = Enum.GetName(typeof(NeuralNetworkAPI.GenerationMethod), nnAPI.Method);
                this.label_conf_threshold_data.Text = nnAPI.Threshold.ToString();
            }
            else
            {
                // Datos de la RNA
                this.label_NN_state_data.Text = "Inexistente";
                this.label_NN_modelName_data.Text = "-";
                this.label_NN_nCount_data.Text = "-";
                this.label_NN_patterns_data.Text = "-";

                // Datos de configuracion gral
                this.label_conf_method_data.Text = "-";
                this.label_conf_threshold_data.Text = "-";
            }
        }

        // Carga una imagen que va a ser usada para reconocer
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

        // UI para enseñar un kanji
        private void teachKanjiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Pide la imagen a enseñar
            if (openMultipleFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lista de kanjis
                var kanjisToLearn = new List<Kanji>();

                // Carga la/s imagen y pide datos extras
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

                    // Pide los datos adicionales
                    using (var teachKanjiForm = new frmTeachKanji(sourceImage, Path.GetFileName(filepath)))
                    {
                        var result = teachKanjiForm.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            // Genera el kanji y lo agrega a la lista de kanjis a aprender
                            kanjisToLearn.Add(new Kanji(sourceImage, teachKanjiForm.name, teachKanjiForm.description));
                        }
                    }
                }

                // Aprende los kanjis
                try
                {
                    nnAPI.TeachKanjis(kanjisToLearn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al intenar aprender los kanjis.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Actualiza el form
                this.updateDisplayData();
            }
        }

        // Analiza e intenta reconocer la imagen
        private void button_runDynamics_Click(object sender, EventArgs e)
        {
            // Verifica la imagen a analizar
            if (currentImage == null)
            {
                MessageBox.Show("Cargue una imagen a reconocer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verifica la existencia de una red neuronal
            if (nnAPI == null || !nnAPI.IsNetworkCreated)
            {
                MessageBox.Show("Cree una red neuronal para poder analizar la imagen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Analiza el kanji
            Kanji result;
            Bitmap resultBitmap;
            result = nnAPI.RecognizeKanji(currentImage, out resultBitmap);

            // Muestra el resultado si existe, sino notifica
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

            // Actualiza el form
            this.updateDisplayData();
        }

        // Agrega ruido a la imgen
        private void button_addNoise_Click(object sender, EventArgs e)
        {
            // Verifica la imagen distorsionar
            if (currentImage == null)
            {
                MessageBox.Show("No hay ninguna imagen cargada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

        // Abre el editor gráfico
        private void button_edit_Click(object sender, EventArgs e)
        {
            using (var editKanjiForm = new frmEditKanji(currentImage))
            {
                var result = editKanjiForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    currentImage = editKanjiForm.image;
                    pictureBox_loadedImage.Image = currentImage;
                }
            }
        }

        #endregion
    }
}
