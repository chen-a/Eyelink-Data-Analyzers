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
            this.btnFormTwo = new System.Windows.Forms.Button();
            this.removeLabel = new System.Windows.Forms.Label();
            this.removeOutliersBT = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // DragAndDropTB
            // 
            this.DragAndDropTB.AllowDrop = true;
            this.DragAndDropTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DragAndDropTB.Location = new System.Drawing.Point(9, 101);
            this.DragAndDropTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DragAndDropTB.Multiline = true;
            this.DragAndDropTB.Name = "DragAndDropTB";
            this.DragAndDropTB.Size = new System.Drawing.Size(600, 71);
            this.DragAndDropTB.TabIndex = 0;
            this.DragAndDropTB.TextChanged += new System.EventHandler(this.DragAndDropTB_TextChanged);
            this.DragAndDropTB.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragAndDropTB_DragDrop);
            this.DragAndDropTB.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragAndDropTB_DragEnter);
            // 
            // Instructions
            // 
            this.Instructions.AutoSize = true;
            this.Instructions.Location = new System.Drawing.Point(9, 70);
            this.Instructions.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Instructions.Name = "Instructions";
            this.Instructions.Size = new System.Drawing.Size(0, 16);
            this.Instructions.TabIndex = 1;
            // 
            // StartButton
            // 
            this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton.Location = new System.Drawing.Point(485, 174);
            this.StartButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(126, 26);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "Create Report";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // lnstructionBox
            // 
            this.lnstructionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnstructionBox.Location = new System.Drawing.Point(9, 6);
            this.lnstructionBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnstructionBox.Name = "lnstructionBox";
            this.lnstructionBox.Size = new System.Drawing.Size(599, 93);
            this.lnstructionBox.TabIndex = 3;
            this.lnstructionBox.Text = resources.GetString("lnstructionBox.Text");
            // 
            // AcclerationDataButton
            // 
            this.AcclerationDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AcclerationDataButton.Location = new System.Drawing.Point(12, 557);
            this.AcclerationDataButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AcclerationDataButton.Name = "AcclerationDataButton";
            this.AcclerationDataButton.Size = new System.Drawing.Size(119, 28);
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
            this.VelocityLable.Location = new System.Drawing.Point(-2, 402);
            this.VelocityLable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VelocityLable.Name = "VelocityLable";
            this.VelocityLable.Size = new System.Drawing.Size(0, 16);
            this.VelocityLable.TabIndex = 6;
            // 
            // TimeLable
            // 
            this.TimeLable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeLable.AutoSize = true;
            this.TimeLable.Location = new System.Drawing.Point(292, 557);
            this.TimeLable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TimeLable.Name = "TimeLable";
            this.TimeLable.Size = new System.Drawing.Size(0, 16);
            this.TimeLable.TabIndex = 7;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Right Eye Abductions",
            "Right Eye Average Abduction",
            "Right Eye Average Acceleration Abduction",
            "Right Eye Adductions",
            "Right Eye Average Adduction",
            "Right Eye Average Acceleration Adduction",
            "Left Eye Abductions",
            "Left Eye Average Abduction",
            "Left Eye Average Acceleration Abduction",
            "Left Eye Adductions",
            "Left Eye Average Adduction",
            "Left Eye Average Acceleration Adduction"});
            this.comboBox1.Location = new System.Drawing.Point(127, 263);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(277, 24);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.TextChanged += new System.EventHandler(this.ComboBox1_TextChanged);
            // 
            // AccInstructions
            // 
            this.AccInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AccInstructions.AutoSize = true;
            this.AccInstructions.Location = new System.Drawing.Point(9, 202);
            this.AccInstructions.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AccInstructions.Name = "AccInstructions";
            this.AccInstructions.Size = new System.Drawing.Size(541, 48);
            this.AccInstructions.TabIndex = 9;
            this.AccInstructions.Text = "Graph Instructions:\r\nWith a chosen file in the box use the \"Choose Graph\" box to " +
    "view different graphs or use the\r\n \"Get Raw Data\" button to get a text file with" +
    " the raw data.\r\n";
            // 
            // ChooseGraphLabel
            // 
            this.ChooseGraphLabel.AutoSize = true;
            this.ChooseGraphLabel.Location = new System.Drawing.Point(16, 265);
            this.ChooseGraphLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ChooseGraphLabel.Name = "ChooseGraphLabel";
            this.ChooseGraphLabel.Size = new System.Drawing.Size(97, 16);
            this.ChooseGraphLabel.TabIndex = 10;
            this.ChooseGraphLabel.Text = "Choose Graph:";
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(140, 174);
            this.ClearButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(127, 25);
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
            this.chart1.Location = new System.Drawing.Point(15, 289);
            this.chart1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chart1.MaximumSize = new System.Drawing.Size(595, 265);
            this.chart1.MinimumSize = new System.Drawing.Size(595, 265);
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
            this.chart1.Size = new System.Drawing.Size(595, 265);
            this.chart1.TabIndex = 12;
            this.chart1.Text = "chart1";
            title1.Name = "Title1";
            this.chart1.Titles.Add(title1);
            // 
            // ChooseFileBT
            // 
            this.ChooseFileBT.Location = new System.Drawing.Point(9, 174);
            this.ChooseFileBT.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChooseFileBT.Name = "ChooseFileBT";
            this.ChooseFileBT.Size = new System.Drawing.Size(127, 26);
            this.ChooseFileBT.TabIndex = 20;
            this.ChooseFileBT.Text = "Choose File";
            this.ChooseFileBT.UseVisualStyleBackColor = true;
            this.ChooseFileBT.Click += new System.EventHandler(this.ChooseFileBT_Click);
            // 
            // btnFormTwo
            // 
            this.btnFormTwo.Location = new System.Drawing.Point(466, 557);
            this.btnFormTwo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnFormTwo.Name = "btnFormTwo";
            this.btnFormTwo.Size = new System.Drawing.Size(145, 28);
            this.btnFormTwo.TabIndex = 24;
            this.btnFormTwo.Text = "Standard Deviation";
            this.btnFormTwo.UseVisualStyleBackColor = true;
            this.btnFormTwo.Click += new System.EventHandler(this.BtnFormTwo_Click);
            // 
            // removeLabel
            // 
            this.removeLabel.AutoSize = true;
            this.removeLabel.Location = new System.Drawing.Point(463, 270);
            this.removeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.removeLabel.Name = "removeLabel";
            this.removeLabel.Size = new System.Drawing.Size(0, 16);
            this.removeLabel.TabIndex = 25;
            // 
            // removeOutliersBT
            // 
            this.removeOutliersBT.AutoSize = true;
            this.removeOutliersBT.Checked = true;
            this.removeOutliersBT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.removeOutliersBT.Location = new System.Drawing.Point(441, 265);
            this.removeOutliersBT.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.removeOutliersBT.Name = "removeOutliersBT";
            this.removeOutliersBT.Size = new System.Drawing.Size(159, 20);
            this.removeOutliersBT.TabIndex = 26;
            this.removeOutliersBT.Text = "Auto Remove Outliers";
            this.removeOutliersBT.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(272, 175);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(209, 25);
            this.progressBar1.TabIndex = 27;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(472, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 31);
            this.button1.TabIndex = 28;
            this.button1.Text = "Advanced Options";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 622);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.removeOutliersBT);
            this.Controls.Add(this.removeLabel);
            this.Controls.Add(this.btnFormTwo);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(652, 669);
            this.MinimumSize = new System.Drawing.Size(652, 669);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EyeLink Text File Analyzer: Horizontal Saccades";
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
        private System.Windows.Forms.Button btnFormTwo;
        private System.Windows.Forms.Label removeLabel;
        private System.Windows.Forms.CheckBox removeOutliersBT;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
    }
}

