namespace EyelinkFileAnalizer
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.selectFilesButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearButton = new System.Windows.Forms.Button();
            this.mainButton = new System.Windows.Forms.Button();
            this.instructions = new System.Windows.Forms.Label();
            this.resultsTB = new System.Windows.Forms.TextBox();
            this.ReslutsLable = new System.Windows.Forms.Label();
            this.removeOutliersBT = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.AllowDrop = true;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 24;
            this.listBox1.Location = new System.Drawing.Point(31, 212);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(565, 220);
            this.listBox1.TabIndex = 0;
            this.listBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListBox1_DragDrop);
            this.listBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListBox1_DragEnter);
            this.listBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ListBox1_MouseUp);
            // 
            // selectFilesButton
            // 
            this.selectFilesButton.Location = new System.Drawing.Point(292, 445);
            this.selectFilesButton.Name = "selectFilesButton";
            this.selectFilesButton.Size = new System.Drawing.Size(149, 35);
            this.selectFilesButton.TabIndex = 1;
            this.selectFilesButton.Text = "Select Files";
            this.selectFilesButton.UseVisualStyleBackColor = true;
            this.selectFilesButton.Click += new System.EventHandler(this.SelectFilesButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(147, 40);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(146, 36);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(447, 445);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(149, 35);
            this.ClearButton.TabIndex = 3;
            this.ClearButton.Text = "Clear Box";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // mainButton
            // 
            this.mainButton.Location = new System.Drawing.Point(341, 833);
            this.mainButton.Name = "mainButton";
            this.mainButton.Size = new System.Drawing.Size(255, 35);
            this.mainButton.TabIndex = 4;
            this.mainButton.Text = "Get Standard Deviation";
            this.mainButton.UseVisualStyleBackColor = true;
            this.mainButton.Click += new System.EventHandler(this.MainButton_Click);
            // 
            // instructions
            // 
            this.instructions.AutoSize = true;
            this.instructions.Location = new System.Drawing.Point(12, 9);
            this.instructions.Name = "instructions";
            this.instructions.Size = new System.Drawing.Size(611, 200);
            this.instructions.TabIndex = 5;
            this.instructions.Text = resources.GetString("instructions.Text");
            // 
            // resultsTB
            // 
            this.resultsTB.Location = new System.Drawing.Point(31, 486);
            this.resultsTB.Multiline = true;
            this.resultsTB.Name = "resultsTB";
            this.resultsTB.Size = new System.Drawing.Size(565, 341);
            this.resultsTB.TabIndex = 6;
            // 
            // ReslutsLable
            // 
            this.ReslutsLable.AutoSize = true;
            this.ReslutsLable.Location = new System.Drawing.Point(26, 458);
            this.ReslutsLable.Name = "ReslutsLable";
            this.ReslutsLable.Size = new System.Drawing.Size(82, 25);
            this.ReslutsLable.TabIndex = 7;
            this.ReslutsLable.Text = "Results:";
            // 
            // removeOutliersBT
            // 
            this.removeOutliersBT.AutoSize = true;
            this.removeOutliersBT.Checked = true;
            this.removeOutliersBT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.removeOutliersBT.Location = new System.Drawing.Point(31, 837);
            this.removeOutliersBT.Name = "removeOutliersBT";
            this.removeOutliersBT.Size = new System.Drawing.Size(228, 29);
            this.removeOutliersBT.TabIndex = 27;
            this.removeOutliersBT.Text = "Auto Remove Outliers";
            this.removeOutliersBT.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 878);
            this.Controls.Add(this.removeOutliersBT);
            this.Controls.Add(this.ReslutsLable);
            this.Controls.Add(this.resultsTB);
            this.Controls.Add(this.instructions);
            this.Controls.Add(this.mainButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.selectFilesButton);
            this.Controls.Add(this.listBox1);
            this.MaximumSize = new System.Drawing.Size(658, 942);
            this.MinimumSize = new System.Drawing.Size(658, 942);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Standard Deviation Calculator";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button selectFilesButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button mainButton;
        private System.Windows.Forms.Label instructions;
        private System.Windows.Forms.TextBox resultsTB;
        private System.Windows.Forms.Label ReslutsLable;
        private System.Windows.Forms.CheckBox removeOutliersBT;
    }
}