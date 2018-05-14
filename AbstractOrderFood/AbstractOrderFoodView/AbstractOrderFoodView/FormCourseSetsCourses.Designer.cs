namespace AbstractOrderFoodView
{
    partial class FormCourseSetsCourses
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxCourses = new System.Windows.Forms.ComboBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.Save_Course = new System.Windows.Forms.Button();
            this.Cancel_Course = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Блюдо:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Количество:";
            // 
            // comboBoxCourses
            // 
            this.comboBoxCourses.FormattingEnabled = true;
            this.comboBoxCourses.Location = new System.Drawing.Point(96, 15);
            this.comboBoxCourses.Name = "comboBoxCourses";
            this.comboBoxCourses.Size = new System.Drawing.Size(272, 21);
            this.comboBoxCourses.TabIndex = 2;
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(96, 44);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(272, 20);
            this.textBoxCount.TabIndex = 3;
            // 
            // Save_Course
            // 
            this.Save_Course.Location = new System.Drawing.Point(188, 70);
            this.Save_Course.Name = "Save_Course";
            this.Save_Course.Size = new System.Drawing.Size(75, 23);
            this.Save_Course.TabIndex = 4;
            this.Save_Course.Text = "Сохранить";
            this.Save_Course.UseVisualStyleBackColor = true;
            this.Save_Course.Click += new System.EventHandler(this.Save_Course_Click);
            // 
            // Cancel_Course
            // 
            this.Cancel_Course.Location = new System.Drawing.Point(293, 70);
            this.Cancel_Course.Name = "Cancel_Course";
            this.Cancel_Course.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Course.TabIndex = 5;
            this.Cancel_Course.Text = "Отмена";
            this.Cancel_Course.UseVisualStyleBackColor = true;
            this.Cancel_Course.Click += new System.EventHandler(this.Cancel_Course_Click);
            // 
            // FormCourseSetsCourses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 103);
            this.Controls.Add(this.Cancel_Course);
            this.Controls.Add(this.Save_Course);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.comboBoxCourses);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormCourseSetsCourses";
            this.Text = "Блюдо в набор";
            this.Load += new System.EventHandler(this.FormCourseSetsCourses_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxCourses;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Button Save_Course;
        private System.Windows.Forms.Button Cancel_Course;
    }
}