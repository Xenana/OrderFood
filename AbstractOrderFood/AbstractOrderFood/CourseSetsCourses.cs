using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFood
{
    public class CourseSetsCourses
    {
        public int Id { get; set; }

        public int CourseSetsId { get; set; }

        public int CoursesId { get; set; }

        public int Count { get; set; }

        public virtual CourseSets CourseSets { get; set; }

        public virtual Courses Courses { get; set; }
    }
}
