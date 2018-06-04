using AbstractOrderFoodService;
using AbstractOrderFoodService.ImplementationsBD;
using AbstractOrderFoodService.ImplementationsList;
using AbstractOrderFoodService.Interfaces;
using System;
using System.Data.Entity;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractOrderFoodView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormOrderFood>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractDbContext>(new HierarchicalLifetimeManager());
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
