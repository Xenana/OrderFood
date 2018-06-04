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
    public class OrderFoodServiceList : IOrderFoodService
    {
        private DataListSingleton source;

        public OrderFoodServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<BasketCourseViewModel> GetList()
        {
            List<BasketCourseViewModel> result = source.Baskets
                .Select(rec => new BasketCourseViewModel
                {
                    Id = rec.Id,
                    CustomersId = rec.CustomersId,
                    CourseSetsId = rec.CourseSetsId,
                    ChefsId = rec.ChefsId,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    DateImplement = rec.DateImplement?.ToLongDateString(),
                    Status = rec.Status.ToString(),
                    CustomersFIO = source.Customer
                                    .FirstOrDefault(recCu => recCu.Id == rec.CustomersId)?.CustomersFIO,
                    CourseSetsName = source.CourseSet
                                    .FirstOrDefault(recCS => recCS.Id == rec.CourseSetsId)?.CourseSetsName,
                    ChefsName = source.Chef
                                    .FirstOrDefault(recCh => recCh.Id == rec.ChefsId)?.ChefsFIO
                })
                .ToList();
            return result;
        }

        public void CreateOrder(BasketCourseBindingModel model)
        {
            int maxId = source.Baskets.Count > 0 ? source.Baskets.Max(rec => rec.Id) : 0;
            source.Baskets.Add(new BasketCourse
            {
                Id = maxId + 1,
                CustomersId = model.CustomersId,
                CourseSetsId = model.CourseSetsId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = BasketCourseStatus.Принят
            });
        }

       public void TakeOrderInWork(BasketCourseBindingModel model)
        {
            BasketCourse element = source.Baskets.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            var courseSetsCourses = source.CourseSetsCourse.Where(rec => rec.CourseSetsId == element.CourseSetsId);
            foreach (var courseSetsCourse in courseSetsCourses)
            {
                int countOnKitchens = source.KitchenCourse
                                            .Where(rec => rec.CoursesId == courseSetsCourse.CoursesId)
                                            .Sum(rec => rec.Count);
                if (countOnKitchens < courseSetsCourse.Count * element.Count)
                {
                    var courseName = source.Course
                                    .FirstOrDefault(rec => rec.Id == courseSetsCourse.CoursesId);
                    throw new Exception("Не достаточно блюда " + courseName?.CoursesName +
                    " требуется " + courseSetsCourse.Count + ", в наличии " + countOnKitchens);
                }
            }
            // списываем
            foreach (var courseSetsCourse in courseSetsCourses)
            {
                int countOnKitchens = courseSetsCourse.Count * element.Count;
                var kitchenCourses = source.KitchenCourse
                                            .Where(rec => rec.CoursesId == courseSetsCourse.CoursesId);
                foreach (var kitchenCourse in kitchenCourses)
                {
                    // компонентов на одном слкаде может не хватать
                    if (kitchenCourse.Count >= countOnKitchens)
                        {
                            kitchenCourse.Count -= countOnKitchens;
                            break;
                        }
                    else
                    {
                        countOnKitchens -= kitchenCourse.Count;
                        kitchenCourse.Count = 0;
                    }
                }
            }
            element.ChefsId = model.ChefsId;
            element.DateImplement = DateTime.Now;
            element.Status = BasketCourseStatus.Выполняется;
        }

        public void FinishOrder(int id)
        {
            BasketCourse element = source.Baskets.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = BasketCourseStatus.Готов;
        }

        public void PayOrder(int id)
        {
            BasketCourse element = source.Baskets.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = BasketCourseStatus.Оплачен;
        }

        public void PutCoursesOnKitchen(KitchenCoursesBindingModel model)
        {
            KitchenCourses element = source.KitchenCourse
                                                .FirstOrDefault(rec => rec.KitchenId == model.KitchenId &&
                                                                    rec.CoursesId == model.CoursesId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.KitchenCourse.Count > 0 ? source.KitchenCourse.Max(rec => rec.Id) : 0;
                source.KitchenCourse.Add(new KitchenCourses
                {
                    Id = ++maxId,
                    KitchenId = model.KitchenId,
                    CoursesId = model.CoursesId,
                    Count = model.Count
                });
            }
        }
    }
}
