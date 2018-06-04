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
    public class ChefsServiceList : IChefsService
    {
        private DataListSingleton source;

        public ChefsServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ChefsViewModel> GetList()
        {
            List<ChefsViewModel> result = source.Chef
                .Select(rec => new ChefsViewModel
                {
                    Id = rec.Id,
                    ChefsFIO = rec.ChefsFIO
                })
                .ToList();
            return result;
        }

        public ChefsViewModel GetElement(int id)
        {
            Chefs element = source.Chef.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ChefsViewModel
                {
                    Id = element.Id,
                    ChefsFIO = element.ChefsFIO
                };
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(ChefsBindingModel model)
        {
            Chefs element = source.Chef.FirstOrDefault(rec => rec.ChefsFIO == model.ChefsFIO);
            if (element != null)
            {
                throw new Exception("Уже есть шеф-повар с таким ФИО");
            }
            int maxId = source.Chef.Count > 0 ? source.Chef.Max(rec => rec.Id) : 0;
            source.Chef.Add(new Chefs
            {
                Id = maxId + 1,
                ChefsFIO = model.ChefsFIO
            });
        }
       
        public void UpdElement(ChefsBindingModel model)
        {
            Chefs element = source.Chef.FirstOrDefault(rec =>
                                        rec.ChefsFIO == model.ChefsFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть шеф-повар с таким ФИО");
            }
            element = source.Chef.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ChefsFIO = model.ChefsFIO;
        }

        public void DelElement(int id)
        {
            Chefs element = source.Chef.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Chef.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
