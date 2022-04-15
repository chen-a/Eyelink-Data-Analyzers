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
            this.LCThresholdBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RCThresholdBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MaxAmplitudeBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LCThresholdBox
            // 
            this.LCThresholdBox.Location = new System.Drawing.Point(12, 12);
            this.LCThresholdBox.Name = "LCThresholdBox";
            this.LCThresholdBox.Size = new System.Drawing.Size(73, 22);
            this.LCThresholdBox.TabIndex = 0;
            this.LCThresholdBox.Text = "440";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(92, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Left and Center Resolution Threshold (Default: 440)";
            // 
            // RCThresholdBox
            // 
            this.RCThresholdBox.Location = new System.Drawing.Point(12, 48);
            this.RCThresholdBox.Name = "RCThresholdBox";
            this.RCThresholdBox.Size = new System.Drawing.Size(73, 22);
            this.RCThresholdBox.TabIndex = 2;
            this.RCThresholdBox.Text = "840";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(92, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(406, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Right and Center Resolution Threshold (Default: 840)";
            // 
            // MaxAmplitudeBox
            // 
            this.MaxAmplitudeBox.Location = new System.Drawing.Point(12, 84);
            this.MaxAmplitudeBox.Name = "MaxAmplitudeBox";
            this.MaxAmplitudeBox.Size = new System.Drawing.Size(73, 22);
            this.MaxAmplitudeBox.TabIndex = 4;
            this.MaxAmplitudeBox.Text = "45";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(92, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(387, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Maximum Amplitude Allowed (Default: 45 degrees)";
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(431, 133);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(121, 23);
            this.exitButton.TabIndex = 6;
            this.exitButton.Text = "Save and Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Click Save and Exit to Close";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 168);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MaxAmplitudeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RCThresholdBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LCThresholdBox);
            this.Name = "Form3";
            this.Text = "Advanced Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox LCThresholdBox;
        public System.Windows.Forms.TextBox RCThresholdBox;
        public System.Windows.Forms.TextBox MaxAmplitudeBox;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label label4;
    }
}