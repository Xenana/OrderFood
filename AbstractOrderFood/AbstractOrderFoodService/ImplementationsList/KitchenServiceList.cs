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
            List<KitchenViewModel> result = new List<KitchenViewModel>();
            for (int i = 0; i < source.Kitchens.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<KitchenCoursesViewModel> KitchenCourses = new List<KitchenCoursesViewModel>();
                for (int j = 0; j < source.KitchenCourse.Count; ++j)
                {
                    if (source.KitchenCourse[j].KitchenId == source.Kitchens[i].Id)
                    {
                        string courseName = string.Empty;
                        for (int k = 0; k < source.Course.Count; ++k)
                        {
                            if (source.CourseSetsCourse[j].CoursesId == source.Course[k].Id)
                            {
                                courseName = source.Course[k].CoursesName;
                                break;
                            }
                        }
                        KitchenCourses.Add(new KitchenCoursesViewModel
                        {
                            Id = source.KitchenCourse[j].Id,
                            KitchenId = source.KitchenCourse[j].KitchenId,
                            CoursesId = source.KitchenCourse[j].CoursesId,
                            CoursesName = courseName,
                            Count = source.KitchenCourse[j].Count
                        });
                    }
                }
                result.Add(new KitchenViewModel
                {
                    Id = source.Kitchens[i].Id,
                    KitchenName = source.Kitchens[i].KitchenName,
                    KitchenCourses = KitchenCourses
                });
            }
            return result;
        }

        public KitchenViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Kitchens.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<KitchenCoursesViewModel> KitchenCourses = new List<KitchenCoursesViewModel>();
                for (int j = 0; j < source.KitchenCourse.Count; ++j)
                {
                    if (source.KitchenCourse[j].KitchenId == source.Kitchens[i].Id)
                    {
                        string courseName = string.Empty;
                        for (int k = 0; k < source.Course.Count; ++k)
                        {
                            if (source.CourseSetsCourse[j].CoursesId == source.Course[k].Id)
                            {
                                courseName = source.Course[k].CoursesName;
                                break;
                            }
                        }
                        KitchenCourses.Add(new KitchenCoursesViewModel
                        {
                            Id = source.KitchenCourse[j].Id,
                            KitchenId = source.KitchenCourse[j].KitchenId,
                            CoursesId = source.KitchenCourse[j].CoursesId,
                            CoursesName = courseName,
                            Count = source.KitchenCourse[j].Count
                        });
                    }
                }
                if (source.Kitchens[i].Id == id)
                {
                    return new KitchenViewModel
                    {
                        Id = source.Kitchens[i].Id,
                        KitchenName = source.Kitchens[i].KitchenName,
                        KitchenCourses = KitchenCourses
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(KitchenBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Kitchens.Count; ++i)
            {
                if (source.Kitchens[i].Id > maxId)
                {
                    maxId = source.Kitchens[i].Id;
                }
                if (source.Kitchens[i].KitchenName == model.KitchenName)
                {
                    throw new Exception("Уже есть кухня с таким названием");
                }
            }
            source.Kitchens.Add(new Kitchen
            {
                Id = maxId + 1,
                KitchenName = model.KitchenName
            });
        }

        public void UpdElement(KitchenBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Kitchens.Count; ++i)
            {
                if (source.Kitchens[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Kitchens[i].KitchenName == model.KitchenName &&
                    source.Kitchens[i].Id != model.Id)
                {
                    throw new Exception("Уже есть кухня с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Kitchens[index].KitchenName = model.KitchenName;
        }

        public void DelElement(int id)
        {
            // при удалении удаляем все записи о компонентах на удаляемом складе
            for (int i = 0; i < source.KitchenCourse.Count; ++i)
            {
                if (source.KitchenCourse[i].KitchenId == id)
                {
                    source.KitchenCourse.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Kitchens.Count; ++i)
            {
                if (source.Kitchens[i].Id == id)
                {
                    source.Kitchens.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
