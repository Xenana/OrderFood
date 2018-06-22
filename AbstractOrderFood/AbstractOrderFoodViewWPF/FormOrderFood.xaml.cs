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
    /// Логика взаимодействия для FormOrderFood.xaml
    /// </summary>
    public partial class FormOrderFood : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IOrderFoodService service;

        public FormOrderFood(IOrderFoodService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void LoadData()
        {
            try
            {
                List<BasketCourseViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridView.ItemsSource = list;
                    dataGridView.Columns[0].Visibility = Visibility.Hidden;
                    dataGridView.Columns[1].Visibility = Visibility.Hidden;
                    dataGridView.Columns[3].Visibility = Visibility.Hidden;
                    dataGridView.Columns[5].Visibility = Visibility.Hidden;
                    dataGridView.Columns[1].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void butCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<FormCreateBasketCourse>();
            form.ShowDialog();
            LoadData();
        }

        private void заказчики_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<FormCustomers>();
            form.ShowDialog();
        }

        private void различныеблюда_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<FormCourses>();
            form.ShowDialog();
        }

        private void наборыблюд_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<FormCourseSets>();
            form.ShowDialog();
        }

        private void кухни_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<FormKitchens>();
            form.ShowDialog();
        }

        private void шефповара_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<FormChefs>();
            form.ShowDialog();
        }

        private void пополнитькухню_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<FormPutOnKitchen>();
            form.ShowDialog();
        }

        private void butOrderInWork_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItem != null)
            {
                var form = Container.Resolve<FormBasketCourseIntoWork>();
                form.Id = ((BasketCourseViewModel)dataGridView.SelectedItem).Id;
                form.ShowDialog();
                LoadData();
            }
        }

        private void butOrderReady_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItem != null)
            {
                int id = ((BasketCourseViewModel)dataGridView.SelectedItem).Id;
                try
                {
                    service.FinishOrder(id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void butOrderPay_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItem != null)
            {
                int id = ((BasketCourseViewModel)dataGridView.SelectedItem).Id;
                try
                {
                    service.PayOrder(id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void butRefList_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
