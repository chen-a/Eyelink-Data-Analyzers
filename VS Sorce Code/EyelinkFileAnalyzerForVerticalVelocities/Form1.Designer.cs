namespace EyelinkFileAnalizer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.DragAndDropTB = new System.Windows.Forms.TextBox();
            this.Instructions = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.lnstructionBox = new System.Windows.Forms.Label();
            this.AcclerationDataButton = new System.Windows.Forms.Button();
            this.VelocityLable = new System.Windows.Forms.Label();
            this.TimeLable = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.AccInstructions = new System.Windows.Forms.Label();
            this.ChooseGraphLabel = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChooseFileBT = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.removeLabel = new System.Windows.Forms.Label();
            this.removeOutliersBT = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.testSelectionBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // DragAndDropTB
            // 
            this.DragAndDropTB.AllowDrop = true;
            this.DragAndDropTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DragAndDropTB.Location = new System.Drawing.Point(12, 151);
            this.DragAndDropTB.Multiline = true;
            this.DragAndDropTB.Name = "DragAndDropTB";
            this.DragAndDropTB.Size = new System.Drawing.Size(823, 104);
            this.DragAndDropTB.TabIndex = 0;
            this.DragAndDropTB.TextChanged += new System.EventHandler(this.DragAndDropTB_TextChanged);
            this.DragAndDropTB.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragAndDropTB_DragDrop);
            this.DragAndDropTB.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragAndDropTB_DragEnter);
            // 
            // Instructions
            // 
            this.Instructions.AutoSize = true;
            this.Instructions.Location = new System.Drawing.Point(12, 105);
            this.Instructions.Name = "Instructions";
            this.Instructions.Size = new System.Drawing.Size(0, 25);
            this.Instructions.TabIndex = 1;
            // 
            // StartButton
            // 
            this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton.Location = new System.Drawing.Point(667, 261);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(173, 39);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "Create Report";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // lnstructionBox
            // 
            this.lnstructionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnstructionBox.Location = new System.Drawing.Point(12, 9);
            this.lnstructionBox.Name = "lnstructionBox";
            this.lnstructionBox.Size = new System.Drawing.Size(823, 139);
            this.lnstructionBox.TabIndex = 3;
            this.lnstructionBox.Text = resources.GetString("lnstructionBox.Text");
            // 
            // AcclerationDataButton
            // 
            this.AcclerationDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AcclerationDataButton.Location = new System.Drawing.Point(17, 836);
            this.AcclerationDataButton.Name = "AcclerationDataButton";
            this.AcclerationDataButton.Size = new System.Drawing.Size(163, 42);
            this.AcclerationDataButton.TabIndex = 4;
            this.AcclerationDataButton.Text = "Get Raw Data \r\n";
            this.AcclerationDataButton.UseVisualStyleBackColor = true;
            this.AcclerationDataButton.Click += new System.EventHandler(this.AcclerationDataButton_Click);
            // 
            // VelocityLable
            // 
            this.VelocityLable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.VelocityLable.AutoSize = true;
            this.VelocityLable.Location = new System.Drawing.Point(-3, 603);
            this.VelocityLable.Name = "VelocityLable";
            this.VelocityLable.Size = new System.Drawing.Size(0, 25);
            this.VelocityLable.TabIndex = 6;
            // 
            // TimeLable
            // 
            this.TimeLable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeLable.AutoSize = true;
            this.TimeLable.Location = new System.Drawing.Point(402, 836);
            this.TimeLable.Name = "TimeLable";
            this.TimeLable.Size = new System.Drawing.Size(0, 25);
            this.TimeLable.TabIndex = 7;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Right Eye Sursumductions",
            "Right Eye Average Sursumduction",
            "Right Eye Average Acceleration Sursumduction",
            "Right Eye Infraductions",
            "Right Eye Average Infraduction",
            "Right Eye Average Acceleration Infraduction",
            "Left Eye Sursumductions",
            "Left Eye Average Sursumduction",
            "Left Eye Average Acceleration Sursumduction",
            "Left Eye Infraductions",
            "Left Eye Average Infraduction",
            "Left Eye Average Acceleration Infraduction"});
            this.comboBox1.Location = new System.Drawing.Point(397, 395);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(438, 32);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.TextChanged += new System.EventHandler(this.ComboBox1_TextChanged);
            // 
            // AccInstructions
            // 
            this.AccInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AccInstructions.AutoSize = true;
            this.AccInstructions.Location = new System.Drawing.Point(12, 303);
            this.AccInstructions.Name = "AccInstructions";
            this.AccInstructions.Size = new System.Drawing.Size(803, 75);
            this.AccInstructions.TabIndex = 9;
            this.AccInstructions.Text = "Graph Instructions:\r\nWith a chosen file in the box use the \"Choose Graph\" box to " +
    "view different graphs or use the\r\n \"Get Raw Data\" button to get a text file with" +
    " the raw data.\r\n";
            // 
            // ChooseGraphLabel
            // 
            this.ChooseGraphLabel.AutoSize = true;
            this.ChooseGraphLabel.Location = new System.Drawing.Point(22, 398);
            this.ChooseGraphLabel.Name = "ChooseGraphLabel";
            this.ChooseGraphLabel.Size = new System.Drawing.Size(146, 25);
            this.ChooseGraphLabel.TabIndex = 10;
            this.ChooseGraphLabel.Text = "Choose Graph:";
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(193, 261);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(175, 38);
            this.ClearButton.TabIndex = 11;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.AutoFitMinFontSize = 14;
            legend1.ItemColumnSpacing = 80;
            legend1.LegendItemOrder = System.Windows.Forms.DataVisualization.Charting.LegendItemOrder.ReversedSeriesOrder;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(21, 433);
            this.chart1.MaximumSize = new System.Drawing.Size(818, 397);
            this.chart1.MinimumSize = new System.Drawing.Size(818, 397);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chart1.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Red,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128))))),
        System.Drawing.Color.Yellow,
        System.Drawing.Color.Lime,
        System.Drawing.Color.Aqua,
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))),
        System.Drawing.Color.Silver,
        System.Drawing.Color.Green,
        System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))))};
            this.chart1.Size = new System.Drawing.Size(818, 397);
            this.chart1.TabIndex = 12;
            this.chart1.Text = "chart1";
            title1.Name = "Title1";
            this.chart1.Titles.Add(title1);
            // 
            // ChooseFileBT
            // 
            this.ChooseFileBT.Location = new System.Drawing.Point(12, 261);
            this.ChooseFileBT.Name = "ChooseFileBT";
            this.ChooseFileBT.Size = new System.Drawing.Size(175, 39);
            this.ChooseFileBT.TabIndex = 20;
            this.ChooseFileBT.Text = "Choose File";
            this.ChooseFileBT.UseVisualStyleBackColor = true;
            this.ChooseFileBT.Click += new System.EventHandler(this.ChooseFileBT_Click);
            // 
            // removeLabel
            // 
            this.removeLabel.AutoSize = true;
            this.removeLabel.Location = new System.Drawing.Point(636, 405);
            this.removeLabel.Name = "removeLabel";
            this.removeLabel.Size = new System.Drawing.Size(0, 25);
            this.removeLabel.TabIndex = 25;
            // 
            // removeOutliersBT
            // 
            this.removeOutliersBT.AutoSize = true;
            this.removeOutliersBT.Checked = true;
            this.removeOutliersBT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.removeOutliersBT.Location = new System.Drawing.Point(607, 844);
            this.removeOutliersBT.Name = "removeOutliersBT";
            this.removeOutliersBT.Size = new System.Drawing.Size(228, 29);
            this.removeOutliersBT.TabIndex = 26;
            this.removeOutliersBT.Text = "Auto Remove Outliers";
            this.removeOutliersBT.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(374, 262);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(287, 37);
            this.progressBar1.TabIndex = 27;
            // 
            // testSelectionBox
            // 
            this.testSelectionBox.FormattingEnabled = true;
            this.testSelectionBox.Items.AddRange(new object[] {
            "Test 1: Head Straight",
            "Test 2: Head Right Turn",
            "Test 3: Head Left Turn"});
            this.testSelectionBox.Location = new System.Drawing.Point(174, 395);
            this.testSelectionBox.Name = "testSelectionBox";
            this.testSelectionBox.Size = new System.Drawing.Size(217, 32);
            this.testSelectionBox.TabIndex = 28;
            this.testSelectionBox.TextChanged += new System.EventHandler(this.TestSelectionBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 916);
            this.Controls.Add(this.testSelectionBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.removeOutliersBT);
            this.Controls.Add(this.removeLabel);
            this.Controls.Add(this.ChooseFileBT);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.ChooseGraphLabel);
            this.Controls.Add(this.AccInstructions);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.TimeLable);
            this.Controls.Add(this.VelocityLable);
            this.Controls.Add(this.AcclerationDataButton);
            this.Controls.Add(this.lnstructionBox);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.Instructions);
            this.Controls.Add(this.DragAndDropTB);
            this.MaximumSize = new System.Drawing.Size(890, 980);
            this.MinimumSize = new System.Drawing.Size(890, 980);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EyeLink Text File Analyzer: Vertical Saccades";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DragAndDropTB;
        private System.Windows.Forms.Label Instructions;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label lnstructionBox;
        private System.Windows.Forms.Button AcclerationDataButton;
        private System.Windows.Forms.Label VelocityLable;
        private System.Windows.Forms.Label TimeLable;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label AccInstructions;
        private System.Windows.Forms.Label ChooseGraphLabel;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button ChooseFileBT;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label removeLabel;
        private System.Windows.Forms.CheckBox removeOutliersBT;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox testSelectionBox;
    }
}

