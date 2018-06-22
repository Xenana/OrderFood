using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.BindingModel
{
    public class CourseSetsCoursesBindingModel
    {
        public int Id { get; set; }

        public int CourseSetsId { get; set; }

        public int CoursesId { get; set; }

        public int Count { get; set; }

    }
}
