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
    public class CourseSetsServiceBD : ICourseSetsService
    {
        private OrderFoodDbContext context;

        public CourseSetsServiceBD(OrderFoodDbContext context)
        {
            this.context = context;
        }

        public List<CourseSetsViewModel> GetList()
        {
            List<CourseSetsViewModel> result = context.CourseSet
                .Select(rec => new CourseSetsViewModel
                {
                    Id = rec.Id,
                    CourseSetsName = rec.CourseSetsName,
                    Cost = rec.Cost,
                    CourseSetsCourses = context.CourseSetsCourse
                            .Where(recCSC => recCSC.CourseSetsId == rec.Id)
                            .Select(recCSC => new CourseSetsCoursesViewModel
                            {
                                Id = recCSC.Id,
                                CourseSetsId = recCSC.CourseSetsId,
                                CoursesId = recCSC.CoursesId,
                                CoursesName = recCSC.Courses.CoursesName,
                                Count = recCSC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public CourseSetsViewModel GetElement(int id)
        {
            CourseSets element = context.CourseSet.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CourseSetsViewModel
                {
                    Id = element.Id,
                    CourseSetsName = element.CourseSetsName,
                    Cost = element.Cost,
                    CourseSetsCourses = context.CourseSetsCourse
                            .Where(recCSC => recCSC.CourseSetsId == element.Id)
                            .Select(recCSC => new CourseSetsCoursesViewModel
                            {
                                Id = recCSC.Id,
                                CourseSetsId = recCSC.CourseSetsId,
                                CoursesId = recCSC.CoursesId,
                                CoursesName = recCSC.Courses.CoursesName,
                                Count = recCSC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CourseSetsBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    CourseSets element = context.CourseSet.FirstOrDefault(rec => rec.CourseSetsName == model.CourseSetName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть набор блюд с таким названием");
                    }
                    element = new CourseSets
                    {
                        CourseSetsName = model.CourseSetName,
                        Cost = model.Cost
                    };
                    context.CourseSet.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupComponents = model.CourseSetsCourses
                                                .GroupBy(rec => rec.CoursesId)
                                                .Select(rec => new
                                                {
                                                    CoursesId = rec.Key,
                                                    Count = rec.Sum(r => r.Count)
                                                });
                    // добавляем компоненты
                    foreach (var groupComponent in groupComponents)
                    {
                        context.CourseSetsCourse.Add(new CourseSetsCourses
                        {
                            CourseSetsId = element.Id,
                            CoursesId = groupComponent.CoursesId,
                            Count = groupComponent.Count
                        });
                        context.SaveChanges();
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

        public void UpdElement(CourseSetsBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    CourseSets element = context.CourseSet.FirstOrDefault(rec =>
                                        rec.CourseSetsName == model.CourseSetName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть набор блюд с таким названием");
                    }
                    element = context.CourseSet.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.CourseSetsName = model.CourseSetName;
                    element.Cost = model.Cost;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты
                    var courIds = model.CourseSetsCourses.Select(rec => rec.CoursesId).Distinct();
                    var updateCourses = context.CourseSetsCourse
                                                    .Where(rec => rec.CourseSetsId == model.Id &&
                                                        courIds.Contains(rec.CoursesId));
                    foreach (var updateCourse in updateCourses)
                    {
                        updateCourse.Count = model.CourseSetsCourses
                                                        .FirstOrDefault(rec => rec.Id == updateCourse.Id).Count;
                    }
                    context.SaveChanges();
                    context.CourseSetsCourse.RemoveRange(
                                        context.CourseSetsCourse.Where(rec => rec.CourseSetsId == model.Id &&
                                                                            !courIds.Contains(rec.CoursesId)));
                    context.SaveChanges();
                    // новые записи
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
                        CourseSetsCourses elementCSC = context.CourseSetsCourse
                                                .FirstOrDefault(rec => rec.CourseSetsId == model.Id &&
                                                                rec.CoursesId == groupCourse.CoursesId);
                        if (elementCSC != null)
                        {
                            elementCSC.Count += groupCourse.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.CourseSetsCourse.Add(new CourseSetsCourses
                            {
                                CourseSetsId = model.Id,
                                CoursesId = groupCourse.CoursesId,
                                Count = groupCourse.Count
                            });
                            context.SaveChanges();
                        }
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

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    CourseSets element = context.CourseSet.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.CourseSetsCourse.RemoveRange(
                                            context.CourseSetsCourse.Where(rec => rec.CourseSetsId == id));
                        context.CourseSet.Remove(element);
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
