using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractOrderFood;

namespace AbstractOrderFoodService
{
    [Table("OrderFoodDatabase_lab13")]
    public class AbstractDbContext : DbContext
    {
        public AbstractDbContext()
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Customers> Customer { get; set; }

        public virtual DbSet<Courses> Course { get; set; }

        public virtual DbSet<Chefs> Chef { get; set; }

        public virtual DbSet<BasketCourse> Baskets { get; set; }

        public virtual DbSet<CourseSets> CourseSet { get; set; }

        public virtual DbSet<CourseSetsCourses> CourseSetsCourse { get; set; }

        public virtual DbSet<Kitchen> Kitchens { get; set; }

        public virtual DbSet<KitchenCourses> KitchenCourse { get; set; }
    }
}
