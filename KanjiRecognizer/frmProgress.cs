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
    public partial class frmProgress : Form
    {
        #region Variables (privadas)

        private NeuralNetworkAPI nnAPI;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nnAPI">Red a monitorear</param>
        /// <param name="taskName">Nombre de la tarea a monitorear</param>
        public frmProgress(NeuralNetworkAPI nnAPI, string taskName = "Progreso")
        {
            InitializeComponent();

            this.Text = taskName;
            this.nnAPI = nnAPI;
            this.nnAPI.RegisterProgressChangedDel(progressChangedDelegate);
        }

        /// <summary>
        /// Cambio de progreso
        /// </summary>
        /// <param name="progress">Nuevo valor de progreso</param>
        private void progressChangedDelegate(float progress)
        {
            try
            {
                progressBar.Value = (int)progress;
                if (progress == 100f)
                {
                    // Limpia el delegado
                    nnAPI.ClearProgressChangedDel();

                    // Revisa si hubo error
                    if (nnAPI.Error != null)
                    {
                        MessageBox.Show("Hubo un error al intenar aprender los kanjis.\n" + nnAPI.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        // Establece el resultado
                        DialogResult = System.Windows.Forms.DialogResult.Abort;
                    }
                    else
                    {
                        // Establece el resultado
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }

                    // Sale
                    Close();
                }
            }
            catch (Exception e)
            {
                // Limpia el delegado
                nnAPI.ClearProgressChangedDel();

                // Establece el resultado
                DialogResult = System.Windows.Forms.DialogResult.Abort;

                // Muestra el error
                MessageBox.Show("Hubo un error al intenar aprender los kanjis.\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Se cancela la tarea
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_cancel_Click(object sender, EventArgs e)
        {
            nnAPI.Cancel();
            nnAPI.ClearProgressChangedDel();
        }

        /// <summary>
        /// Evento de KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void event_KeyDown(object sender, KeyEventArgs e)
        {
            //Ignora los ENTER
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
    
}
