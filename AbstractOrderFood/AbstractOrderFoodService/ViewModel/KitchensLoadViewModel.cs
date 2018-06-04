using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFoodService.ViewModel
{
    public class KitchensLoadViewModel
    {
        public string KitchenName { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<Tuple<string, int>> Courses { get; set; }
    }
}
