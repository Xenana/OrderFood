using AbstractOrderFood;
using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationOfInter;
using AbstractOrderFoodService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.ImplementationsBD
{
    public class CustomersServiceBD : ICustomersService
    {
        private AbstractDbContext context;

        public CustomersServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<CustomersViewModel> GetList()
        {
            List<CustomersViewModel> result = context.Customer
                .Select(rec => new CustomersViewModel
                {
                    Id = rec.Id,
                    CustomersFIO = rec.CustomersFIO
                })
                .ToList();
            return result;
        }

        public CustomersViewModel GetElement(int id)
        {
            Customers element = context.Customer.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CustomersViewModel
                {
                    Id = element.Id,
                    CustomersFIO = element.CustomersFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CustomersBindingModel model)
        {
            Customers element = context.Customer.FirstOrDefault(rec => rec.CustomersFIO == model.CustomersFIO);
            if (element != null)
            {
                throw new Exception("Уже есть заказчик с таким ФИО");
            }
            context.Customer.Add(new Customers
            {
                CustomersFIO = model.CustomersFIO
            });
            context.SaveChanges();
        }

        public void UpdElement(CustomersBindingModel model)
        {
            Customers element = context.Customer.FirstOrDefault(rec =>
                                    rec.CustomersFIO == model.CustomersFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть заказчик с таким ФИО");
            }
            element = context.Customer.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CustomersFIO = model.CustomersFIO;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Customers element = context.Customer.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Customer.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}

