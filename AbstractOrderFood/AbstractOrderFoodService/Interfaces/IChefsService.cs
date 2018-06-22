using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ImplementationOfInter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.Interfaces
{
    public interface IChefsService
    {
        List<ChefsViewModel> GetList();

        ChefsViewModel GetElement(int id);

        void AddElement(ChefsBindingModel model);

        void UpdElement(ChefsBindingModel model);

        void DelElement(int id);
    }
}
