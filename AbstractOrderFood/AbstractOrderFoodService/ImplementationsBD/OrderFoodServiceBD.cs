using AbstractOrderFood;
using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationOfInter;
using AbstractOrderFoodService.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.ImplementationsBD
{
    public class OrderFoodServiceBD : IOrderFoodService
    {
        private AbstractDbContext context;

        public OrderFoodServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<BasketCourseViewModel> GetList()
        {
            List<BasketCourseViewModel> result = context.Baskets
                .Select(rec => new BasketCourseViewModel
                {
                    Id = rec.Id,
                    CustomersId = rec.CustomersId,
                    CourseSetsId = rec.CourseSetsId,
                    ChefsId = rec.ChefsId,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    DateImplement = rec.DateImplement == null ? "" :
                                        SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    CustomersFIO = rec.Customers.CustomersFIO,
                    CourseSetsName = rec.CourseSets.CourseSetsName,
                    ChefsName = rec.Chefs.ChefsFIO
                })
                .ToList();
            return result;
        }

        public void CreateOrder(BasketCourseBindingModel model)
        {
            context.Baskets.Add(new BasketCourse
            {
                CustomersId = model.CustomersId,
                CourseSetsId = model.CourseSetsId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = BasketCourseStatus.Принят
            });
            context.SaveChanges();
        }

        public void TakeOrderInWork(BasketCourseBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    BasketCourse element = context.Baskets.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    var courseSetsCourse = context.CourseSetsCourse
                                                .Include(rec => rec.Courses)
                                                .Where(rec => rec.CourseSetsId == element.CourseSetsId);
                    // списываем
                    foreach (var courseSetCourse in courseSetsCourse)
                    {
                        int countOnKitchens = courseSetCourse.Count * element.Count;
                        var kitchenCourses = context.KitchenCourse
                                                    .Where(rec => rec.CoursesId == courseSetCourse.CoursesId);
                        foreach (var kitchenCourse in kitchenCourses)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (kitchenCourse.Count >= countOnKitchens)
                            {
                                kitchenCourse.Count -= countOnKitchens;
                                countOnKitchens = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnKitchens -= kitchenCourse.Count;
                                kitchenCourse.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnKitchens > 0)
                        {
                            throw new Exception("Не достаточно блюда " +
                                courseSetCourse.Courses.CoursesName + " требуется " +
                                courseSetCourse.Count + ", не хватает " + countOnKitchens);
                        }
                    }
                    element.ChefsId = model.ChefsId;
                    element.DateImplement = DateTime.Now;
                    element.Status = BasketCourseStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void FinishOrder(int id)
        {
            BasketCourse element = context.Baskets.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = BasketCourseStatus.Готов;
            context.SaveChanges();
        }

        public void PayOrder(int id)
        {
            BasketCourse element = context.Baskets.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = BasketCourseStatus.Оплачен;
            context.SaveChanges();
        }

        public void PutCoursesOnKitchen(KitchenCoursesBindingModel model)
        {
            KitchenCourses element = context.KitchenCourse
                                                .FirstOrDefault(rec => rec.KitchenId == model.KitchenId &&
                                                                    rec.CoursesId == model.CoursesId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.KitchenCourse.Add(new KitchenCourses
                {
                    KitchenId = model.KitchenId,
                    CoursesId = model.CoursesId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }
    }
}
