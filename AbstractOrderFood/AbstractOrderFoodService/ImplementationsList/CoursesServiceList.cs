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
    public class CoursesServiceList : ICoursesService
    {
        private DataListSingleton source;

        public CoursesServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<CoursesViewModel> GetList()
        {
            List<CoursesViewModel> result = source.Course
                .Select(rec => new CoursesViewModel
                {
                    Id = rec.Id,
                    CoursesName = rec.CoursesName
                })
                .ToList();

            return result;
        }

        public CoursesViewModel GetElement(int id)
        {
            Courses element = source.Course.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CoursesViewModel
                {
                    Id = element.Id,
                    CoursesName = element.CoursesName
                };
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(CoursesBindingModel model)
        {
            Courses element = source.Course.FirstOrDefault(rec => rec.CoursesName == model.CoursesName);
            if (element != null)
            {
                throw new Exception("Уже есть блюдо с таким названием");
            }
            int maxId = source.Course.Count > 0 ? source.Course.Max(rec => rec.Id) : 0;
            source.Course.Add(new Courses
            {
                Id = maxId + 1,
                CoursesName = model.CoursesName
            });
        }

        public void UpdElement(CoursesBindingModel model)
        {
            Courses element = source.Course.FirstOrDefault(rec =>
                                        rec.CoursesName == model.CoursesName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть блюдо с таким названием");
            }
            element = source.Course.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CoursesName = model.CoursesName;
        }

        public void DelElement(int id)
        {
            Courses element = source.Course.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Course.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
