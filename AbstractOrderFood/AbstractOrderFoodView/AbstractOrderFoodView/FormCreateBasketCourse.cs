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
    public partial class FormCreateBasketCourse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ICustomersService serviceCu;

        private readonly ICourseSetsService serviceCo;

        private readonly IOrderFoodService serviceOF;

        public FormCreateBasketCourse(ICustomersService serviceCu, ICourseSetsService serviceCo, IOrderFoodService serviceOF)
        {
            InitializeComponent();
            this.serviceCu = serviceCu;
            this.serviceCo = serviceCo;
            this.serviceOF = serviceOF;
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomersViewModel> listC = serviceCu.GetList();
                if (listC != null)
                {
                    comboBoxCustomer.DisplayMember = "CustomersFIO";
                    comboBoxCustomer.ValueMember = "Id";
                    comboBoxCustomer.DataSource = listC;
                    comboBoxCustomer.SelectedItem = null;
                }
                List<CourseSetsViewModel> listP = serviceCo.GetList();
                if (listP != null)
                {
                    comboBoxCourseSet.DisplayMember = "CourseSetsName";
                    comboBoxCourseSet.ValueMember = "Id";
                    comboBoxCourseSet.DataSource = listP;
                    comboBoxCourseSet.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxCourseSet.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxCourseSet.SelectedValue);
                    CourseSetsViewModel product = serviceCo.GetElement(id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * product.Cost).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxCourseSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCustomer.SelectedValue == null)
            {
                MessageBox.Show("Выберите заказчика", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCourseSet.SelectedValue == null)
            {
                MessageBox.Show("Выберите набор блюд", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceOF.CreateOrder(new BasketCourseBindingModel
                {
                    CustomersId = Convert.ToInt32(comboBoxCustomer.SelectedValue),
                    CourseSetsId = Convert.ToInt32(comboBoxCourseSet.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
