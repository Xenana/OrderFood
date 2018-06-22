using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationOfInter;
using AbstractOrderFoodService.ImplementationsList;
using AbstractOrderFoodService.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
namespace AbstractOrderFoodView
{
    public partial class FormPutOnKitchen : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IKitchenService serviceK;

        private readonly ICoursesService serviceC;

        private readonly IOrderFoodService serviceOF;

        public FormPutOnKitchen(IKitchenService serviceK, ICoursesService serviceC, IOrderFoodService serviceOF)
        {
            InitializeComponent();
            this.serviceK = serviceK;
            this.serviceC = serviceC;
            this.serviceOF = serviceOF;
        }

        private void FormPutOnKitchen_Load(object sender, EventArgs e)
        {
            try
            {
                List<CoursesViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxCourses.DisplayMember = "CoursesName";
                    comboBoxCourses.ValueMember = "Id";
                    comboBoxCourses.DataSource = listC;
                    comboBoxCourses.SelectedItem = null;
                }
                List<KitchenViewModel> listS = serviceK.GetList();
                if (listS != null)
                {
                    comboBoxKitchen.DisplayMember = "KitchenName";
                    comboBoxKitchen.ValueMember = "Id";
                    comboBoxKitchen.DataSource = listS;
                    comboBoxKitchen.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCourses.SelectedValue == null)
            {
                MessageBox.Show("Выберите блюдо", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxKitchen.SelectedValue == null)
            {
                MessageBox.Show("Выберите кухню", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceOF.PutCoursesOnKitchen(new KitchenCoursesBindingModel
                {
                    CoursesId = Convert.ToInt32(comboBoxCourses.SelectedValue),
                    KitchenId = Convert.ToInt32(comboBoxKitchen.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
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
