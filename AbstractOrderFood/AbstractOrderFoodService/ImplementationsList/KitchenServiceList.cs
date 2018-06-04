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
    public class KitchenServiceList : IKitchenService
    {
        private DataListSingleton source;

        public KitchenServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<KitchenViewModel> GetList()
        {
            List<KitchenViewModel> result = source.Kitchens
                .Select(rec => new KitchenViewModel
                {
                    Id = rec.Id,
                    KitchenName = rec.KitchenName,
                    KitchenCourses = source.KitchenCourse
                             .Where(recCSC => recCSC.KitchenId == rec.Id)
                             .Select(recCSC => new KitchenCoursesViewModel
                              {
                                 Id = recCSC.Id,
                                 KitchenId = recCSC.KitchenId,
                                 CoursesId = recCSC.CoursesId,
                                 CoursesName = source.Course
                                     .FirstOrDefault(recC => recC.Id == recCSC.CoursesId)?.CoursesName,
                                 Count = recCSC.Count
                             })
                             .ToList()
                 })
                 .ToList();
        
            return result;
        }

        public KitchenViewModel GetElement(int id)
        {
            Kitchen element = source.Kitchens.FirstOrDefault(rec => rec.Id == id);
                        if (element != null)
            {
                return new KitchenViewModel
                {
                    Id = element.Id,
                    KitchenName = element.KitchenName,
                    KitchenCourses = source.KitchenCourse
                             .Where(recCSC => recCSC.KitchenId == element.Id)
                             .Select(recCSC => new KitchenCoursesViewModel
                             {
                                 Id = recCSC.Id,
                                 KitchenId = recCSC.KitchenId,
                                 CoursesId = recCSC.CoursesId,
                                 CoursesName = source.Course
                                     .FirstOrDefault(recC => recC.Id == recCSC.CoursesId)?.CoursesName,
                                 Count = recCSC.Count
                             })
                             .ToList()
                 };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(KitchenBindingModel model)
        {
            Kitchen element = source.Kitchens.FirstOrDefault(rec => rec.KitchenName == model.KitchenName);
            if (element != null)
            {
                throw new Exception("Уже есть кухня с таким названием");
            }
            int maxId = source.Kitchens.Count > 0 ? source.Kitchens.Max(rec => rec.Id) : 0;
            source.Kitchens.Add(new Kitchen
            {
                Id = maxId + 1,
                KitchenName = model.KitchenName
            });
        }

        public void UpdElement(KitchenBindingModel model)
        {
            Kitchen element = source.Kitchens.FirstOrDefault(rec =>
                                        rec.KitchenName == model.KitchenName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть кухня с таким названием");
            }
            element = source.Kitchens.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.KitchenName = model.KitchenName;
        }

        public void DelElement(int id)
        {
            Kitchen element = source.Kitchens.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // при удалении удаляем все записи о компонентах на удаляемом складе
                source.KitchenCourse.RemoveAll(rec => rec.KitchenId == id);
                source.Kitchens.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
            
        }
    }
}
