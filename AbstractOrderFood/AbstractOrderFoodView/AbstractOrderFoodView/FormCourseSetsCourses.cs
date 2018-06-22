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
    public partial class FormCourseSetsCourses : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public CourseSetsCoursesViewModel Model { set { model = value; } get { return model; } }

        private readonly ICoursesService service;

        private CourseSetsCoursesViewModel model;

        public FormCourseSetsCourses(ICoursesService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormCourseSetsCourses_Load(object sender, EventArgs e)
        {
            try
            {
                List<CoursesViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBoxCourses.DisplayMember = "CoursesName";
                    comboBoxCourses.ValueMember = "Id";
                    comboBoxCourses.DataSource = list;
                    comboBoxCourses.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxCourses.Enabled = false;
                comboBoxCourses.SelectedValue = model.CoursesId;
                textBoxCount.Text = model.Count.ToString();
            }
        }

        private void Save_Course_Click(object sender, EventArgs e)
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
            try
            {
                if (model == null)
                {
                    model = new CourseSetsCoursesViewModel
                    {
                        CoursesId = Convert.ToInt32(comboBoxCourses.SelectedValue),
                        CoursesName = comboBoxCourses.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cancel_Course_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
