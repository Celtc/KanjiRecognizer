namespace KanjiRecognizer
{
    partial class frmCreanteNN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreanteNN));
            this.label_nCount = new System.Windows.Forms.Label();
            this.textBox_nCount = new System.Windows.Forms.TextBox();
            this.button_cancelFRM = new System.Windows.Forms.Button();
            this.button_createNN = new System.Windows.Forms.Button();
            this.errorProvider_frmCreateNN = new System.Windows.Forms.ErrorProvider(this.components);
            this.comboBox_gMethod = new System.Windows.Forms.ComboBox();
            this.label_gMethod = new System.Windows.Forms.Label();
            this.toolTip_gMethod = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_frmCreateNN)).BeginInit();
            this.SuspendLayout();
            // 
            // label_nCount
            // 
            resources.ApplyResources(this.label_nCount, "label_nCount");
            this.label_nCount.Name = "label_nCount";
            // 
            // textBox_nCount
            // 
            resources.ApplyResources(this.textBox_nCount, "textBox_nCount");
            this.textBox_nCount.Name = "textBox_nCount";
            this.textBox_nCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_nCount_KeyDown);
            this.textBox_nCount.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_nCount_Validating);
            this.textBox_nCount.Validated += new System.EventHandler(this.textBox_nCount_Validated);
            // 
            // button_cancelFRM
            // 
            this.button_cancelFRM.CausesValidation = false;
            this.button_cancelFRM.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.button_cancelFRM, "button_cancelFRM");
            this.button_cancelFRM.Name = "button_cancelFRM";
            this.button_cancelFRM.UseVisualStyleBackColor = true;
            // 
            // button_createNN
            // 
            resources.ApplyResources(this.button_createNN, "button_createNN");
            this.button_createNN.Name = "button_createNN";
            this.button_createNN.UseVisualStyleBackColor = true;
            this.button_createNN.Click += new System.EventHandler(this.button_createNN_Click);
            // 
            // errorProvider_frmCreateNN
            // 
            this.errorProvider_frmCreateNN.ContainerControl = this;
            // 
            // comboBox_gMethod
            // 
            this.comboBox_gMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_gMethod.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_gMethod, "comboBox_gMethod");
            this.comboBox_gMethod.Name = "comboBox_gMethod";
            // 
            // label_gMethod
            // 
            resources.ApplyResources(this.label_gMethod, "label_gMethod");
            this.label_gMethod.Name = "label_gMethod";
            this.toolTip_gMethod.SetToolTip(this.label_gMethod, resources.GetString("label_gMethod.ToolTip"));
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
            // frmCreanteNN
            // 
            this.AcceptButton = this.button_createNN;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.CancelButton = this.button_cancelFRM;
            this.ControlBox = false;
            this.Controls.Add(this.label_gMethod);
            this.Controls.Add(this.comboBox_gMethod);
            this.Controls.Add(this.button_createNN);
            this.Controls.Add(this.button_cancelFRM);
            this.Controls.Add(this.textBox_nCount);
            this.Controls.Add(this.label_nCount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmCreanteNN";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmCreanteNN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_frmCreateNN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_nCount;
        private System.Windows.Forms.TextBox textBox_nCount;
        private System.Windows.Forms.Button button_cancelFRM;
        private System.Windows.Forms.Button button_createNN;
        private System.Windows.Forms.ErrorProvider errorProvider_frmCreateNN;
        private System.Windows.Forms.Label label_gMethod;
        private System.Windows.Forms.ComboBox comboBox_gMethod;
        private System.Windows.Forms.ToolTip toolTip_gMethod;
    }
}