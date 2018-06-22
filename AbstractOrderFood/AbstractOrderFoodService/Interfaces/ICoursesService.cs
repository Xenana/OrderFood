using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationOfInter;
using AbstractOrderFoodService.ImplementationsList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.Interfaces
{
    public interface ICoursesService
    {
        List<CoursesViewModel> GetList();

        CoursesViewModel GetElement(int id);

        void AddElement(CoursesBindingModel model);

        void UpdElement(CoursesBindingModel model);

        void DelElement(int id);
    }
}
