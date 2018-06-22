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
            List<ChefsViewModel> result = new List<ChefsViewModel>();
            for (int i = 0; i < source.Chef.Count; ++i)
            {
                result.Add(new ChefsViewModel
                {
                    Id = source.Chef[i].Id,
                    ChefsFIO = source.Chef[i].ChefsFIO
                });
            }

            return result;
        }

        public ChefsViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Chef.Count; ++i)
            {
                if (source.Chef[i].Id == id)
                {
                    return new ChefsViewModel
                    {
                        Id = source.Chef[i].Id,
                        ChefsFIO = source.Chef[i].ChefsFIO
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(ChefsBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Chef.Count; ++i)
            {
                if (source.Chef[i].Id > maxId)
                {
                    maxId = source.Chef[i].Id;
                }
                if (source.Chef[i].ChefsFIO == model.ChefsFIO)
                {
                    throw new Exception("Уже есть шеф-повар с таким ФИО");
                }
            }
            source.Chef.Add(new AbstractOrderFood.Chefs
            {
                Id = maxId + 1,
                ChefsFIO = model.ChefsFIO
            });
        }
       
        public void UpdElement(ChefsBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Chef.Count; ++i)
            {
                if (source.Chef[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Chef[i].ChefsFIO == model.ChefsFIO &&
                    source.Chef[i].Id != model.Id)
                {
                    throw new Exception("Уже есть шеф-повар с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Chef[index].ChefsFIO = model.ChefsFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Chef.Count; ++i)
            {
                if (source.Chef[i].Id == id)
                {
                    source.Chef.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
