using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationOfInter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.Interfaces
{
    public interface IKitchenService
    {
        List<KitchenViewModel> GetList();

        KitchenViewModel GetElement(int id);

        void AddElement(KitchenBindingModel model);

        void UpdElement(KitchenBindingModel model);

        void DelElement(int id);
    }
}
