namespace KanjiRecognizer
{
    partial class frmCreateImage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateImage));
            this.label_dim = new System.Windows.Forms.Label();
            this.textBox_width = new System.Windows.Forms.TextBox();
            this.button_cancelFRM = new System.Windows.Forms.Button();
            this.button_accept = new System.Windows.Forms.Button();
            this.errorProvider_frmCreateNN = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip_gMethod = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_height = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_frmCreateNN)).BeginInit();
            this.SuspendLayout();
            // 
            // label_dim
            // 
            resources.ApplyResources(this.label_dim, "label_dim");
            this.label_dim.Name = "label_dim";
            // 
            // textBox_width
            // 
            resources.ApplyResources(this.textBox_width, "textBox_width");
            this.textBox_width.Name = "textBox_width";
            this.textBox_width.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_dim_Validating);
            this.textBox_width.Validated += new System.EventHandler(this.textBox_dim_Validated);
            // 
            // button_cancelFRM
            // 
            this.button_cancelFRM.CausesValidation = false;
            this.button_cancelFRM.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.button_cancelFRM, "button_cancelFRM");
            this.button_cancelFRM.Name = "button_cancelFRM";
            this.button_cancelFRM.UseVisualStyleBackColor = true;
            // 
            // button_accept
            // 
            this.button_accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.button_accept, "button_accept");
            this.button_accept.Name = "button_accept";
            this.button_accept.UseVisualStyleBackColor = true;
            // 
            // errorProvider_frmCreateNN
            // 
            this.errorProvider_frmCreateNN.ContainerControl = this;
            // 
            // toolTip_gMethod
            // 
            this.toolTip_gMethod.AutoPopDelay = 32766;
            this.toolTip_gMethod.InitialDelay = 200;
            this.toolTip_gMethod.IsBalloon = true;
            this.toolTip_gMethod.ReshowDelay = 100;
            this.toolTip_gMethod.ShowAlways = true;
            this.toolTip_gMethod.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip_gMethod.ToolTipTitle = "Métodos de generación de patrones";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox_height
            // 
            resources.ApplyResources(this.textBox_height, "textBox_height");
            this.textBox_height.Name = "textBox_height";
            this.textBox_height.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_dim_Validating);
            this.textBox_height.Validated += new System.EventHandler(this.textBox_dim_Validated);
            // 
            // frmCreateImage
            // 
            this.AcceptButton = this.button_accept;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.CancelButton = this.button_cancelFRM;
            this.ControlBox = false;
            this.Controls.Add(this.textBox_height);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_accept);
            this.Controls.Add(this.button_cancelFRM);
            this.Controls.Add(this.textBox_width);
            this.Controls.Add(this.label_dim);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmCreateImage";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmCreanteImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_frmCreateNN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_dim;
        private System.Windows.Forms.TextBox textBox_width;
        private System.Windows.Forms.Button button_cancelFRM;
        private System.Windows.Forms.Button button_accept;
        private System.Windows.Forms.ErrorProvider errorProvider_frmCreateNN;
        private System.Windows.Forms.ToolTip toolTip_gMethod;
        private System.Windows.Forms.TextBox textBox_height;
        private System.Windows.Forms.Label label1;
    }
}