namespace KanjiRecognizer
{
    partial class frmAddNoise
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
            this.nudDistortionLevel = new System.Windows.Forms.NumericUpDown();
            this.lblPercent = new System.Windows.Forms.Label();
            this.button_accept = new System.Windows.Forms.Button();
            this.lblDistortionLevelSelectOffer = new System.Windows.Forms.Label();
            this.button_cancel = new System.Windows.Forms.Button();
            this.checkBox_monochrome = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistortionLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // nudDistortionLevel
            // 
            this.nudDistortionLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudDistortionLevel.Location = new System.Drawing.Point(65, 31);
            this.nudDistortionLevel.Name = "nudDistortionLevel";
            this.nudDistortionLevel.Size = new System.Drawing.Size(73, 20);
            this.nudDistortionLevel.TabIndex = 0;
            this.nudDistortionLevel.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDistortionLevel.ValueChanged += new System.EventHandler(this.nudDistortionLevel_ValueChanged);
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(144, 33);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(15, 13);
            this.lblPercent.TabIndex = 1;
            this.lblPercent.Text = "%";
            // 
            // button_accept
            // 
            this.button_accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_accept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_accept.Location = new System.Drawing.Point(10, 79);
            this.button_accept.Name = "button_accept";
            this.button_accept.Size = new System.Drawing.Size(95, 23);
            this.button_accept.TabIndex = 2;
            this.button_accept.Text = "Aceptar";
            this.button_accept.UseVisualStyleBackColor = true;
            this.button_accept.Click += new System.EventHandler(this.button_accept_Click);
            // 
            // lblDistortionLevelSelectOffer
            // 
            this.lblDistortionLevelSelectOffer.AutoSize = true;
            this.lblDistortionLevelSelectOffer.Location = new System.Drawing.Point(7, 9);
            this.lblDistortionLevelSelectOffer.Name = "lblDistortionLevelSelectOffer";
            this.lblDistortionLevelSelectOffer.Size = new System.Drawing.Size(211, 13);
            this.lblDistortionLevelSelectOffer.TabIndex = 1;
            this.lblDistortionLevelSelectOffer.Text = "Defina el porcentaje de distorsión por ruido:";
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.Location = new System.Drawing.Point(117, 79);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(95, 23);
            this.button_cancel.TabIndex = 3;
            this.button_cancel.Text = "Cancelar";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox_monochrome.AutoSize = true;
            this.checkBox_monochrome.Checked = true;
            this.checkBox_monochrome.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_monochrome.Location = new System.Drawing.Point(10, 56);
            this.checkBox_monochrome.Name = "checkBox1";
            this.checkBox_monochrome.Size = new System.Drawing.Size(112, 17);
            this.checkBox_monochrome.TabIndex = 4;
            this.checkBox_monochrome.Text = "Ruido monocromo";
            this.checkBox_monochrome.UseVisualStyleBackColor = true;
            // 
            // frmAddNoise
            // 
            this.AcceptButton = this.button_accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(224, 109);
            this.Controls.Add(this.checkBox_monochrome);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_accept);
            this.Controls.Add(this.lblDistortionLevelSelectOffer);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.nudDistortionLevel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddNoise";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nivel de Distorsión";
            this.Load += new System.EventHandler(this.frmAddDistortion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDistortionLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudDistortionLevel;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Button button_accept;
        private System.Windows.Forms.Label lblDistortionLevelSelectOffer;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.CheckBox checkBox_monochrome;
    }
}