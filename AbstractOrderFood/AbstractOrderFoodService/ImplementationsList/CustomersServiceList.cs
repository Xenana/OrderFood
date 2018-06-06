using AbstractOrderFood;
using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationOfInter;
using AbstractOrderFoodService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.ImplementationsList
{
    public class CustomersServiceList : ICustomersService
    {
        private DataListSingleton source;

        public CustomersServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<CustomersViewModel> GetList()
        {
            List<CustomersViewModel> result = source.Customer
                .Select(rec => new CustomersViewModel
                {
                    Id = rec.Id,
                    CustomersFIO = rec.CustomersFIO
                })
            .ToList();
            return result;

            /*List<CustomersViewModel> result1 = (from rec in source.Customer
                                                select new CustomersViewModel
                                                {
                                                    Id = rec.Id,
                                                    CustomersFIO = rec.CustomersFIO
                                                }).ToList();
            return result1;*/
        }

        public CustomersViewModel GetElement(int id)
        {
            Customers element = source.Customer.FirstOrDefault(rec => rec.Id == id);
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
            Customers element = source.Customer.FirstOrDefault(rec => rec.CustomersFIO == model.CustomersFIO);
            if (element != null)
            {
                throw new Exception("Уже есть заказчик с таким ФИО");
            }
            int maxId = source.Customer.Count > 0 ? source.Customer.Max(rec => rec.Id) : 0;
            source.Customer.Add(new Customers
            {
                Id = maxId + 1,
                CustomersFIO = model.CustomersFIO
            });
        }

        public void UpdElement(CustomersBindingModel model)
        {
            Customers element = source.Customer.FirstOrDefault(rec =>
                                       rec.CustomersFIO == model.CustomersFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть заказчик с таким ФИО");
            }
            element = source.Customer.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CustomersFIO = model.CustomersFIO;
        }

        public void DelElement(int id)
        {
            Customers element = source.Customer.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Customer.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}

