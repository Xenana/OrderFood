using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFood
{
    public class Courses
    {
        public int Id { get; set; }

        [Required]
        public string CoursesName { get; set; }

        [ForeignKey("CoursesId")]
        public virtual List<CourseSetsCourses> CourseSetsCourse { get; set; }

        [ForeignKey("CoursesId")]
        public virtual List<KitchenCourses> KitchenCourse { get; set; }
    }
}
