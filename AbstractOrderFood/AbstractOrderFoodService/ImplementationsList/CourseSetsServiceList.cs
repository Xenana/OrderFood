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
            List<CourseSetsViewModel> result = source.CourseSet
                .Select(rec => new CourseSetsViewModel
                {
                    Id = rec.Id,
                    CourseSetsName = rec.CourseSetsName,
                    Cost = rec.Cost,
                    CourseSetsCourses = source.CourseSetsCourse
                            .Where(recCSC => recCSC.CourseSetsId == rec.Id)
                            .Select(recCSC => new CourseSetsCoursesViewModel
                            {
                                Id = recCSC.Id,
                                CourseSetsId = recCSC.CourseSetsId,
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

        public CourseSetsViewModel GetElement(int id)
        {
            CourseSets element = source.CourseSet.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CourseSetsViewModel
                {
                    Id = element.Id,
                    CourseSetsName = element.CourseSetsName,
                    Cost = element.Cost,
                    CourseSetsCourses = source.CourseSetsCourse
                            .Where(recCSC => recCSC.CourseSetsId == element.Id)
                            .Select(recCSC => new CourseSetsCoursesViewModel
                            {
                                Id = recCSC.Id,
                                CourseSetsId = recCSC.CourseSetsId,
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

        public void AddElement(CourseSetsBindingModel model)
        {
            CourseSets element = source.CourseSet.FirstOrDefault(rec => rec.CourseSetsName == model.CourseSetName);
            if (element != null)
            {
                throw new Exception("Уже есть набор блюд с таким названием");
            }
            int maxId = source.CourseSet.Count > 0 ? source.CourseSet.Max(rec => rec.Id) : 0;
            source.CourseSet.Add(new CourseSets
            {
                Id = maxId + 1,
                CourseSetsName = model.CourseSetName,
                Cost = model.Cost
            });
            int maxCSCId = source.CourseSetsCourse.Count > 0 ?
                                    source.CourseSetsCourse.Max(rec => rec.Id) : 0;
            var groupCourses = model.CourseSetsCourses
                                        .GroupBy(rec => rec.CoursesId)
                                        .Select(rec => new
                                        {
                                            CoursesId = rec.Key,
                                            Count = rec.Sum(r => r.Count)
                                        });
            foreach (var groupCourse in groupCourses)
            {
                source.CourseSetsCourse.Add(new CourseSetsCourses
                {
                    Id = ++maxCSCId,
                    CourseSetsId = maxId + 1,
                    CoursesId = groupCourse.CoursesId,
                    Count = groupCourse.Count
                });
            }
        }

        public void UpdElement(CourseSetsBindingModel model)
        {
            CourseSets element = source.CourseSet.FirstOrDefault(rec =>
                                        rec.CourseSetsName == model.CourseSetName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть набор блюд с таким названием");
            }
            element = source.CourseSet.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CourseSetsName = model.CourseSetName;
            element.Cost = model.Cost;

            int maxCSCId = source.CourseSetsCourse.Count > 0 ? source.CourseSetsCourse.Max(rec => rec.Id) : 0;
            var courIds = model.CourseSetsCourses.Select(rec => rec.CoursesId).Distinct();
            var updateCourses = source.CourseSetsCourse
                                            .Where(rec => rec.CourseSetsId == model.Id &&
                                            courIds.Contains(rec.CoursesId));
            foreach (var updateCourse in updateCourses)
            {
                updateCourse.Count = model.CourseSetsCourses
                                                .FirstOrDefault(rec => rec.Id == updateCourse.Id).Count;
            }
            source.CourseSetsCourse.RemoveAll(rec => rec.CourseSetsId == model.Id &&
                                        !courIds.Contains(rec.CoursesId));
            var groupCourses = model.CourseSetsCourses
                                        .Where(rec => rec.Id == 0)
                                        .GroupBy(rec => rec.CoursesId)
                                        .Select(rec => new
                                        {
                                            CoursesId = rec.Key,
                                            Count = rec.Sum(r => r.Count)
                                        });
            foreach (var groupCourse in groupCourses)
            {
                CourseSetsCourses elementCSC = source.CourseSetsCourse
                                        .FirstOrDefault(rec => rec.CourseSetsId == model.Id &&
                                                        rec.CoursesId == groupCourse.CoursesId);
                if (elementCSC != null)
                {
                    elementCSC.Count += groupCourse.Count;
                }
                else
                {
                    source.CourseSetsCourse.Add(new CourseSetsCourses
                    {
                        Id = ++maxCSCId,
                        CourseSetsId = model.Id,
                        CoursesId = groupCourse.CoursesId,
                        Count = groupCourse.Count
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            CourseSets element = source.CourseSet.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.CourseSetsCourse.RemoveAll(rec => rec.CourseSetsId == id);
                source.CourseSet.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
            
        }
    }
}
