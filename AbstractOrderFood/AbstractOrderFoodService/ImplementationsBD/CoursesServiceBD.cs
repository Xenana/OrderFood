using AbstractOrderFood;
using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationsList;
using AbstractOrderFoodService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.ImplementationsBD
{
    public class CoursesServiceBD : ICoursesService
    {
        private OrderFoodDbContext context;

        public CoursesServiceBD(OrderFoodDbContext context)
        {
            this.context = context;
        }

        public List<CoursesViewModel> GetList()
        {
            List<CoursesViewModel> result = context.Course
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
            Courses element = context.Course.FirstOrDefault(rec => rec.Id == id);
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
            Courses element = context.Course.FirstOrDefault(rec => rec.CoursesName == model.CoursesName);
            if (element != null)
            {
                throw new Exception("Уже есть блюдо с таким названием");
            }
            context.Course.Add(new Courses
            {
                CoursesName = model.CoursesName
            });
            context.SaveChanges();
        }

        public void UpdElement(CoursesBindingModel model)
        {
            Courses element = context.Course.FirstOrDefault(rec =>
                                        rec.CoursesName == model.CoursesName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть блюдо с таким названием");
            }
            element = context.Course.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CoursesName = model.CoursesName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Courses element = context.Course.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Course.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
