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
            List<BasketCourseViewModel> result = new List<BasketCourseViewModel>();
            for (int i = 0; i < source.Baskets.Count; ++i)
            {
                string customerFIO = string.Empty;
                for(int j=0; j<source.Customer.Count; ++j)
                {
                    if(source.Customer[j].Id == source.Baskets[i].CustomersId)
                    {
                        customerFIO = source.Customer[j].CustomersFIO;
                        break;
                    }
                }
                string courseSetName = string.Empty;
                for(int j=0; j<source.CourseSet.Count; ++j)
                {
                    if(source.CourseSet[j].Id == source.Baskets[i].CourseSetsId)
                    {
                        courseSetName = source.CourseSet[j].CourseSetsName;
                        break;
                    }
                }
                string chefFIO = string.Empty;
                if (source.Baskets[i].ChefsId.HasValue)
                {
                    for(int j=0; j<source.Chef.Count; ++j)
                    {
                        if(source.Chef[j].Id == source.Baskets[i].ChefsId.Value)
                        {
                            chefFIO = source.Chef[j].ChefsFIO;
                            break;
                        }
                    }
                }
                result.Add(new BasketCourseViewModel
                {
                    Id = source.Baskets[i].Id,
                    CustomersId = source.Baskets[i].CustomersId,
                    CustomersFIO = customerFIO,
                    CourseSetsId = source.Baskets[i].CourseSetsId,
                    CourseSetsName = courseSetName,
                    ChefsId = source.Baskets[i].ChefsId,
                    ChefsName = chefFIO,
                    Count = source.Baskets[i].Count,
                    Sum = source.Baskets[i].Sum,
                    DateCreate = source.Baskets[i].DateCreate.ToLongDateString(),
                    DateImplement = source.Baskets[i].DateImplement?.ToLongDateString(),
                    Status = source.Baskets[i].Status.ToString()
                });
            }

            return result;
        }

        public void CreateOrder(BasketCourseBindingModel model)
        {
            int maxId = 0;
            for(int i=0; i<source.Baskets.Count; ++i)
            {
                if (source.Baskets[i].Id > maxId)
                {
                    maxId = source.Customer[i].Id;
                }
            }
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
            int index = -1;
            for (int i = 0; i < source.Baskets.Count; ++i)
            {
                if (source.Baskets[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            for (int i = 0; i < source.CourseSetsCourse.Count; ++i)
            {
                if (source.CourseSetsCourse[i].CourseSetsId == source.Baskets[index].CourseSetsId)
                {
                    int countOnKitchens= 0;
                    for (int j = 0; j < source.KitchenCourse.Count; ++j)
                    {
                        if (source.KitchenCourse[j].CoursesId == source.CourseSetsCourse[i].CoursesId)
                        {
                            countOnKitchens += source.KitchenCourse[j].Count;
                        }
                    }
                    if (countOnKitchens < source.CourseSetsCourse[i].Count * source.Baskets[index].Count)
                    {
                        for (int j = 0; j < source.Course.Count; ++j)
                        {
                            if (source.Course[j].Id == source.CourseSetsCourse[i].CoursesId)
                            {
                                throw new Exception("Не достаточно блюда " + source.Course[j].CoursesName +
                                    " требуется " + source.CourseSetsCourse[i].Count + ", в наличии " + countOnKitchens);
                            }
                        }
                    }
                }
            }
            // списываем
            for (int i = 0; i < source.CourseSetsCourse.Count; ++i)
            {
                if (source.CourseSetsCourse[i].CourseSetsId == source.Baskets[index].CourseSetsId)
                {
                    int countOnKitchens = source.CourseSetsCourse[i].Count * source.Baskets[index].Count;
                    for (int j = 0; j < source.KitchenCourse.Count; ++j)
                    {
                        if (source.KitchenCourse[j].CoursesId == source.CourseSetsCourse[i].CoursesId)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (source.KitchenCourse[j].Count >= countOnKitchens)
                            {
                                source.KitchenCourse[j].Count -= countOnKitchens;
                                break;
                            }
                            else
                            {
                                countOnKitchens -= source.KitchenCourse[j].Count;
                                source.KitchenCourse[j].Count = 0;
                            }
                        }
                    }
                }
            }
            source.Baskets[index].ChefsId = model.ChefsId;
            source.Baskets[index].DateImplement = DateTime.Now;
            source.Baskets[index].Status = BasketCourseStatus.Выполняется;
        }

        public void FinishOrder(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Baskets.Count; ++i)
            {
                if (source.Customer[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Baskets[index].Status = BasketCourseStatus.Готов;
        }

        public void PayOrder(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Baskets.Count; ++i)
            {
                if (source.Customer[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Baskets[index].Status = BasketCourseStatus.Оплачен;
        }

        public void PutCoursesOnKitchen(KitchenCoursesBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.KitchenCourse.Count; ++i)
            {
                if (source.KitchenCourse[i].KitchenId == model.KitchenId &&
                    source.KitchenCourse[i].KitchenId == model.KitchenId)
                {
                    source.KitchenCourse[i].Count += model.Count;
                    return;
                }
                if (source.KitchenCourse[i].Id > maxId)
                {
                    maxId = source.KitchenCourse[i].Id;
                }
            }
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
