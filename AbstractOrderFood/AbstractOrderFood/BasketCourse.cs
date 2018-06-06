using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFood
{
    public class BasketCourse
    {
        public int Id { get; set; }

        public int CustomersId { get; set; }

        public int CourseSetsId { get; set; }

        public int? ChefsId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public BasketCourseStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }
    }
}
