namespace CSC_440_Group_Project
{
    partial class Print_Report
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
            this.submitPrintButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cancelPrintButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // submitPrintButton
            // 
            this.submitPrintButton.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitPrintButton.Location = new System.Drawing.Point(100, 290);
            this.submitPrintButton.Margin = new System.Windows.Forms.Padding(2);
            this.submitPrintButton.Name = "submitPrintButton";
            this.submitPrintButton.Size = new System.Drawing.Size(79, 30);
            this.submitPrintButton.TabIndex = 16;
            this.submitPrintButton.Text = "Submit";
            this.submitPrintButton.UseVisualStyleBackColor = true;
            this.submitPrintButton.Click += new System.EventHandler(this.submitPrintButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(40, 76);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 32);
            this.label1.TabIndex = 15;
            this.label1.Text = "Student ID";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(172, 82);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 26);
            this.textBox1.TabIndex = 14;
            // 
            // cancelPrintButton
            // 
            this.cancelPrintButton.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelPrintButton.Location = new System.Drawing.Point(405, 290);
            this.cancelPrintButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelPrintButton.Name = "cancelPrintButton";
            this.cancelPrintButton.Size = new System.Drawing.Size(79, 30);
            this.cancelPrintButton.TabIndex = 17;
            this.cancelPrintButton.Text = "Cancel";
            this.cancelPrintButton.UseVisualStyleBackColor = true;
            this.cancelPrintButton.Click += new System.EventHandler(this.cancelPrintButton_Click);
            // 
            // Print_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.cancelPrintButton);
            this.Controls.Add(this.submitPrintButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Print_Report";
            this.Text = "Print Report";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submitPrintButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button cancelPrintButton;
    }
}