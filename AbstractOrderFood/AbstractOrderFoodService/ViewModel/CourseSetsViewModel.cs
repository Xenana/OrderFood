using AbstractOrderFoodService.BindingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.ImplementationOfInter
{
    public class CourseSetsViewModel
    {
        public int Id { get; set; }

        public string CourseSetsName { get; set; }

        public decimal Cost { get; set; }

        public List<CourseSetsCoursesViewModel> CourseSetsCourses { get; set; }

    }
}
