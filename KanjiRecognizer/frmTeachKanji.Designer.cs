namespace KanjiRecognizer
{
    partial class frmTeachKanji
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
            this.label1 = new System.Windows.Forms.Label();
            this.button_accept = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.label_filename = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_name = new System.Windows.Forms.Label();
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
            this.pictureBox_kanji.Size = new System.Drawing.Size(310, 310);
            this.pictureBox_kanji.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_kanji.TabIndex = 0;
            this.pictureBox_kanji.TabStop = false;
            // 
            // richTextBox_desc
            // 
            this.richTextBox_desc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_desc.Location = new System.Drawing.Point(14, 387);
            this.richTextBox_desc.Name = "richTextBox_desc";
            this.richTextBox_desc.Size = new System.Drawing.Size(309, 80);
            this.richTextBox_desc.TabIndex = 2;
            this.richTextBox_desc.Text = "";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 371);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Descripción";
            // 
            // button_accept
            // 
            this.button_accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_accept.Location = new System.Drawing.Point(166, 483);
            this.button_accept.Name = "button_accept";
            this.button_accept.Size = new System.Drawing.Size(75, 23);
            this.button_accept.TabIndex = 4;
            this.button_accept.Text = "Enseñar";
            this.button_accept.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(247, 483);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 5;
            this.button_cancel.Text = "Cancelar";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // label_filename
            // 
            this.label_filename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_filename.Location = new System.Drawing.Point(16, 306);
            this.label_filename.Name = "label_filename";
            this.label_filename.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_filename.Size = new System.Drawing.Size(302, 13);
            this.label_filename.TabIndex = 6;
            this.label_filename.Text = "Filename";
            this.label_filename.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // textBox_name
            // 
            this.textBox_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_name.Location = new System.Drawing.Point(12, 346);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(310, 20);
            this.textBox_name.TabIndex = 1;
            // 
            // label_name
            // 
            this.label_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(16, 330);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(44, 13);
            this.label_name.TabIndex = 7;
            this.label_name.Text = "Nombre";
            // 
            // frmTeachKanji
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 518);
            this.ControlBox = false;
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.label_filename);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_accept);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox_desc);
            this.Controls.Add(this.pictureBox_kanji);
            this.MinimumSize = new System.Drawing.Size(260, 380);
            this.Name = "frmTeachKanji";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enseñar Kanji";
            this.Load += new System.EventHandler(this.frmTeachKanji_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_kanji)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_kanji;
        private System.Windows.Forms.RichTextBox richTextBox_desc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_accept;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label_filename;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label_name;
    }
}