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
    public class KitchenServiceBD : IKitchenService
    {
        private OrderFoodDbContext context;

        public KitchenServiceBD(OrderFoodDbContext context)
        {
            this.context = context;
        }

        public List<KitchenViewModel> GetList()
        {
            List<KitchenViewModel> result = context.Kitchens
                .Select(rec => new KitchenViewModel
                {
                    Id = rec.Id,
                    KitchenName = rec.KitchenName,
                    KitchenCourses = context.KitchenCourse
                            .Where(recCSC => recCSC.KitchenId == rec.Id)
                            .Select(recCSC => new KitchenCoursesViewModel
                            {
                                Id = recCSC.Id,
                                KitchenId = recCSC.KitchenId,
                                CoursesId = recCSC.CoursesId,
                                CoursesName = recCSC.Courses.CoursesName,
                                Count = recCSC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public KitchenViewModel GetElement(int id)
        {
            Kitchen element = context.Kitchens.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new KitchenViewModel
                {
                    Id = element.Id,
                    KitchenName = element.KitchenName,
                    KitchenCourses = context.KitchenCourse
                            .Where(recCSC => recCSC.KitchenId == element.Id)
                            .Select(recCSC => new KitchenCoursesViewModel
                            {
                                Id = recCSC.Id,
                                KitchenId = recCSC.KitchenId,
                                CoursesId = recCSC.CoursesId,
                                CoursesName = recCSC.Courses.CoursesName,
                                Count = recCSC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(KitchenBindingModel model)
        {
            Kitchen element = context.Kitchens.FirstOrDefault(rec => rec.KitchenName == model.KitchenName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            context.Kitchens.Add(new Kitchen
            {
                KitchenName = model.KitchenName
            });
            context.SaveChanges();
        }

        public void UpdElement(KitchenBindingModel model)
        {
            Kitchen element = context.Kitchens.FirstOrDefault(rec =>
                                        rec.KitchenName == model.KitchenName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.Kitchens.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.KitchenName = model.KitchenName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Kitchen element = context.Kitchens.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // при удалении удаляем все записи о компонентах на удаляемом складе
                        context.KitchenCourse.RemoveRange(
                                            context.KitchenCourse.Where(rec => rec.KitchenId == id));
                        context.Kitchens.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
