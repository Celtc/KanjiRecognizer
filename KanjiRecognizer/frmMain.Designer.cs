namespace KanjiRecognizer
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox_NN_Info = new System.Windows.Forms.GroupBox();
            this.label_NN_state = new System.Windows.Forms.Label();
            this.label_NN_patterns_data = new System.Windows.Forms.Label();
            this.label_NN_nCount_data = new System.Windows.Forms.Label();
            this.label_NN_modelName_data = new System.Windows.Forms.Label();
            this.label_NN_state_data = new System.Windows.Forms.Label();
            this.label_NN_patterns = new System.Windows.Forms.Label();
            this.label_NN_nCount = new System.Windows.Forms.Label();
            this.label_NN_modelName = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.redNeuronalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarANNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patronesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teachKanjiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLearnedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox_loadedImage = new System.Windows.Forms.PictureBox();
            this.button_loadImage = new System.Windows.Forms.Button();
            this.button_runDynamics = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox_loadedKanji = new System.Windows.Forms.GroupBox();
            this.button_edit = new System.Windows.Forms.Button();
            this.button_addNoise = new System.Windows.Forms.Button();
            this.openMultipleFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_conf_method = new System.Windows.Forms.Label();
            this.label_conf_threshold_data = new System.Windows.Forms.Label();
            this.label_conf_method_data = new System.Windows.Forms.Label();
            this.label_conf_threshold = new System.Windows.Forms.Label();
            this.groupBox_NN_Info.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_loadedImage)).BeginInit();
            this.groupBox_loadedKanji.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_NN_Info
            // 
            this.groupBox_NN_Info.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox_NN_Info.Controls.Add(this.label_NN_state);
            this.groupBox_NN_Info.Controls.Add(this.label_NN_patterns_data);
            this.groupBox_NN_Info.Controls.Add(this.label_NN_nCount_data);
            this.groupBox_NN_Info.Controls.Add(this.label_NN_modelName_data);
            this.groupBox_NN_Info.Controls.Add(this.label_NN_state_data);
            this.groupBox_NN_Info.Controls.Add(this.label_NN_patterns);
            this.groupBox_NN_Info.Controls.Add(this.label_NN_nCount);
            this.groupBox_NN_Info.Controls.Add(this.label_NN_modelName);
            this.groupBox_NN_Info.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox_NN_Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_NN_Info.Location = new System.Drawing.Point(25, 37);
            this.groupBox_NN_Info.Name = "groupBox_NN_Info";
            this.groupBox_NN_Info.Size = new System.Drawing.Size(355, 71);
            this.groupBox_NN_Info.TabIndex = 0;
            this.groupBox_NN_Info.TabStop = false;
            this.groupBox_NN_Info.Text = "Red Neuronal";
            // 
            // label_NN_state
            // 
            this.label_NN_state.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_NN_state.AutoSize = true;
            this.label_NN_state.Location = new System.Drawing.Point(14, 25);
            this.label_NN_state.Name = "label_NN_state";
            this.label_NN_state.Size = new System.Drawing.Size(43, 13);
            this.label_NN_state.TabIndex = 0;
            this.label_NN_state.Text = "Estado:";
            // 
            // label_NN_patterns_data
            // 
            this.label_NN_patterns_data.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_NN_patterns_data.Location = new System.Drawing.Point(271, 47);
            this.label_NN_patterns_data.Name = "label_NN_patterns_data";
            this.label_NN_patterns_data.Size = new System.Drawing.Size(74, 13);
            this.label_NN_patterns_data.TabIndex = 9;
            this.label_NN_patterns_data.Text = "status";
            this.label_NN_patterns_data.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_NN_nCount_data
            // 
            this.label_NN_nCount_data.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_NN_nCount_data.Location = new System.Drawing.Point(97, 47);
            this.label_NN_nCount_data.Name = "label_NN_nCount_data";
            this.label_NN_nCount_data.Size = new System.Drawing.Size(70, 13);
            this.label_NN_nCount_data.TabIndex = 5;
            this.label_NN_nCount_data.Text = "status";
            this.label_NN_nCount_data.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_NN_modelName_data
            // 
            this.label_NN_modelName_data.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_NN_modelName_data.Location = new System.Drawing.Point(262, 25);
            this.label_NN_modelName_data.Name = "label_NN_modelName_data";
            this.label_NN_modelName_data.Size = new System.Drawing.Size(83, 13);
            this.label_NN_modelName_data.TabIndex = 8;
            this.label_NN_modelName_data.Text = "status";
            this.label_NN_modelName_data.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_NN_state_data
            // 
            this.label_NN_state_data.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_NN_state_data.Location = new System.Drawing.Point(63, 25);
            this.label_NN_state_data.Name = "label_NN_state_data";
            this.label_NN_state_data.Size = new System.Drawing.Size(104, 13);
            this.label_NN_state_data.TabIndex = 3;
            this.label_NN_state_data.Text = "status";
            this.label_NN_state_data.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_NN_patterns
            // 
            this.label_NN_patterns.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_NN_patterns.AutoSize = true;
            this.label_NN_patterns.Location = new System.Drawing.Point(187, 47);
            this.label_NN_patterns.Name = "label_NN_patterns";
            this.label_NN_patterns.Size = new System.Drawing.Size(82, 13);
            this.label_NN_patterns.TabIndex = 7;
            this.label_NN_patterns.Text = "N° de Patrones:";
            // 
            // label_NN_nCount
            // 
            this.label_NN_nCount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_NN_nCount.AutoSize = true;
            this.label_NN_nCount.Location = new System.Drawing.Point(14, 47);
            this.label_NN_nCount.Name = "label_NN_nCount";
            this.label_NN_nCount.Size = new System.Drawing.Size(86, 13);
            this.label_NN_nCount.TabIndex = 1;
            this.label_NN_nCount.Text = "N° de Neuronas:";
            // 
            // label_NN_modelName
            // 
            this.label_NN_modelName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_NN_modelName.AutoSize = true;
            this.label_NN_modelName.Location = new System.Drawing.Point(187, 25);
            this.label_NN_modelName.Name = "label_NN_modelName";
            this.label_NN_modelName.Size = new System.Drawing.Size(45, 13);
            this.label_NN_modelName.TabIndex = 6;
            this.label_NN_modelName.Text = "Modelo:";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.redNeuronalToolStripMenuItem,
            this.patronesToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(400, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // redNeuronalToolStripMenuItem
            // 
            this.redNeuronalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNNToolStripMenuItem,
            this.exportarANNToolStripMenuItem,
            this.exportarToolStripMenuItem});
            this.redNeuronalToolStripMenuItem.Name = "redNeuronalToolStripMenuItem";
            this.redNeuronalToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.redNeuronalToolStripMenuItem.Text = "Archivo";
            // 
            // createNNToolStripMenuItem
            // 
            this.createNNToolStripMenuItem.Name = "createNNToolStripMenuItem";
            this.createNNToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.createNNToolStripMenuItem.Text = "Crear nueva RNA";
            this.createNNToolStripMenuItem.Click += new System.EventHandler(this.crearNuevaToolStripMenuItem_Click);
            // 
            // exportarANNToolStripMenuItem
            // 
            this.exportarANNToolStripMenuItem.Enabled = false;
            this.exportarANNToolStripMenuItem.Name = "exportarANNToolStripMenuItem";
            this.exportarANNToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exportarANNToolStripMenuItem.Text = "Importar...";
            // 
            // exportarToolStripMenuItem
            // 
            this.exportarToolStripMenuItem.Enabled = false;
            this.exportarToolStripMenuItem.Name = "exportarToolStripMenuItem";
            this.exportarToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exportarToolStripMenuItem.Text = "Exportar...";
            // 
            // patronesToolStripMenuItem
            // 
            this.patronesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.teachKanjiToolStripMenuItem,
            this.showLearnedToolStripMenuItem});
            this.patronesToolStripMenuItem.Enabled = false;
            this.patronesToolStripMenuItem.Name = "patronesToolStripMenuItem";
            this.patronesToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.patronesToolStripMenuItem.Text = "Patrones";
            // 
            // teachKanjiToolStripMenuItem
            // 
            this.teachKanjiToolStripMenuItem.Name = "teachKanjiToolStripMenuItem";
            this.teachKanjiToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.teachKanjiToolStripMenuItem.Text = "Enseñar";
            this.teachKanjiToolStripMenuItem.Click += new System.EventHandler(this.teachKanjiToolStripMenuItem_Click);
            // 
            // showLearnedToolStripMenuItem
            // 
            this.showLearnedToolStripMenuItem.Enabled = false;
            this.showLearnedToolStripMenuItem.Name = "showLearnedToolStripMenuItem";
            this.showLearnedToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.showLearnedToolStripMenuItem.Text = "Aprendidos";
            // 
            // pictureBox_loadedImage
            // 
            this.pictureBox_loadedImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_loadedImage.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox_loadedImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_loadedImage.Location = new System.Drawing.Point(6, 19);
            this.pictureBox_loadedImage.Name = "pictureBox_loadedImage";
            this.pictureBox_loadedImage.Size = new System.Drawing.Size(258, 258);
            this.pictureBox_loadedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_loadedImage.TabIndex = 2;
            this.pictureBox_loadedImage.TabStop = false;
            // 
            // button_loadImage
            // 
            this.button_loadImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_loadImage.Location = new System.Drawing.Point(274, 18);
            this.button_loadImage.Name = "button_loadImage";
            this.button_loadImage.Size = new System.Drawing.Size(75, 23);
            this.button_loadImage.TabIndex = 3;
            this.button_loadImage.Text = "Cargar";
            this.button_loadImage.UseVisualStyleBackColor = true;
            this.button_loadImage.Click += new System.EventHandler(this.button_loadImage_Click);
            // 
            // button_runDynamics
            // 
            this.button_runDynamics.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_runDynamics.Location = new System.Drawing.Point(159, 467);
            this.button_runDynamics.Name = "button_runDynamics";
            this.button_runDynamics.Size = new System.Drawing.Size(82, 27);
            this.button_runDynamics.TabIndex = 4;
            this.button_runDynamics.Text = "Analizar";
            this.button_runDynamics.UseVisualStyleBackColor = true;
            this.button_runDynamics.Click += new System.EventHandler(this.button_runDynamics_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Imagenes|*.jpg; *.png; *.bmp";
            // 
            // groupBox_loadedKanji
            // 
            this.groupBox_loadedKanji.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_loadedKanji.Controls.Add(this.button_edit);
            this.groupBox_loadedKanji.Controls.Add(this.button_addNoise);
            this.groupBox_loadedKanji.Controls.Add(this.pictureBox_loadedImage);
            this.groupBox_loadedKanji.Controls.Add(this.button_loadImage);
            this.groupBox_loadedKanji.Location = new System.Drawing.Point(25, 170);
            this.groupBox_loadedKanji.Name = "groupBox_loadedKanji";
            this.groupBox_loadedKanji.Size = new System.Drawing.Size(355, 283);
            this.groupBox_loadedKanji.TabIndex = 5;
            this.groupBox_loadedKanji.TabStop = false;
            this.groupBox_loadedKanji.Text = "Kanji a Reconocer";
            // 
            // button_edit
            // 
            this.button_edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_edit.Location = new System.Drawing.Point(274, 76);
            this.button_edit.Name = "button_edit";
            this.button_edit.Size = new System.Drawing.Size(75, 23);
            this.button_edit.TabIndex = 4;
            this.button_edit.Text = "Editar";
            this.button_edit.UseVisualStyleBackColor = true;
            this.button_edit.Click += new System.EventHandler(this.button_edit_Click);
            // 
            // button_addNoise
            // 
            this.button_addNoise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_addNoise.Location = new System.Drawing.Point(274, 47);
            this.button_addNoise.Name = "button_addNoise";
            this.button_addNoise.Size = new System.Drawing.Size(75, 23);
            this.button_addNoise.TabIndex = 4;
            this.button_addNoise.Text = "Añadir ruido";
            this.button_addNoise.UseVisualStyleBackColor = true;
            this.button_addNoise.Click += new System.EventHandler(this.button_addNoise_Click);
            // 
            // openMultipleFileDialog
            // 
            this.openMultipleFileDialog.FileName = "openFileDialog";
            this.openMultipleFileDialog.Filter = "Imagenes|*.jpg; *.png; *.bmp";
            this.openMultipleFileDialog.Multiselect = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_conf_method);
            this.groupBox1.Controls.Add(this.label_conf_threshold_data);
            this.groupBox1.Controls.Add(this.label_conf_method_data);
            this.groupBox1.Controls.Add(this.label_conf_threshold);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(25, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 50);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuración";
            // 
            // label_conf_method
            // 
            this.label_conf_method.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_conf_method.AutoSize = true;
            this.label_conf_method.Location = new System.Drawing.Point(14, 25);
            this.label_conf_method.Name = "label_conf_method";
            this.label_conf_method.Size = new System.Drawing.Size(46, 13);
            this.label_conf_method.TabIndex = 0;
            this.label_conf_method.Text = "Método:";
            // 
            // label_conf_threshold_data
            // 
            this.label_conf_threshold_data.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_conf_threshold_data.Location = new System.Drawing.Point(255, 25);
            this.label_conf_threshold_data.Name = "label_conf_threshold_data";
            this.label_conf_threshold_data.Size = new System.Drawing.Size(90, 13);
            this.label_conf_threshold_data.TabIndex = 5;
            this.label_conf_threshold_data.Text = "status";
            this.label_conf_threshold_data.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_conf_method_data
            // 
            this.label_conf_method_data.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_conf_method_data.Location = new System.Drawing.Point(63, 25);
            this.label_conf_method_data.Name = "label_conf_method_data";
            this.label_conf_method_data.Size = new System.Drawing.Size(104, 13);
            this.label_conf_method_data.TabIndex = 3;
            this.label_conf_method_data.Text = "status";
            this.label_conf_method_data.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_conf_threshold
            // 
            this.label_conf_threshold.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_conf_threshold.AutoSize = true;
            this.label_conf_threshold.Location = new System.Drawing.Point(187, 25);
            this.label_conf_threshold.Name = "label_conf_threshold";
            this.label_conf_threshold.Size = new System.Drawing.Size(57, 13);
            this.label_conf_threshold.TabIndex = 1;
            this.label_conf_threshold.Text = "Threshold:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 502);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_loadedKanji);
            this.Controls.Add(this.button_runDynamics);
            this.Controls.Add(this.groupBox_NN_Info);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(416, 485);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kanji Recognizer";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox_NN_Info.ResumeLayout(false);
            this.groupBox_NN_Info.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_loadedImage)).EndInit();
            this.groupBox_loadedKanji.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_NN_Info;
        private System.Windows.Forms.Label label_NN_nCount;
        private System.Windows.Forms.Label label_NN_state;
        private System.Windows.Forms.Label label_NN_nCount_data;
        private System.Windows.Forms.Label label_NN_state_data;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem redNeuronalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNNToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox_loadedImage;
        private System.Windows.Forms.Button button_loadImage;
        private System.Windows.Forms.Button button_runDynamics;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem exportarANNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patronesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem teachKanjiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLearnedToolStripMenuItem;
        private System.Windows.Forms.Label label_NN_patterns_data;
        private System.Windows.Forms.Label label_NN_modelName_data;
        private System.Windows.Forms.Label label_NN_patterns;
        private System.Windows.Forms.Label label_NN_modelName;
        private System.Windows.Forms.GroupBox groupBox_loadedKanji;
        private System.Windows.Forms.OpenFileDialog openMultipleFileDialog;
        private System.Windows.Forms.Button button_addNoise;
        private System.Windows.Forms.Button button_edit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_conf_method;
        private System.Windows.Forms.Label label_conf_threshold_data;
        private System.Windows.Forms.Label label_conf_method_data;
        private System.Windows.Forms.Label label_conf_threshold;
    }
}