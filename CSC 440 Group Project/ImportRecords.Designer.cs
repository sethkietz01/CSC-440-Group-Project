namespace CSC_440_Group_Project
{
    partial class importRecords
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
            this.fromFileButton = new System.Windows.Forms.Button();
            this.fromFolderButton = new System.Windows.Forms.Button();
            this.mainLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cancelImportButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fromFileButton
            // 
            this.fromFileButton.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromFileButton.Location = new System.Drawing.Point(136, 142);
            this.fromFileButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fromFileButton.Name = "fromFileButton";
            this.fromFileButton.Size = new System.Drawing.Size(104, 35);
            this.fromFileButton.TabIndex = 0;
            this.fromFileButton.Text = "From File";
            this.fromFileButton.UseVisualStyleBackColor = true;
            this.fromFileButton.Click += new System.EventHandler(this.fromFileButton_Click);
            // 
            // fromFolderButton
            // 
            this.fromFolderButton.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromFolderButton.Location = new System.Drawing.Point(352, 142);
            this.fromFolderButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fromFolderButton.Name = "fromFolderButton";
            this.fromFolderButton.Size = new System.Drawing.Size(132, 35);
            this.fromFolderButton.TabIndex = 1;
            this.fromFolderButton.Text = "From Folder";
            this.fromFolderButton.UseVisualStyleBackColor = true;
            this.fromFolderButton.Click += new System.EventHandler(this.fromFolderButton_Click);
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.Font = new System.Drawing.Font("Yu Gothic UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainLabel.ForeColor = System.Drawing.Color.White;
            this.mainLabel.Location = new System.Drawing.Point(124, 45);
            this.mainLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(353, 65);
            this.mainLabel.TabIndex = 2;
            this.mainLabel.Text = "Import Records";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cancelImportButton
            // 
            this.cancelImportButton.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelImportButton.Location = new System.Drawing.Point(243, 242);
            this.cancelImportButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cancelImportButton.Name = "cancelImportButton";
            this.cancelImportButton.Size = new System.Drawing.Size(94, 35);
            this.cancelImportButton.TabIndex = 3;
            this.cancelImportButton.Text = "Cancel";
            this.cancelImportButton.UseVisualStyleBackColor = true;
            this.cancelImportButton.Click += new System.EventHandler(this.cancelImportButton_Click);
            // 
            // importRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.cancelImportButton);
            this.Controls.Add(this.mainLabel);
            this.Controls.Add(this.fromFolderButton);
            this.Controls.Add(this.fromFileButton);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "importRecords";
            this.Text = "Import Records";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button fromFileButton;
        private System.Windows.Forms.Button fromFolderButton;
        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button cancelImportButton;
    }
}