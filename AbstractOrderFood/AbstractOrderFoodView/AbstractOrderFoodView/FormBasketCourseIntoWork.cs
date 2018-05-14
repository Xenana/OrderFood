using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationOfInter;
using AbstractOrderFoodService.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace AbstractOrderFoodView
{
    public partial class FormBasketCourseIntoWork : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IChefsService serviceC;

        private readonly IOrderFoodService serviceOF;

        private int? id;

        public FormBasketCourseIntoWork(IChefsService serviceC, IOrderFoodService serviceOF)
        {
            InitializeComponent();
            this.serviceC = serviceC;
            this.serviceOF = serviceOF;
        }

        private void FormTakeOrderInWork_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                List<ChefsViewModel> listI = serviceC.GetList();
                if (listI != null)
                {
                    comboBoxChef.DisplayMember = "ChefsFIO";
                    comboBoxChef.ValueMember = "Id";
                    comboBoxChef.DataSource = listI;
                    comboBoxChef.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxChef.SelectedValue == null)
            {
                MessageBox.Show("Выберите шеф-повара", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceOF.TakeOrderInWork(new BasketCourseBindingModel
                {
                    Id = id.Value,
                    ChefsId = Convert.ToInt32(comboBoxChef.SelectedValue)
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
