namespace AbstractOrderFoodView
{
    partial class FormBasketCourseIntoWork
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
            this.labelImplementer = new System.Windows.Forms.Label();
            this.comboBoxChef = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelImplementer
            // 
            this.labelImplementer.AutoSize = true;
            this.labelImplementer.Location = new System.Drawing.Point(12, 9);
            this.labelImplementer.Name = "labelImplementer";
            this.labelImplementer.Size = new System.Drawing.Size(66, 13);
            this.labelImplementer.TabIndex = 1;
            this.labelImplementer.Text = "Шеф-повар:";
            // 
            // comboBoxChef
            // 
            this.comboBoxChef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChef.FormattingEnabled = true;
            this.comboBoxChef.Location = new System.Drawing.Point(84, 6);
            this.comboBoxChef.Name = "comboBoxChef";
            this.comboBoxChef.Size = new System.Drawing.Size(230, 21);
            this.comboBoxChef.TabIndex = 2;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(140, 42);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(239, 42);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormTakeOrderInWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 75);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxChef);
            this.Controls.Add(this.labelImplementer);
            this.Name = "FormTakeOrderInWork";
            this.Text = "Отдать заказ в работу";
            this.Load += new System.EventHandler(this.FormTakeOrderInWork_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelImplementer;
        private System.Windows.Forms.ComboBox comboBoxChef;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}