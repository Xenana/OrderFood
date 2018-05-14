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
    public class CourseSetsServiceList : ICourseSetsService
    {
        private DataListSingleton source;

        public CourseSetsServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<CourseSetsViewModel> GetList()
        {
            List<CourseSetsViewModel> result = new List<CourseSetsViewModel>();
            for (int i = 0; i < source.CourseSet.Count; ++i)
            {
                List<CourseSetsCoursesViewModel> courseSetsCourses = new List<CourseSetsCoursesViewModel>();
                for (int j = 0; j < source.CourseSetsCourse.Count; ++j)
                {
                    if (source.CourseSetsCourse[j].CourseSetsId == source.CourseSet[i].Id)
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
                        courseSetsCourses.Add(new CourseSetsCoursesViewModel
                        {
                            Id = source.CourseSetsCourse[j].Id,
                            CourseSetsId = source.CourseSetsCourse[j].CourseSetsId,
                            CoursesId = source.CourseSetsCourse[j].CoursesId,
                            CoursesName = courseName,
                            Count = source.CourseSetsCourse[j].Count
                        });
                    }
                }
                result.Add(new CourseSetsViewModel
                {
                    Id = source.CourseSet[i].Id,
                    CourseSetsName = source.CourseSet[i].CourseSetsName,
                    Cost = source.CourseSet[i].Cost,
                    CourseSetsCourses = courseSetsCourses
                });
            }
            return result;
        }

        public CourseSetsViewModel GetElement(int id)
        {
            for (int i = 0; i < source.CourseSet.Count; ++i)
            {
                List<CourseSetsCoursesViewModel> courseSetsCourses = new List<CourseSetsCoursesViewModel>();
                for (int j = 0; j < source.CourseSetsCourse.Count; ++j)
                {
                    if (source.CourseSetsCourse[j].CourseSetsId == source.CourseSet[i].Id)
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
                        courseSetsCourses.Add(new CourseSetsCoursesViewModel
                        {
                            Id = source.CourseSetsCourse[j].Id,
                            CourseSetsId = source.CourseSetsCourse[j].CourseSetsId,
                            CoursesId = source.CourseSetsCourse[j].CoursesId,
                            CoursesName = courseName,
                            Count = source.CourseSetsCourse[j].Count
                        });
                    }
                }
                if (source.CourseSet[i].Id == id)
                {
                    return new CourseSetsViewModel
                    {
                        Id = source.CourseSet[i].Id,
                        CourseSetsName = source.CourseSet[i].CourseSetsName,
                        Cost = source.CourseSet[i].Cost,
                        CourseSetsCourses = courseSetsCourses
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(CourseSetsBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.CourseSet.Count; ++i)
            {
                if (source.CourseSet[i].Id > maxId)
                {
                    maxId = source.CourseSet[i].Id;
                }
                if (source.CourseSet[i].CourseSetsName == model.CourseSetName)
                {
                    throw new Exception("Уже есть набор блюд с таким названием");
                }
            }
            source.CourseSet.Add(new CourseSets
            {
                Id = maxId + 1,
                CourseSetsName = model.CourseSetName,
                Cost = model.Cost
            });
            int maxCSCId = 0;
            for (int i = 0; i < source.CourseSetsCourse.Count; ++i)
            {
                if (source.CourseSetsCourse[i].Id > maxCSCId)
                {
                    maxCSCId = source.CourseSetsCourse[i].Id;
                }
            }
            for (int i = 0; i < model.CourseSetsCourses.Count; ++i)
            {
                for (int j = 1; j < model.CourseSetsCourses.Count; ++j)
                {
                    if (model.CourseSetsCourses[i].CoursesId ==
                        model.CourseSetsCourses[j].CoursesId)
                    {
                        model.CourseSetsCourses[i].Count +=
                            model.CourseSetsCourses[j].Count;
                        model.CourseSetsCourses.RemoveAt(j--);
                    }
                }
            }
            for (int i = 0; i < model.CourseSetsCourses.Count; ++i)
            {
                source.CourseSetsCourse.Add(new CourseSetsCourses
                {
                    Id = ++maxCSCId,
                    CourseSetsId = maxId + 1,
                    CoursesId = model.CourseSetsCourses[i].CoursesId,
                    Count = model.CourseSetsCourses[i].Count
                });
            }
        }

        public void UpdElement(CourseSetsBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.CourseSet.Count; ++i)
            {
                if (source.CourseSet[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.CourseSet[i].CourseSetsName == model.CourseSetName &&
                    source.CourseSet[i].Id != model.Id)
                {
                    throw new Exception("Уже есть набор блюд с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.CourseSet[index].CourseSetsName = model.CourseSetName;
            int maxCSCId = 0;
            for (int i = 0; i < source.CourseSetsCourse.Count; ++i)
            {
                if (source.CourseSetsCourse[i].Id > maxCSCId)
                {
                    maxCSCId = source.CourseSetsCourse[i].Id;
                }
            }
            for (int i = 0; i < source.CourseSetsCourse.Count; ++i)
            {
                if (source.CourseSetsCourse[i].CourseSetsId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.CourseSetsCourses.Count; ++j)
                    {
                        if (source.CourseSetsCourse[i].Id == model.CourseSetsCourses[j].Id)
                        {
                            source.CourseSetsCourse[i].Count = model.CourseSetsCourses[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        source.CourseSetsCourse.RemoveAt(i--);
                    }
                }
            }
            for (int i = 0; i < model.CourseSetsCourses.Count; ++i)
            {
                if (model.CourseSetsCourses[i].Id == 0)
                {
                    for (int j = 0; j < source.CourseSetsCourse.Count; ++j)
                    {
                        if (source.CourseSetsCourse[j].CourseSetsId == model.Id &&
                            source.CourseSetsCourse[j].CourseSetsId == model.CourseSetsCourses[i].CoursesId)
                        {
                            source.CourseSetsCourse[j].Count += model.CourseSetsCourses[i].Count;
                            model.CourseSetsCourses[i].Id = source.CourseSetsCourse[j].Id;
                            break;
                        }
                    }
                    if (model.CourseSetsCourses[i].Id == 0)
                    {
                        source.CourseSetsCourse.Add(new CourseSetsCourses
                        {
                            Id = ++maxCSCId,
                            CourseSetsId = model.Id,
                            CoursesId = model.CourseSetsCourses[i].CoursesId,
                            Count = model.CourseSetsCourses[i].Count
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.CourseSetsCourse.Count; ++i)
            {
                if (source.CourseSetsCourse[i].CourseSetsId == id)
                {
                    source.CourseSetsCourse.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.CourseSet.Count; ++i)
            {
                if (source.CourseSet[i].Id == id)
                {
                    source.CourseSet.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
