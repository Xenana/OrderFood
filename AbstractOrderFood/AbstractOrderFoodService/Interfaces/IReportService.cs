using AbstractOrderFoodService.BindingModel;
using AbstractOrderFoodService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.Interfaces
{
    public interface IReportService
    {
        void SaveCourseSetPrice(ReportBindingModel model);

        List<KitchensLoadViewModel> GetKitchensLoad();

        void SaveKitchensLoad(ReportBindingModel model);

        List<CustomerBasketsModel> GetCustomerBaskets(ReportBindingModel model);

        void SaveCustomerBaskets(ReportBindingModel model);
    }
}
