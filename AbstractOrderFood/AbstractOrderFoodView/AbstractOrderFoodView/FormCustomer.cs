using AbstractOrderFoodService.Interfaces;
using AbstractOrderFoodService.ImplementationOfInter;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
using AbstractOrderFoodService.BindingModel;

namespace AbstractOrderFoodView
{
    public partial class FormCustomer : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ICustomersService service;

        private int? id;

        public FormCustomer(ICustomersService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CustomersViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        FIO_Cust.Text = view.CustomersFIO;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Save_Cust_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FIO_Cust.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new CustomersBindingModel
                    {
                        Id = id.Value,
                        CustomersFIO = FIO_Cust.Text
                    });
                }
                else
                {
                    service.AddElement(new CustomersBindingModel
                    {
                        CustomersFIO = FIO_Cust.Text
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

        private void Cancel_Cust_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
