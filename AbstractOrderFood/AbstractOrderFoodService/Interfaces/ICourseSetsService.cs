using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationOfInter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.Interfaces
{
    public interface ICourseSetsService
    {
        List<CourseSetsViewModel> GetList();

        CourseSetsViewModel GetElement(int id);

        void AddElement(CourseSetsBindingModel model);

        void UpdElement(CourseSetsBindingModel model);

        void DelElement(int id);
    }
}
