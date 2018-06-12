using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFood
{
    public class Kitchen
    {
        public int Id { get; set; }

        [Required]
        public string KitchenName { get; set; }

        [ForeignKey("KitchenId")]
        public virtual List<KitchenCourses> KitchenCourse { get; set; }
    }
}
