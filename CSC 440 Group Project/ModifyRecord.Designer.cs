namespace CSC_440_Group_Project
{
    partial class ModifyRecord
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
            this.submitModifyButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SemesterIn = new System.Windows.Forms.TextBox();
            this.YearIn = new System.Windows.Forms.TextBox();
            this.GradeIn = new System.Windows.Forms.TextBox();
            this.CourseNumIn = new System.Windows.Forms.TextBox();
            this.CoursePrefixIn = new System.Windows.Forms.TextBox();
            this.StudentIDIn = new System.Windows.Forms.TextBox();
            this.cancelModifyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // submitModifyButton
            // 
            this.submitModifyButton.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitModifyButton.Location = new System.Drawing.Point(192, 262);
            this.submitModifyButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.submitModifyButton.Name = "submitModifyButton";
            this.submitModifyButton.Size = new System.Drawing.Size(80, 31);
            this.submitModifyButton.TabIndex = 25;
            this.submitModifyButton.Text = "Submit";
            this.submitModifyButton.UseVisualStyleBackColor = true;
            this.submitModifyButton.Click += new System.EventHandler(this.submitModifyButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(342, 135);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 32);
            this.label6.TabIndex = 24;
            this.label6.Text = "Semester";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(387, 100);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 32);
            this.label5.TabIndex = 23;
            this.label5.Text = "Year";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(322, 58);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 32);
            this.label4.TabIndex = 22;
            this.label4.Text = "New Grade";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(2, 135);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 32);
            this.label3.TabIndex = 21;
            this.label3.Text = "Course Number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(32, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 32);
            this.label2.TabIndex = 20;
            this.label2.Text = "Course Prefix";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(56, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 32);
            this.label1.TabIndex = 19;
            this.label1.Text = "Student ID";
            // 
            // SemesterIn
            // 
            this.SemesterIn.Location = new System.Drawing.Point(452, 145);
            this.SemesterIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SemesterIn.Name = "SemesterIn";
            this.SemesterIn.Size = new System.Drawing.Size(122, 26);
            this.SemesterIn.TabIndex = 18;
            // 
            // YearIn
            // 
            this.YearIn.Location = new System.Drawing.Point(452, 105);
            this.YearIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.YearIn.Name = "YearIn";
            this.YearIn.Size = new System.Drawing.Size(122, 26);
            this.YearIn.TabIndex = 17;
            // 
            // GradeIn
            // 
            this.GradeIn.Location = new System.Drawing.Point(452, 66);
            this.GradeIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GradeIn.Name = "GradeIn";
            this.GradeIn.Size = new System.Drawing.Size(122, 26);
            this.GradeIn.TabIndex = 16;
            // 
            // CourseNumIn
            // 
            this.CourseNumIn.Location = new System.Drawing.Point(188, 145);
            this.CourseNumIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CourseNumIn.Name = "CourseNumIn";
            this.CourseNumIn.Size = new System.Drawing.Size(127, 26);
            this.CourseNumIn.TabIndex = 15;
            // 
            // CoursePrefixIn
            // 
            this.CoursePrefixIn.Location = new System.Drawing.Point(188, 108);
            this.CoursePrefixIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CoursePrefixIn.Name = "CoursePrefixIn";
            this.CoursePrefixIn.Size = new System.Drawing.Size(127, 26);
            this.CoursePrefixIn.TabIndex = 14;
            // 
            // StudentIDIn
            // 
            this.StudentIDIn.Location = new System.Drawing.Point(188, 66);
            this.StudentIDIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StudentIDIn.Name = "StudentIDIn";
            this.StudentIDIn.Size = new System.Drawing.Size(127, 26);
            this.StudentIDIn.TabIndex = 13;
            // 
            // cancelModifyButton
            // 
            this.cancelModifyButton.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelModifyButton.Location = new System.Drawing.Point(302, 262);
            this.cancelModifyButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cancelModifyButton.Name = "cancelModifyButton";
            this.cancelModifyButton.Size = new System.Drawing.Size(80, 31);
            this.cancelModifyButton.TabIndex = 26;
            this.cancelModifyButton.Text = "Cancel";
            this.cancelModifyButton.UseVisualStyleBackColor = true;
            this.cancelModifyButton.Click += new System.EventHandler(this.cancelModifyButton_Click);
            // 
            // ModifyRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.cancelModifyButton);
            this.Controls.Add(this.submitModifyButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SemesterIn);
            this.Controls.Add(this.YearIn);
            this.Controls.Add(this.GradeIn);
            this.Controls.Add(this.CourseNumIn);
            this.Controls.Add(this.CoursePrefixIn);
            this.Controls.Add(this.StudentIDIn);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ModifyRecord";
            this.Text = "Modify Record";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submitModifyButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SemesterIn;
        private System.Windows.Forms.TextBox YearIn;
        private System.Windows.Forms.TextBox GradeIn;
        private System.Windows.Forms.TextBox CourseNumIn;
        private System.Windows.Forms.TextBox CoursePrefixIn;
        private System.Windows.Forms.TextBox StudentIDIn;
        private System.Windows.Forms.Button cancelModifyButton;
    }
}