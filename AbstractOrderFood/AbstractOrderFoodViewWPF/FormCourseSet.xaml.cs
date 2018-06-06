using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationOfInter;
using AbstractOrderFoodService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;
using Unity.Attributes;

namespace AbstractOrderFoodViewWPF
{
    /// <summary>
    /// Логика взаимодействия для FormCourseSet.xaml
    /// </summary>
    public partial class FormCourseSet : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ICourseSetsService service;

        private int? id;

        private List<CourseSetsCoursesViewModel> courseSetCourses;

        public FormCourseSet(ICourseSetsService service)
        {
            InitializeComponent();
            Loaded += FormCourseSet_Load;
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
                        courseSetCourses = view.CourseSetsCourses;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                courseSetCourses = new List<CourseSetsCoursesViewModel>();
        }

        private void LoadData()
        {
            try
            {
                if (courseSetCourses != null)
                {
                    dataGridView.ItemsSource = null;
                    dataGridView.ItemsSource = courseSetCourses;
                    dataGridView.Columns[0].Visibility = Visibility.Hidden;
                    dataGridView.Columns[1].Visibility = Visibility.Hidden;
                    dataGridView.Columns[2].Visibility = Visibility.Hidden;
                    dataGridView.Columns[3].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<FormCourseSetsCourses>();
            if (form.ShowDialog() == true)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                        form.Model.CourseSetsId = id.Value;
                    courseSetCourses.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItem != null)
            {
                var form = Container.Resolve<FormCourseSetsCourses>();
                form.Model = courseSetCourses[dataGridView.SelectedIndex];
                if (form.ShowDialog() == true)
                {
                    courseSetCourses[dataGridView.SelectedIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        courseSetCourses.RemoveAt(dataGridView.SelectedIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (courseSetCourses == null || courseSetCourses.Count == 0)
            {
                MessageBox.Show("Заполните блюда", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                List<CourseSetsCoursesBindingModel> courseSetsCoursesBM = new List<CourseSetsCoursesBindingModel>();
                for (int i = 0; i < courseSetCourses.Count; ++i)
                {
                    courseSetsCoursesBM.Add(new CourseSetsCoursesBindingModel
                    {
                        Id = courseSetCourses[i].Id,
                        CourseSetsId = courseSetCourses[i].CourseSetsId,
                        CoursesId = courseSetCourses[i].CoursesId,
                        Count = courseSetCourses[i].Count
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
                MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
