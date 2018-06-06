using AbstractOrderFoodService.ImplementationsList;
using AbstractOrderFoodService.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace AbstractOrderFoodViewWPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var container = BuildUnityContainer();

            var application = new App();
            application.Run(container.Resolve<FormOrderFood>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<ICustomersService, CustomersServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICoursesService, CoursesServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IChefsService, ChefsServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICourseSetsService, CourseSetsServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IKitchenService, KitchenServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderFoodService, OrderFoodServiceList>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
