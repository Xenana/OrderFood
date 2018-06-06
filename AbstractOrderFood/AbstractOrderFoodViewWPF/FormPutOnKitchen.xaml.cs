using AbstractOrderFoodService.BindingModel;
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
    /// Логика взаимодействия для FormPutOnKitchen.xaml
    /// </summary>
    public partial class FormPutOnKitchen : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IKitchenService serviceK;

        private readonly ICoursesService serviceC;

        private readonly IOrderFoodService serviceOF;

        public FormPutOnKitchen(IKitchenService serviceK, ICoursesService serviceC, IOrderFoodService serviceOF)
        {
            InitializeComponent();
            Loaded += FormPutOnKitchen_Load;
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
                    comboBoxCourse.DisplayMemberPath = "CoursesName";
                    comboBoxCourse.SelectedValuePath = "Id";
                    comboBoxCourse.ItemsSource = listC;
                    comboBoxCourse.SelectedItem = null;
                }
                List<KitchenViewModel> listK = serviceK.GetList();
                if (listK != null)
                {
                    comboBoxKitchen.DisplayMemberPath = "KitchenName";
                    comboBoxKitchen.SelectedValuePath = "Id";
                    comboBoxKitchen.ItemsSource = listK;
                    comboBoxKitchen.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Выберите блюдо", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxKitchen.SelectedItem == null)
            {
                MessageBox.Show("Выберите кухню", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                serviceOF.PutCoursesOnKitchen(new KitchenCoursesBindingModel
                {
                    CoursesId = Convert.ToInt32(comboBoxCourse.SelectedValue),
                    KitchenId = Convert.ToInt32(comboBoxKitchen.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
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
