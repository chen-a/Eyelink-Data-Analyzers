namespace EyelinkFileAnalizer
{
    partial class Form3
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
            this.TCThresholdBox = new System.Windows.Forms.TextBox();
            this.BCThresholdBox = new System.Windows.Forms.TextBox();
            this.MaxAmplitudeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.MinAmplitudeBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TCThresholdBox
            // 
            this.TCThresholdBox.Location = new System.Drawing.Point(12, 12);
            this.TCThresholdBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TCThresholdBox.Name = "TCThresholdBox";
            this.TCThresholdBox.Size = new System.Drawing.Size(73, 22);
            this.TCThresholdBox.TabIndex = 0;
            this.TCThresholdBox.Text = "712";
            // 
            // BCThresholdBox
            // 
            this.BCThresholdBox.Location = new System.Drawing.Point(12, 48);
            this.BCThresholdBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BCThresholdBox.Name = "BCThresholdBox";
            this.BCThresholdBox.Size = new System.Drawing.Size(73, 22);
            this.BCThresholdBox.TabIndex = 1;
            this.BCThresholdBox.Text = "312";
            // 
            // MaxAmplitudeBox
            // 
            this.MaxAmplitudeBox.Location = new System.Drawing.Point(12, 84);
            this.MaxAmplitudeBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaxAmplitudeBox.Name = "MaxAmplitudeBox";
            this.MaxAmplitudeBox.Size = new System.Drawing.Size(73, 22);
            this.MaxAmplitudeBox.TabIndex = 2;
            this.MaxAmplitudeBox.Text = "45";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(92, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(382, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Top to Center Resolution Threshold (Default: 712)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(92, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(408, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Bottom to Center Resolution Threshold (Default: 312)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(92, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(387, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Maximum Amplitude Allowed (Default: 45 degrees)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 166);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(347, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Note: Click Save and Exit to Close. Do not click X to close.";
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(430, 163);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(121, 23);
            this.exitButton.TabIndex = 7;
            this.exitButton.Text = "Save and Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // MinAmplitudeBox
            // 
            this.MinAmplitudeBox.Location = new System.Drawing.Point(12, 120);
            this.MinAmplitudeBox.Name = "MinAmplitudeBox";
            this.MinAmplitudeBox.Size = new System.Drawing.Size(72, 22);
            this.MinAmplitudeBox.TabIndex = 8;
            this.MinAmplitudeBox.Text = "5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(92, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(374, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Minimum Amplitude Allowed (Default: 5 degrees)";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 199);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MinAmplitudeBox);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MaxAmplitudeBox);
            this.Controls.Add(this.BCThresholdBox);
            this.Controls.Add(this.TCThresholdBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form3";
            this.Text = "Advanced Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox TCThresholdBox;
        public System.Windows.Forms.TextBox BCThresholdBox;
        public System.Windows.Forms.TextBox MaxAmplitudeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button exitButton;
        public System.Windows.Forms.TextBox MinAmplitudeBox;
        private System.Windows.Forms.Label label5;
    }
}