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
            List<CoursesViewModel> result = new List<CoursesViewModel>();
            for (int i = 0; i < source.Course.Count; ++i)
            {
                result.Add(new CoursesViewModel
                {
                    Id = source.Course[i].Id,
                    CoursesName = source.Course[i].CoursesName
                });
            }

            return result;
        }

        public CoursesViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Course.Count; ++i)
            {
                if (source.Course[i].Id == id)
                {
                    return new CoursesViewModel
                    {
                        Id = source.Course[i].Id,
                        CoursesName = source.Course[i].CoursesName
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(CoursesBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Course.Count; ++i)
            {
                if (source.Course[i].Id > maxId)
                {
                    maxId = source.Course[i].Id;
                }
                if (source.Course[i].CoursesName == model.CoursesName)
                {
                    throw new Exception("Уже есть блюдо с таким названием");
                }
            }
            source.Course.Add(new AbstractOrderFood.Courses
            {
                Id = maxId + 1,
                CoursesName = model.CoursesName
            });
        }

        public void UpdElement(CoursesBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Course.Count; ++i)
            {
                if (source.Course[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Course[i].CoursesName == model.CoursesName && source.Course[i].Id != model.Id)
                {
                    throw new Exception("Уже есть блюдо с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Course[index].CoursesName = model.CoursesName;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Course.Count; ++i)
            {
                if (source.Course[i].Id == id)
                {
                    source.Course.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
