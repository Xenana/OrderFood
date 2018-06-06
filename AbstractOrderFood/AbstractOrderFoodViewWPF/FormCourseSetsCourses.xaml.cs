using AbstractOrderFoodService.ImplementationOfInter;
using AbstractOrderFoodService.ImplementationsList;
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
    /// Логика взаимодействия для FormCourseSetsCourses.xaml
    /// </summary>
    public partial class FormCourseSetsCourses : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public CourseSetsCoursesViewModel Model { set { model = value; } get { return model; } }

        private readonly ICoursesService service;

        private CourseSetsCoursesViewModel model;

        public FormCourseSetsCourses(ICoursesService service)
        {
            InitializeComponent();
            Loaded += FormCourseSetsCourses_Load;
            this.service = service;
        }

        private void FormCourseSetsCourses_Load(object sender, EventArgs e)
        {
            List<CoursesViewModel> list = service.GetList();
            try
            {
                if (list != null)
                {
                    comboBoxCourse.DisplayMemberPath = "CoursesName";
                    comboBoxCourse.SelectedValuePath = "Id";
                    comboBoxCourse.ItemsSource = list;
                    comboBoxCourse.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (model != null)
            {
                comboBoxCourse.IsEnabled = false;
                foreach (CoursesViewModel item in list)
                {
                    if (item.CoursesName == model.CoursesName)
                    {
                        comboBoxCourse.SelectedItem = item;
                    }
                }
                textBoxCount.Text = model.Count.ToString();
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxCourse.SelectedItem == null)
            {
                MessageBox.Show("Выберите заготовку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new CourseSetsCoursesViewModel
                    {
                        CoursesId = Convert.ToInt32(comboBoxCourse.SelectedValue),
                        CoursesName = comboBoxCourse.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
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
