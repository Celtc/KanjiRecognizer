namespace KanjiRecognizer
{
    partial class frmShowKanji
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
            this.pictureBox_kanji = new System.Windows.Forms.PictureBox();
            this.richTextBox_desc = new System.Windows.Forms.RichTextBox();
            this.label_desc = new System.Windows.Forms.Label();
            this.button_accept = new System.Windows.Forms.Button();
            this.label_name = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_kanji)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_kanji
            // 
            this.pictureBox_kanji.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_kanji.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_kanji.Location = new System.Drawing.Point(12, 12);
            this.pictureBox_kanji.Name = "pictureBox_kanji";
            this.pictureBox_kanji.Size = new System.Drawing.Size(309, 309);
            this.pictureBox_kanji.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_kanji.TabIndex = 0;
            this.pictureBox_kanji.TabStop = false;
            // 
            // richTextBox_desc
            // 
            this.richTextBox_desc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_desc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_desc.Location = new System.Drawing.Point(334, 66);
            this.richTextBox_desc.Name = "richTextBox_desc";
            this.richTextBox_desc.ReadOnly = true;
            this.richTextBox_desc.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox_desc.Size = new System.Drawing.Size(143, 255);
            this.richTextBox_desc.TabIndex = 2;
            this.richTextBox_desc.Text = "";
            // 
            // label_desc
            // 
            this.label_desc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_desc.AutoSize = true;
            this.label_desc.Location = new System.Drawing.Point(332, 50);
            this.label_desc.Name = "label_desc";
            this.label_desc.Size = new System.Drawing.Size(63, 13);
            this.label_desc.TabIndex = 3;
            this.label_desc.Text = "Descripción";
            // 
            // button_accept
            // 
            this.button_accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_accept.Location = new System.Drawing.Point(403, 329);
            this.button_accept.Name = "button_accept";
            this.button_accept.Size = new System.Drawing.Size(75, 23);
            this.button_accept.TabIndex = 4;
            this.button_accept.Text = "Aceptar";
            this.button_accept.UseVisualStyleBackColor = true;
            // 
            // label_name
            // 
            this.label_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(332, 12);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(44, 13);
            this.label_name.TabIndex = 5;
            this.label_name.Text = "Nombre";
            // 
            // textBox_name
            // 
            this.textBox_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_name.Location = new System.Drawing.Point(334, 28);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.ReadOnly = true;
            this.textBox_name.Size = new System.Drawing.Size(143, 20);
            this.textBox_name.TabIndex = 6;
            // 
            // frmShowKanji
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 364);
            this.ControlBox = false;
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.button_accept);
            this.Controls.Add(this.label_desc);
            this.Controls.Add(this.richTextBox_desc);
            this.Controls.Add(this.pictureBox_kanji);
            this.MinimumSize = new System.Drawing.Size(360, 260);
            this.Name = "frmShowKanji";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KanjiName";
            this.Load += new System.EventHandler(this.frmShowKanji_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_kanji)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_kanji;
        private System.Windows.Forms.RichTextBox richTextBox_desc;
        private System.Windows.Forms.Label label_desc;
        private System.Windows.Forms.Button button_accept;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.TextBox textBox_name;
    }
}