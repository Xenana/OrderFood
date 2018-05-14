using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.BindingModel
{
    public class KitchenCoursesBindingModel
    {
        public int Id { get; set; }

        public int KitchenId { get; set; }

        public int CoursesId { get; set; }

        public int Count { get; set; }

    }
}
