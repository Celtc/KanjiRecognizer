namespace KanjiRecognizer
{
    partial class frmFailRecog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFailRecog));
            this.button_toggleResult = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox_icon = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.imNNState = new ImageMagnifier.ImageMagnifier();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // button_toggleResult
            // 
            this.button_toggleResult.Location = new System.Drawing.Point(70, 82);
            this.button_toggleResult.Name = "button_toggleResult";
            this.button_toggleResult.Size = new System.Drawing.Size(75, 23);
            this.button_toggleResult.TabIndex = 1;
            this.button_toggleResult.Text = "Mostrar";
            this.button_toggleResult.UseVisualStyleBackColor = true;
            this.button_toggleResult.Click += new System.EventHandler(this.button_toggleResult_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Resultado";
            // 
            // pictureBox_icon
            // 
            this.pictureBox_icon.Location = new System.Drawing.Point(13, 15);
            this.pictureBox_icon.Name = "pictureBox_icon";
            this.pictureBox_icon.Size = new System.Drawing.Size(50, 50);
            this.pictureBox_icon.TabIndex = 3;
            this.pictureBox_icon.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(70, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(201, 58);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "No se pudo reconocer la imagen. Intente con más neuronas, menos patrones, o image" +
                "nes que presenten mayor diferencia entre ellas.";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(187, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // imNNState
            // 
            this.imNNState.ImageToMagnify = ((System.Drawing.Image)(resources.GetObject("imNNState.ImageToMagnify")));
            this.imNNState.Location = new System.Drawing.Point(13, 119);
            this.imNNState.MagnificationCoefficient = 20;
            this.imNNState.MaximumSize = new System.Drawing.Size(250, 250);
            this.imNNState.Name = "imNNState";
            this.imNNState.Size = new System.Drawing.Size(200, 200);
            this.imNNState.TabIndex = 5;
            this.imNNState.Text = "imNNState";
            this.imNNState.Visible = false;
            // 
            // frmFailRecog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 112);
            this.Controls.Add(this.imNNState);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox_icon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_toggleResult);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(280, 140);
            this.Name = "frmFailRecog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kanji No Reconocido";
            this.Load += new System.EventHandler(this.frmFailRecog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_toggleResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox_icon;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private ImageMagnifier.ImageMagnifier imNNState;
    }
}