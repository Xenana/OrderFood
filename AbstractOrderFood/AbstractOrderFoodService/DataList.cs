using AbstractOrderFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService
{
    class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Customers> Customer { get; set; }
        
        public List<Courses> Course { get; set; } 

        public List<Chefs> Chef { get; set; }

        public List<BasketCourse> Baskets { get; set; }

        public List<CourseSets> CourseSet { get; set; }

        public List<CourseSetsCourses> CourseSetsCourse { get; set; }

        public List<Kitchen> Kitchens { get; set; }

        public List<KitchenCourses> KitchenCourse { get; set; }

        private DataListSingleton()
        {
            Customer = new List<Customers>();
            Course = new List<Courses>();
            Chef = new List<Chefs>();
            Baskets = new List<BasketCourse>();
            CourseSet = new List<CourseSets>();
            CourseSetsCourse = new List<CourseSetsCourses>();
            Kitchens = new List<Kitchen>();
            KitchenCourse = new List<KitchenCourses>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }

            return instance;
        }
    }
}
