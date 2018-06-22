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
    public class ChefsServiceBD : IChefsService
    {
        private OrderFoodDbContext context;

        public ChefsServiceBD(OrderFoodDbContext context)
        {
            this.context = context;
        }

        public List<ChefsViewModel> GetList()
        {
            List<ChefsViewModel> result = context.Chef
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
            Chefs element = context.Chef.FirstOrDefault(rec => rec.Id == id);
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
            Chefs element = context.Chef.FirstOrDefault(rec => rec.ChefsFIO == model.ChefsFIO);
            if (element != null)
            {
                throw new Exception("Уже есть шеф-повар с таким ФИО");
            }
            context.Chef.Add(new Chefs
            {
                ChefsFIO = model.ChefsFIO
            });
            context.SaveChanges();
        }

        public void UpdElement(ChefsBindingModel model)
        {
            Chefs element = context.Chef.FirstOrDefault(rec =>
                                        rec.ChefsFIO == model.ChefsFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть шеф-повар с таким ФИО");
            }
            element = context.Chef.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ChefsFIO = model.ChefsFIO;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Chefs element = context.Chef.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Chef.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
