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
            this.label_tab1_nCount = new System.Windows.Forms.Label();
            this.textBox_tab1_nCount = new System.Windows.Forms.TextBox();
            this.button_cancelFRM = new System.Windows.Forms.Button();
            this.button_createNN = new System.Windows.Forms.Button();
            this.errorProvider_frmCreateNN = new System.Windows.Forms.ErrorProvider(this.components);
            this.comboBox_tab1_gMethod = new System.Windows.Forms.ComboBox();
            this.label_tab1_gMethod = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label_tab1_updSequence = new System.Windows.Forms.Label();
            this.label_tab1_threshold = new System.Windows.Forms.Label();
            this.label_tab2_threshold = new System.Windows.Forms.Label();
            this.label_tab2_gMethod = new System.Windows.Forms.Label();
            this.comboBox_tab1_updSequence = new System.Windows.Forms.ComboBox();
            this.nud_tab1_threshold = new System.Windows.Forms.NumericUpDown();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_tab2_initialRadius = new System.Windows.Forms.Label();
            this.label_tab2_endingRadius = new System.Windows.Forms.Label();
            this.textBox_tab2_initialRadius = new System.Windows.Forms.TextBox();
            this.textBox_tab2_endingRadius = new System.Windows.Forms.TextBox();
            this.label_tab2_iterations = new System.Windows.Forms.Label();
            this.textBox_tab2_iterations = new System.Windows.Forms.TextBox();
            this.label_tab2_initialRate = new System.Windows.Forms.Label();
            this.label_tab2_endingRate = new System.Windows.Forms.Label();
            this.textBox_tab2_initialRate = new System.Windows.Forms.TextBox();
            this.textBox_tab2_endingRate = new System.Windows.Forms.TextBox();
            this.label_tab2_n2Count = new System.Windows.Forms.Label();
            this.textBox_tab2_n2Count = new System.Windows.Forms.TextBox();
            this.label_tab2_n1Count = new System.Windows.Forms.Label();
            this.nud_tab2_threshold = new System.Windows.Forms.NumericUpDown();
            this.textBox_tab2_n1Count = new System.Windows.Forms.TextBox();
            this.comboBox_tab2_gMethod = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_frmCreateNN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tab1_threshold)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tab2_threshold)).BeginInit();
            this.SuspendLayout();
            // 
            // label_tab1_nCount
            // 
            resources.ApplyResources(this.label_tab1_nCount, "label_tab1_nCount");
            this.label_tab1_nCount.Name = "label_tab1_nCount";
            // 
            // textBox_tab1_nCount
            // 
            resources.ApplyResources(this.textBox_tab1_nCount, "textBox_tab1_nCount");
            this.textBox_tab1_nCount.Name = "textBox_tab1_nCount";
            this.textBox_tab1_nCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox_tab1_nCount.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_nCount_Validating);
            this.textBox_tab1_nCount.Validated += new System.EventHandler(this.textBox_nCount_Validated);
            // 
            // button_cancelFRM
            // 
            resources.ApplyResources(this.button_cancelFRM, "button_cancelFRM");
            this.button_cancelFRM.CausesValidation = false;
            this.button_cancelFRM.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
            // comboBox_tab1_gMethod
            // 
            this.comboBox_tab1_gMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tab1_gMethod.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_tab1_gMethod, "comboBox_tab1_gMethod");
            this.comboBox_tab1_gMethod.Name = "comboBox_tab1_gMethod";
            this.comboBox_tab1_gMethod.SelectedIndexChanged += new System.EventHandler(this.comboBox_gMethod_SelectedIndexChanged);
            // 
            // label_tab1_gMethod
            // 
            resources.ApplyResources(this.label_tab1_gMethod, "label_tab1_gMethod");
            this.label_tab1_gMethod.Name = "label_tab1_gMethod";
            this.toolTip.SetToolTip(this.label_tab1_gMethod, resources.GetString("label_tab1_gMethod.ToolTip"));
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 32766;
            this.toolTip.InitialDelay = 200;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ShowAlways = true;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Información";
            // 
            // label_tab1_updSequence
            // 
            resources.ApplyResources(this.label_tab1_updSequence, "label_tab1_updSequence");
            this.label_tab1_updSequence.Name = "label_tab1_updSequence";
            this.toolTip.SetToolTip(this.label_tab1_updSequence, resources.GetString("label_tab1_updSequence.ToolTip"));
            // 
            // label_tab1_threshold
            // 
            resources.ApplyResources(this.label_tab1_threshold, "label_tab1_threshold");
            this.label_tab1_threshold.Name = "label_tab1_threshold";
            this.toolTip.SetToolTip(this.label_tab1_threshold, resources.GetString("label_tab1_threshold.ToolTip"));
            // 
            // label_tab2_threshold
            // 
            resources.ApplyResources(this.label_tab2_threshold, "label_tab2_threshold");
            this.label_tab2_threshold.Name = "label_tab2_threshold";
            this.toolTip.SetToolTip(this.label_tab2_threshold, resources.GetString("label_tab2_threshold.ToolTip"));
            // 
            // label_tab2_gMethod
            // 
            resources.ApplyResources(this.label_tab2_gMethod, "label_tab2_gMethod");
            this.label_tab2_gMethod.Name = "label_tab2_gMethod";
            this.toolTip.SetToolTip(this.label_tab2_gMethod, resources.GetString("label_tab2_gMethod.ToolTip"));
            // 
            // comboBox_tab1_updSequence
            // 
            this.comboBox_tab1_updSequence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tab1_updSequence.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_tab1_updSequence, "comboBox_tab1_updSequence");
            this.comboBox_tab1_updSequence.Name = "comboBox_tab1_updSequence";
            // 
            // nud_tab1_threshold
            // 
            this.nud_tab1_threshold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nud_tab1_threshold.DecimalPlaces = 2;
            this.nud_tab1_threshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            resources.ApplyResources(this.nud_tab1_threshold, "nud_tab1_threshold");
            this.nud_tab1_threshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_tab1_threshold.Name = "nud_tab1_threshold";
            this.nud_tab1_threshold.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            this.nud_tab1_threshold.ValueChanged += new System.EventHandler(this.nudThreshold_ValueChanged);
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label_tab1_nCount);
            this.tabPage1.Controls.Add(this.nud_tab1_threshold);
            this.tabPage1.Controls.Add(this.textBox_tab1_nCount);
            this.tabPage1.Controls.Add(this.label_tab1_threshold);
            this.tabPage1.Controls.Add(this.label_tab1_updSequence);
            this.tabPage1.Controls.Add(this.comboBox_tab1_updSequence);
            this.tabPage1.Controls.Add(this.comboBox_tab1_gMethod);
            this.tabPage1.Controls.Add(this.label_tab1_gMethod);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.label_tab2_n2Count);
            this.tabPage2.Controls.Add(this.textBox_tab2_n2Count);
            this.tabPage2.Controls.Add(this.label_tab2_n1Count);
            this.tabPage2.Controls.Add(this.nud_tab2_threshold);
            this.tabPage2.Controls.Add(this.textBox_tab2_n1Count);
            this.tabPage2.Controls.Add(this.label_tab2_threshold);
            this.tabPage2.Controls.Add(this.comboBox_tab2_gMethod);
            this.tabPage2.Controls.Add(this.label_tab2_gMethod);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_tab2_initialRadius);
            this.groupBox1.Controls.Add(this.label_tab2_endingRadius);
            this.groupBox1.Controls.Add(this.textBox_tab2_initialRadius);
            this.groupBox1.Controls.Add(this.textBox_tab2_endingRadius);
            this.groupBox1.Controls.Add(this.label_tab2_iterations);
            this.groupBox1.Controls.Add(this.textBox_tab2_iterations);
            this.groupBox1.Controls.Add(this.label_tab2_initialRate);
            this.groupBox1.Controls.Add(this.label_tab2_endingRate);
            this.groupBox1.Controls.Add(this.textBox_tab2_initialRate);
            this.groupBox1.Controls.Add(this.textBox_tab2_endingRate);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label_tab2_initialRadius
            // 
            resources.ApplyResources(this.label_tab2_initialRadius, "label_tab2_initialRadius");
            this.label_tab2_initialRadius.Name = "label_tab2_initialRadius";
            // 
            // label_tab2_endingRadius
            // 
            resources.ApplyResources(this.label_tab2_endingRadius, "label_tab2_endingRadius");
            this.label_tab2_endingRadius.Name = "label_tab2_endingRadius";
            // 
            // textBox_tab2_initialRadius
            // 
            resources.ApplyResources(this.textBox_tab2_initialRadius, "textBox_tab2_initialRadius");
            this.textBox_tab2_initialRadius.Name = "textBox_tab2_initialRadius";
            this.textBox_tab2_initialRadius.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox_tab2_initialRadius.Validated += new System.EventHandler(this.textBox_tab2_initialRadius_Validated);
            // 
            // textBox_tab2_endingRadius
            // 
            resources.ApplyResources(this.textBox_tab2_endingRadius, "textBox_tab2_endingRadius");
            this.textBox_tab2_endingRadius.Name = "textBox_tab2_endingRadius";
            this.textBox_tab2_endingRadius.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox_tab2_endingRadius.Validated += new System.EventHandler(this.textBox_tab2_endingRadius_Validated);
            // 
            // label_tab2_iterations
            // 
            resources.ApplyResources(this.label_tab2_iterations, "label_tab2_iterations");
            this.label_tab2_iterations.Name = "label_tab2_iterations";
            // 
            // textBox_tab2_iterations
            // 
            resources.ApplyResources(this.textBox_tab2_iterations, "textBox_tab2_iterations");
            this.textBox_tab2_iterations.Name = "textBox_tab2_iterations";
            this.textBox_tab2_iterations.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox_tab2_iterations.Validated += new System.EventHandler(this.textBox_tab2_iterations_Validated);
            // 
            // label_tab2_initialRate
            // 
            resources.ApplyResources(this.label_tab2_initialRate, "label_tab2_initialRate");
            this.label_tab2_initialRate.Name = "label_tab2_initialRate";
            // 
            // label_tab2_endingRate
            // 
            resources.ApplyResources(this.label_tab2_endingRate, "label_tab2_endingRate");
            this.label_tab2_endingRate.Name = "label_tab2_endingRate";
            // 
            // textBox_tab2_initialRate
            // 
            resources.ApplyResources(this.textBox_tab2_initialRate, "textBox_tab2_initialRate");
            this.textBox_tab2_initialRate.Name = "textBox_tab2_initialRate";
            this.textBox_tab2_initialRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox_tab2_initialRate.Validated += new System.EventHandler(this.textBox_tab2_initialRate_Validated);
            // 
            // textBox_tab2_endingRate
            // 
            resources.ApplyResources(this.textBox_tab2_endingRate, "textBox_tab2_endingRate");
            this.textBox_tab2_endingRate.Name = "textBox_tab2_endingRate";
            this.textBox_tab2_endingRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox_tab2_endingRate.Validated += new System.EventHandler(this.textBox_tab2_endingRate_Validated);
            // 
            // label_tab2_n2Count
            // 
            resources.ApplyResources(this.label_tab2_n2Count, "label_tab2_n2Count");
            this.label_tab2_n2Count.Name = "label_tab2_n2Count";
            // 
            // textBox_tab2_n2Count
            // 
            resources.ApplyResources(this.textBox_tab2_n2Count, "textBox_tab2_n2Count");
            this.textBox_tab2_n2Count.Name = "textBox_tab2_n2Count";
            this.textBox_tab2_n2Count.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox_tab2_n2Count.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_nCount_Validating);
            this.textBox_tab2_n2Count.Validated += new System.EventHandler(this.textBox_nCount_Validated);
            // 
            // label_tab2_n1Count
            // 
            resources.ApplyResources(this.label_tab2_n1Count, "label_tab2_n1Count");
            this.label_tab2_n1Count.Name = "label_tab2_n1Count";
            // 
            // nud_tab2_threshold
            // 
            this.nud_tab2_threshold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nud_tab2_threshold.DecimalPlaces = 2;
            this.nud_tab2_threshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            resources.ApplyResources(this.nud_tab2_threshold, "nud_tab2_threshold");
            this.nud_tab2_threshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_tab2_threshold.Name = "nud_tab2_threshold";
            this.nud_tab2_threshold.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            this.nud_tab2_threshold.ValueChanged += new System.EventHandler(this.nudThreshold_ValueChanged);
            // 
            // textBox_tab2_n1Count
            // 
            resources.ApplyResources(this.textBox_tab2_n1Count, "textBox_tab2_n1Count");
            this.textBox_tab2_n1Count.Name = "textBox_tab2_n1Count";
            this.textBox_tab2_n1Count.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox_tab2_n1Count.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_nCount_Validating);
            this.textBox_tab2_n1Count.Validated += new System.EventHandler(this.textBox_nCount_Validated);
            // 
            // comboBox_tab2_gMethod
            // 
            this.comboBox_tab2_gMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tab2_gMethod.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_tab2_gMethod, "comboBox_tab2_gMethod");
            this.comboBox_tab2_gMethod.Name = "comboBox_tab2_gMethod";
            this.comboBox_tab2_gMethod.SelectedIndexChanged += new System.EventHandler(this.comboBox_gMethod_SelectedIndexChanged);
            // 
            // frmCreanteNN
            // 
            this.AcceptButton = this.button_createNN;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.CancelButton = this.button_cancelFRM;
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.button_createNN);
            this.Controls.Add(this.button_cancelFRM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmCreanteNN";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmCreanteNN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_frmCreateNN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tab1_threshold)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tab2_threshold)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_tab1_nCount;
        private System.Windows.Forms.TextBox textBox_tab1_nCount;
        private System.Windows.Forms.Button button_cancelFRM;
        private System.Windows.Forms.Button button_createNN;
        private System.Windows.Forms.ErrorProvider errorProvider_frmCreateNN;
        private System.Windows.Forms.Label label_tab1_gMethod;
        private System.Windows.Forms.ComboBox comboBox_tab1_gMethod;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label_tab1_updSequence;
        private System.Windows.Forms.ComboBox comboBox_tab1_updSequence;
        private System.Windows.Forms.Label label_tab1_threshold;
        private System.Windows.Forms.NumericUpDown nud_tab1_threshold;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label_tab2_endingRate;
        private System.Windows.Forms.TextBox textBox_tab2_endingRate;
        private System.Windows.Forms.Label label_tab2_initialRate;
        private System.Windows.Forms.TextBox textBox_tab2_initialRate;
        private System.Windows.Forms.Label label_tab2_n2Count;
        private System.Windows.Forms.TextBox textBox_tab2_n2Count;
        private System.Windows.Forms.Label label_tab2_n1Count;
        private System.Windows.Forms.NumericUpDown nud_tab2_threshold;
        private System.Windows.Forms.TextBox textBox_tab2_n1Count;
        private System.Windows.Forms.Label label_tab2_threshold;
        private System.Windows.Forms.ComboBox comboBox_tab2_gMethod;
        private System.Windows.Forms.Label label_tab2_gMethod;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_tab2_initialRadius;
        private System.Windows.Forms.Label label_tab2_endingRadius;
        private System.Windows.Forms.TextBox textBox_tab2_initialRadius;
        private System.Windows.Forms.TextBox textBox_tab2_endingRadius;
        private System.Windows.Forms.Label label_tab2_iterations;
        private System.Windows.Forms.TextBox textBox_tab2_iterations;
    }
}