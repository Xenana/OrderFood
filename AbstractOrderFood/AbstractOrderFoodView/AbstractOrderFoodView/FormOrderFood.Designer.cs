namespace AbstractOrderFoodView
{
    partial class FormOrderFood
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заказчикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.различныеБлюдаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.наборыБлюдToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.кухниToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.шефповараToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пополнитьКухнюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonCreateOrder = new System.Windows.Forms.Button();
            this.buttonTakeOrderInWork = new System.Windows.Forms.Button();
            this.buttonOrderReady = new System.Windows.Forms.Button();
            this.buttonPayOrder = new System.Windows.Forms.Button();
            this.buttonRef = new System.Windows.Forms.Button();
            this.отчётыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.прайсНоборовБлюдToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загруженностьКухоньToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заказыЗаказчиковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.пополнитьКухнюToolStripMenuItem,
            this.отчётыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(998, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.заказчикиToolStripMenuItem,
            this.различныеБлюдаToolStripMenuItem,
            this.наборыБлюдToolStripMenuItem,
            this.кухниToolStripMenuItem,
            this.шефповараToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // заказчикиToolStripMenuItem
            // 
            this.заказчикиToolStripMenuItem.Name = "заказчикиToolStripMenuItem";
            this.заказчикиToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.заказчикиToolStripMenuItem.Text = "Заказчики";
            this.заказчикиToolStripMenuItem.Click += new System.EventHandler(this.заказчикиToolStripMenuItem_Click);
            // 
            // различныеБлюдаToolStripMenuItem
            // 
            this.различныеБлюдаToolStripMenuItem.Name = "различныеБлюдаToolStripMenuItem";
            this.различныеБлюдаToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.различныеБлюдаToolStripMenuItem.Text = "Различные блюда";
            this.различныеБлюдаToolStripMenuItem.Click += new System.EventHandler(this.различныеБлюдаToolStripMenuItem_Click);
            // 
            // наборыБлюдToolStripMenuItem
            // 
            this.наборыБлюдToolStripMenuItem.Name = "наборыБлюдToolStripMenuItem";
            this.наборыБлюдToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.наборыБлюдToolStripMenuItem.Text = "Наборы блюд";
            this.наборыБлюдToolStripMenuItem.Click += new System.EventHandler(this.наборыБлюдToolStripMenuItem_Click);
            // 
            // кухниToolStripMenuItem
            // 
            this.кухниToolStripMenuItem.Name = "кухниToolStripMenuItem";
            this.кухниToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.кухниToolStripMenuItem.Text = "Кухни";
            this.кухниToolStripMenuItem.Click += new System.EventHandler(this.кухниToolStripMenuItem_Click);
            // 
            // шефповараToolStripMenuItem
            // 
            this.шефповараToolStripMenuItem.Name = "шефповараToolStripMenuItem";
            this.шефповараToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.шефповараToolStripMenuItem.Text = "Шеф-повара";
            this.шефповараToolStripMenuItem.Click += new System.EventHandler(this.шефповараToolStripMenuItem_Click);
            // 
            // пополнитьКухнюToolStripMenuItem
            // 
            this.пополнитьКухнюToolStripMenuItem.Name = "пополнитьКухнюToolStripMenuItem";
            this.пополнитьКухнюToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.пополнитьКухнюToolStripMenuItem.Text = "Пополнить кухню";
            this.пополнитьКухнюToolStripMenuItem.Click += new System.EventHandler(this.пополнитьКухнюToolStripMenuItem_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 28);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(794, 313);
            this.dataGridView.TabIndex = 1;
            // 
            // buttonCreateOrder
            // 
            this.buttonCreateOrder.Location = new System.Drawing.Point(834, 53);
            this.buttonCreateOrder.Name = "buttonCreateOrder";
            this.buttonCreateOrder.Size = new System.Drawing.Size(141, 23);
            this.buttonCreateOrder.TabIndex = 2;
            this.buttonCreateOrder.Text = "Создать заказ";
            this.buttonCreateOrder.UseVisualStyleBackColor = true;
            this.buttonCreateOrder.Click += new System.EventHandler(this.buttonCreateOrder_Click);
            // 
            // buttonTakeOrderInWork
            // 
            this.buttonTakeOrderInWork.Location = new System.Drawing.Point(834, 101);
            this.buttonTakeOrderInWork.Name = "buttonTakeOrderInWork";
            this.buttonTakeOrderInWork.Size = new System.Drawing.Size(141, 23);
            this.buttonTakeOrderInWork.TabIndex = 3;
            this.buttonTakeOrderInWork.Text = "Отдать на выполнение";
            this.buttonTakeOrderInWork.UseVisualStyleBackColor = true;
            this.buttonTakeOrderInWork.Click += new System.EventHandler(this.buttonTakeOrderInWork_Click);
            // 
            // buttonOrderReady
            // 
            this.buttonOrderReady.Location = new System.Drawing.Point(834, 149);
            this.buttonOrderReady.Name = "buttonOrderReady";
            this.buttonOrderReady.Size = new System.Drawing.Size(141, 23);
            this.buttonOrderReady.TabIndex = 4;
            this.buttonOrderReady.Text = "Заказ готов";
            this.buttonOrderReady.UseVisualStyleBackColor = true;
            this.buttonOrderReady.Click += new System.EventHandler(this.buttonOrderReady_Click);
            // 
            // buttonPayOrder
            // 
            this.buttonPayOrder.Location = new System.Drawing.Point(834, 203);
            this.buttonPayOrder.Name = "buttonPayOrder";
            this.buttonPayOrder.Size = new System.Drawing.Size(141, 23);
            this.buttonPayOrder.TabIndex = 5;
            this.buttonPayOrder.Text = "Заказ оплачен";
            this.buttonPayOrder.UseVisualStyleBackColor = true;
            this.buttonPayOrder.Click += new System.EventHandler(this.buttonPayOrder_Click);
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(834, 253);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(141, 23);
            this.buttonRef.TabIndex = 6;
            this.buttonRef.Text = "Обновить список";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // отчётыToolStripMenuItem
            // 
            this.отчётыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.прайсНоборовБлюдToolStripMenuItem,
            this.загруженностьКухоньToolStripMenuItem,
            this.заказыЗаказчиковToolStripMenuItem});
            this.отчётыToolStripMenuItem.Name = "отчётыToolStripMenuItem";
            this.отчётыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчётыToolStripMenuItem.Text = "Отчёты";
            // 
            // прайсНоборовБлюдToolStripMenuItem
            // 
            this.прайсНоборовБлюдToolStripMenuItem.Name = "прайсНоборовБлюдToolStripMenuItem";
            this.прайсНоборовБлюдToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.прайсНоборовБлюдToolStripMenuItem.Text = "Прайс наборов блюд";
            this.прайсНоборовБлюдToolStripMenuItem.Click += new System.EventHandler(this.прайсНоборовБлюдToolStripMenuItem_Click);
            // 
            // загруженностьКухоньToolStripMenuItem
            // 
            this.загруженностьКухоньToolStripMenuItem.Name = "загруженностьКухоньToolStripMenuItem";
            this.загруженностьКухоньToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.загруженностьКухоньToolStripMenuItem.Text = "Загруженность кухонь";
            this.загруженностьКухоньToolStripMenuItem.Click += new System.EventHandler(this.загруженностьКухоньToolStripMenuItem_Click);
            // 
            // заказыЗаказчиковToolStripMenuItem
            // 
            this.заказыЗаказчиковToolStripMenuItem.Name = "заказыЗаказчиковToolStripMenuItem";
            this.заказыЗаказчиковToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.заказыЗаказчиковToolStripMenuItem.Text = "Заказы заказчиков";
            this.заказыЗаказчиковToolStripMenuItem.Click += new System.EventHandler(this.заказыЗаказчиковToolStripMenuItem_Click);
            // 
            // FormOrderFood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 343);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.buttonPayOrder);
            this.Controls.Add(this.buttonOrderReady);
            this.Controls.Add(this.buttonTakeOrderInWork);
            this.Controls.Add(this.buttonCreateOrder);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormOrderFood";
            this.Text = "Абстрактный заказ еды";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заказчикиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem различныеБлюдаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem наборыБлюдToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem кухниToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem шефповараToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пополнитьКухнюToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonCreateOrder;
        private System.Windows.Forms.Button buttonTakeOrderInWork;
        private System.Windows.Forms.Button buttonOrderReady;
        private System.Windows.Forms.Button buttonPayOrder;
        private System.Windows.Forms.Button buttonRef;
        private System.Windows.Forms.ToolStripMenuItem отчётыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem прайсНоборовБлюдToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загруженностьКухоньToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заказыЗаказчиковToolStripMenuItem;
    }
}

