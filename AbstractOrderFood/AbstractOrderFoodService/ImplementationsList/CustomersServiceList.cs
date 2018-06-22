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
            List<CustomersViewModel> result = new List<CustomersViewModel>();
            for(int i=0; i<source.Customer.Count; ++i)
            {
                result.Add(new CustomersViewModel
                {
                    Id = source.Customer[i].Id,
                    CustomersFIO = source.Customer[i].CustomersFIO
                });
            }

            return result;
        }

        public CustomersViewModel GetElement(int id)
        {
            for(int i=0; i<source.Customer.Count; ++i)
            {
                if(source.Customer[i].Id == id)
                {
                    return new CustomersViewModel
                    {
                        Id = source.Customer[i].Id,
                        CustomersFIO = source.Customer[i].CustomersFIO
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(CustomersBindingModel model)
        {
            int maxId = 0;
            for(int i=0;i<source.Customer.Count; ++i)
            {
                if (source.Customer[i].Id > maxId)
                {
                    maxId = source.Customer[i].Id;
                }
                if(source.Customer[i].CustomersFIO == model.CustomersFIO)
                {
                    throw new Exception("Уже есть заказчик с таким ФИО");
                }
            }
            source.Customer.Add(new AbstractOrderFood.Customers
            {
                Id = maxId + 1,
                CustomersFIO = model.CustomersFIO
            });
        }

        public void UpdElement(CustomersBindingModel model)
        {
            int index = -1;
            for(int i=0; i<source.Customer.Count; ++i)
            {
                if(source.Customer[i].Id == model.Id)
                {
                    index = i;
                }
                if(source.Customer[i].CustomersFIO == model.CustomersFIO && source.Customer[i].Id != model.Id)
                {
                    throw new Exception("Уже есть заказчик с таким ФИО");
                }
            }
            if(index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Customer[index].CustomersFIO = model.CustomersFIO;
        }

        public void DelElement(int id)
        {
            for(int i=0; i<source.Customer.Count; ++i)
            {
                if(source.Customer[i].Id == id)
                {
                    source.Customer.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
