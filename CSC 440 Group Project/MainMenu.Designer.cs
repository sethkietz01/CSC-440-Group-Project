﻿namespace CSC_440_Group_Project
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
            this.components = new System.ComponentModel.Container();
            this.mainLabel = new System.Windows.Forms.Label();
            this.deleteRecordButton = new System.Windows.Forms.Button();
            this.modifyRecordButton = new System.Windows.Forms.Button();
            this.printReportButton = new System.Windows.Forms.Button();
            this.addRecordButton = new System.Windows.Forms.Button();
            this.importRecordsButton = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.Font = new System.Drawing.Font("Yu Gothic UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainLabel.ForeColor = System.Drawing.Color.White;
            this.mainLabel.Location = new System.Drawing.Point(299, 50);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(681, 65);
            this.mainLabel.TabIndex = 0;
            this.mainLabel.Text = "Student Grade Manage System";
            this.mainLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // deleteRecordButton
            // 
            this.deleteRecordButton.BackColor = System.Drawing.Color.Silver;
            this.deleteRecordButton.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteRecordButton.ForeColor = System.Drawing.Color.Black;
            this.deleteRecordButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.deleteRecordButton.Location = new System.Drawing.Point(525, 270);
            this.deleteRecordButton.Name = "deleteRecordButton";
            this.deleteRecordButton.Size = new System.Drawing.Size(180, 90);
            this.deleteRecordButton.TabIndex = 3;
            this.deleteRecordButton.Text = "Delete Record";
            this.deleteRecordButton.UseVisualStyleBackColor = false;
            this.deleteRecordButton.Click += new System.EventHandler(this.deleteRecordButton_Click);
            // 
            // modifyRecordButton
            // 
            this.modifyRecordButton.BackColor = System.Drawing.Color.Silver;
            this.modifyRecordButton.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modifyRecordButton.ForeColor = System.Drawing.Color.Black;
            this.modifyRecordButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.modifyRecordButton.Location = new System.Drawing.Point(755, 270);
            this.modifyRecordButton.Name = "modifyRecordButton";
            this.modifyRecordButton.Size = new System.Drawing.Size(180, 90);
            this.modifyRecordButton.TabIndex = 4;
            this.modifyRecordButton.Text = "Modify Record";
            this.modifyRecordButton.UseVisualStyleBackColor = false;
            this.modifyRecordButton.Click += new System.EventHandler(this.modifyRecordButton_Click);
            // 
            // printReportButton
            // 
            this.printReportButton.BackColor = System.Drawing.Color.Silver;
            this.printReportButton.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printReportButton.ForeColor = System.Drawing.Color.Black;
            this.printReportButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.printReportButton.Location = new System.Drawing.Point(985, 270);
            this.printReportButton.Name = "printReportButton";
            this.printReportButton.Size = new System.Drawing.Size(180, 90);
            this.printReportButton.TabIndex = 5;
            this.printReportButton.Text = "Print Report";
            this.printReportButton.UseVisualStyleBackColor = false;
            this.printReportButton.Click += new System.EventHandler(this.printReportButton_Click);
            // 
            // addRecordButton
            // 
            this.addRecordButton.BackColor = System.Drawing.Color.Silver;
            this.addRecordButton.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addRecordButton.ForeColor = System.Drawing.Color.Black;
            this.addRecordButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.addRecordButton.Location = new System.Drawing.Point(295, 270);
            this.addRecordButton.Name = "addRecordButton";
            this.addRecordButton.Size = new System.Drawing.Size(180, 90);
            this.addRecordButton.TabIndex = 6;
            this.addRecordButton.Text = "Add Record";
            this.addRecordButton.UseVisualStyleBackColor = false;
            this.addRecordButton.Click += new System.EventHandler(this.addRecordButton_Click);
            // 
            // importRecordsButton
            // 
            this.importRecordsButton.BackColor = System.Drawing.Color.Silver;
            this.importRecordsButton.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importRecordsButton.ForeColor = System.Drawing.Color.Black;
            this.importRecordsButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.importRecordsButton.Location = new System.Drawing.Point(65, 270);
            this.importRecordsButton.Name = "importRecordsButton";
            this.importRecordsButton.Size = new System.Drawing.Size(180, 90);
            this.importRecordsButton.TabIndex = 7;
            this.importRecordsButton.Text = "Import Records";
            this.importRecordsButton.UseVisualStyleBackColor = false;
            this.importRecordsButton.Click += new System.EventHandler(this.importRecordsButton_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1258, 664);
            this.Controls.Add(this.importRecordsButton);
            this.Controls.Add(this.addRecordButton);
            this.Controls.Add(this.printReportButton);
            this.Controls.Add(this.modifyRecordButton);
            this.Controls.Add(this.deleteRecordButton);
            this.Controls.Add(this.mainLabel);
            this.Name = "Form1";
            this.Text = "Student Grade Management System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.Button deleteRecordButton;
        private System.Windows.Forms.Button modifyRecordButton;
        private System.Windows.Forms.Button printReportButton;
        private System.Windows.Forms.Button addRecordButton;
        private System.Windows.Forms.Button importRecordsButton;
        private System.Windows.Forms.ImageList imageList1;
    }
}

