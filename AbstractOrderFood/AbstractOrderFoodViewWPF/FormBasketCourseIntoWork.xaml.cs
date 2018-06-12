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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;
using Unity.Attributes;

namespace AbstractOrderFoodViewWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class FormBasketCourseIntoWork : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IChefsService serviceC;

        private readonly IOrderFoodService serviceOF;

        private int? id;

        public FormBasketCourseIntoWork(IChefsService serviceC, IOrderFoodService serviceOF)
        {
            InitializeComponent();
            Loaded += FormBasketCourseIntoWork_Load;
            this.serviceC = serviceC;
            this.serviceOF = serviceOF;
        }

        private void FormBasketCourseIntoWork_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указана заявка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
                List<ChefsViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxChef.DisplayMemberPath = "ChefsFIO";
                    comboBoxChef.SelectedValuePath = "Id";
                    comboBoxChef.ItemsSource = listC;
                    comboBoxChef.SelectedItem = null;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxChef.SelectedItem == null)
            {
                MessageBox.Show("Выберите шеф-повара", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                serviceOF.TakeOrderInWork(new BasketCourseBindingModel
                {
                    Id = id.Value,
                    ChefsId = ((ChefsViewModel)comboBoxChef.SelectedItem).Id,
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

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
