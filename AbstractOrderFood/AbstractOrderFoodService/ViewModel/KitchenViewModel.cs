using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.ImplementationOfInter
{
    public class KitchenViewModel
    {
        public int Id { get; set; }

        public string KitchenName { get; set; }

        public List<KitchenCoursesViewModel> KitchenCourses { get; set; }

    }
}
