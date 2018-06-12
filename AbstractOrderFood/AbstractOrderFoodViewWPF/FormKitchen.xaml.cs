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
    /// Логика взаимодействия для FormKitchen.xaml
    /// </summary>
    public partial class FormKitchen : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IKitchenService service;

        private int? id;

        public FormKitchen(IKitchenService service)
        {
            InitializeComponent();
            Loaded += FormKitchen_Load;
            this.service = service;
        }

        private void FormKitchen_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    KitchenViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.KitchenName;
                        dataGridView.ItemsSource = view.KitchenCourses;
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
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new KitchenBindingModel
                    {
                        Id = id.Value,
                        KitchenName = textBoxName.Text
                    });
                }
                else
                {
                    service.AddElement(new KitchenBindingModel
                    {
                        KitchenName = textBoxName.Text
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
