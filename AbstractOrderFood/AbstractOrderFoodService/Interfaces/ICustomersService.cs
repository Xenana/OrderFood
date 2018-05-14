using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationOfInter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.Interfaces
{
    public interface ICustomersService
    {
        List<CustomersViewModel> GetList();

        CustomersViewModel GetElement(int id);

        void AddElement(CustomersBindingModel model);

        void UpdElement(CustomersBindingModel model);

        void DelElement(int id);
    }
}
