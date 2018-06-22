using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationOfInter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.Interfaces
{
    public interface IOrderFoodService
    {
        List<BasketCourseViewModel> GetList();

        void CreateOrder(BasketCourseBindingModel model);

        void TakeOrderInWork(BasketCourseBindingModel model);

        void FinishOrder(int id);

        void PayOrder(int id);

        void PutCoursesOnKitchen(KitchenCoursesBindingModel model);
    }
}
