using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.ImplementationOfInter
{
    public class BasketCourseViewModel
    {
        public int Id { get; set; }

        public int CustomersId { get; set; }

        public string CustomersFIO { get; set; }

        public int CourseSetsId { get; set; }

        public string CourseSetsName { get; set; }

        public int? ChefsId { get; set; }

        public string ChefsName { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateImplement { get; set; }
    }
}
