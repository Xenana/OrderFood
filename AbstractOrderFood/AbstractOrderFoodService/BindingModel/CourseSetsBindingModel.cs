using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.BindingModel
{
    public class CourseSetsBindingModel
    {
        public int Id { get; set; }

        public string CourseSetName { get; set; }

        public decimal Cost { get; set; }

        public List<CourseSetsCoursesBindingModel> CourseSetsCourses { get; set; }
    }
}
