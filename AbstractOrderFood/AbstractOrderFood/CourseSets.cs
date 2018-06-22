using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFood
{
    public class CourseSets
    {
        public int Id { get; set; }

        [Required]
        public string CourseSetsName { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [ForeignKey("CourseSetsId")]
        public virtual List<BasketCourse> Baskets { get; set; }

        [ForeignKey("CourseSetsId")]
        public virtual List<CourseSetsCourses> CourseSetsCourse { get; set; }
    }
}
