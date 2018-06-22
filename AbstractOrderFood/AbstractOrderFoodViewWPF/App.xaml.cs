using AbstractOrderFoodService;
using AbstractOrderFoodService.ImplementationsBD;
using AbstractOrderFoodService.ImplementationsList;
using AbstractOrderFoodService.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
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
            currentContainer.RegisterType<DbContext, OrderFoodDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomersService, CustomersServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICoursesService, CoursesServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IChefsService, ChefsServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICourseSetsService, CourseSetsServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IKitchenService, KitchenServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderFoodService, OrderFoodServiceBD>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
