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
    public partial class FormCourseSet : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ICourseSetsService service;

        private int? id;

        private List<CourseSetsCoursesViewModel> courseSetsCourses;

        public FormCourseSet(ICourseSetsService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormCourseSet_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CourseSetsViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.CourseSetsName;
                        textBoxPrice.Text = view.Cost.ToString();
                        courseSetsCourses = view.CourseSetsCourses;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                courseSetsCourses = new List<CourseSetsCoursesViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (courseSetsCourses != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = courseSetsCourses;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCourseSetsCourses>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.CourseSetsId = id.Value;
                    }
                    courseSetsCourses.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormCourseSetsCourses>();
                form.Model = courseSetsCourses[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    courseSetsCourses[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        courseSetsCourses.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (courseSetsCourses == null || courseSetsCourses.Count == 0)
            {
                MessageBox.Show("Заполните блюда", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<CourseSetsCoursesBindingModel> courseSetsCoursesBM = new List<CourseSetsCoursesBindingModel>();
                for (int i = 0; i < courseSetsCourses.Count; ++i)
                {
                    courseSetsCoursesBM.Add(new CourseSetsCoursesBindingModel
                    {
                        Id = courseSetsCourses[i].Id,
                        CourseSetsId = courseSetsCourses[i].CourseSetsId,
                        CoursesId = courseSetsCourses[i].CoursesId,
                        Count = courseSetsCourses[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new CourseSetsBindingModel
                    {
                        Id = id.Value,
                        CourseSetName = textBoxName.Text,
                        Cost = Convert.ToInt32(textBoxPrice.Text),
                        CourseSetsCourses = courseSetsCoursesBM
                    });
                }
                else
                {
                    service.AddElement(new CourseSetsBindingModel
                    {
                        CourseSetName = textBoxName.Text,
                        Cost = Convert.ToInt32(textBoxPrice.Text),
                        CourseSetsCourses = courseSetsCoursesBM
                    });
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
