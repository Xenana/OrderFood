using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationOfInter;
using AbstractOrderFoodService.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace AbstractOrderFoodView
{
    public partial class FormOrderFood : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IOrderFoodService service;

        private readonly IReportService reportService;

        public FormOrderFood(IOrderFoodService service, IReportService reportService)
        {
            InitializeComponent();
            this.service = service;
            this.reportService = reportService;
        }

        private void LoadData()
        {
            try
            {
                List<BasketCourseViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                    dataGridView.Columns[5].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void заказчикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCustomers>();
            form.ShowDialog();
        }

        private void различныеБлюдаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCourses>();
            form.ShowDialog();
        }

        private void наборыБлюдToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCourseSets>();
            form.ShowDialog();
        }

        private void кухниToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormKitchens>();
            form.ShowDialog();
        }

        private void шефповараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormChefs>();
            form.ShowDialog();
        }

        private void пополнитьКухнюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormPutOnKitchen>();
            form.ShowDialog();
        }

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCreateBasketCourse>();
            form.ShowDialog();
            LoadData();
        }

        private void buttonTakeOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormBasketCourseIntoWork>();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.ShowDialog();
                LoadData();
            }
        }

        private void buttonOrderReady_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.FinishOrder(id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonPayOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.PayOrder(id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void прайсНоборовБлюдToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    reportService.SaveCourseSetPrice(new ReportBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void загруженностьКухоньToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormKitchensLoad>();
            form.ShowDialog();
        }

        private void заказыЗаказчиковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCustomerBaskets>();
            form.ShowDialog();
        }
    }
}
