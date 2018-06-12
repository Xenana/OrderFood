using AbstractOrderFoodService.ImplementationsList;
using AbstractOrderFoodService.Interfaces;
using System;
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
