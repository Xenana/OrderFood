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
    /// Логика взаимодействия для FormCreateBasketCourse.xaml
    /// </summary>
    public partial class FormCreateBasketCourse : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly ICustomersService serviceC;

        private readonly ICourseSetsService serviceCS;

        private readonly IOrderFoodService serviceOF;

        public FormCreateBasketCourse(ICustomersService serviceC, ICourseSetsService serviceCS, IOrderFoodService serviceOF)
        {
            InitializeComponent();
            Loaded += FormCreateBasketCourse_Load;
            comboBoxCourseSet.SelectionChanged += comboBoxCourseSet_SelectedIndexChanged;

            comboBoxCourseSet.SelectionChanged += new SelectionChangedEventHandler(comboBoxCourseSet_SelectedIndexChanged);
            this.serviceC = serviceC;
            this.serviceCS = serviceCS;
            this.serviceOF = serviceOF;
        }

        private void FormCreateBasketCourse_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomersViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxCustomer.DisplayMemberPath = "CustomersFIO";
                    comboBoxCustomer.SelectedValuePath = "Id";
                    comboBoxCustomer.ItemsSource = listC;
                    comboBoxCustomer.SelectedItem = null;
                }
                List<CourseSetsViewModel> listCS = serviceCS.GetList();
                if (listCS != null)
                {
                    comboBoxCourseSet.DisplayMemberPath = "CourseSetsName";
                    comboBoxCourseSet.SelectedValuePath = "Id";
                    comboBoxCourseSet.ItemsSource = listCS;
                    comboBoxCourseSet.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxCourseSet.SelectedItem != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = ((CourseSetsViewModel)comboBoxCourseSet.SelectedItem).Id;
                    CourseSetsViewModel product = serviceCS.GetElement(id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * product.Cost).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxCustomer.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказчика", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxCourseSet.SelectedItem == null)
            {
                MessageBox.Show("Выберите набор блюд", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                serviceOF.CreateOrder(new BasketCourseBindingModel
                {
                    CustomersId = ((CustomersViewModel)comboBoxCustomer.SelectedItem).Id,
                    CourseSetsId = ((CourseSetsViewModel)comboBoxCourseSet.SelectedItem).Id,
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void textBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcSum();
        }

        private void textBoxSum_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void comboBoxCourseSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBoxCourseSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
