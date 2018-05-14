namespace AbstractOrderFoodView
{
    partial class FormCustomer
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
            this.FIO_Cust = new System.Windows.Forms.TextBox();
            this.Save_Cust = new System.Windows.Forms.Button();
            this.Cancel_Cust = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ФИО:";
            // 
            // FIO_Cust
            // 
            this.FIO_Cust.Location = new System.Drawing.Point(55, 17);
            this.FIO_Cust.Name = "FIO_Cust";
            this.FIO_Cust.Size = new System.Drawing.Size(256, 20);
            this.FIO_Cust.TabIndex = 1;
            // 
            // Save_Cust
            // 
            this.Save_Cust.Location = new System.Drawing.Point(138, 49);
            this.Save_Cust.Name = "Save_Cust";
            this.Save_Cust.Size = new System.Drawing.Size(75, 23);
            this.Save_Cust.TabIndex = 2;
            this.Save_Cust.Text = "Сохранить";
            this.Save_Cust.UseVisualStyleBackColor = true;
            this.Save_Cust.Click += new System.EventHandler(this.Save_Cust_Click);
            // 
            // Cancel_Cust
            // 
            this.Cancel_Cust.Location = new System.Drawing.Point(236, 49);
            this.Cancel_Cust.Name = "Cancel_Cust";
            this.Cancel_Cust.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Cust.TabIndex = 3;
            this.Cancel_Cust.Text = "Отмена";
            this.Cancel_Cust.UseVisualStyleBackColor = true;
            this.Cancel_Cust.Click += new System.EventHandler(this.Cancel_Cust_Click);
            // 
            // FormCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 84);
            this.Controls.Add(this.Cancel_Cust);
            this.Controls.Add(this.Save_Cust);
            this.Controls.Add(this.FIO_Cust);
            this.Controls.Add(this.label1);
            this.Name = "FormCustomer";
            this.Text = "Заказчик";
            this.Load += new System.EventHandler(this.FormCustomer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FIO_Cust;
        private System.Windows.Forms.Button Save_Cust;
        private System.Windows.Forms.Button Cancel_Cust;
    }
}