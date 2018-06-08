using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFood
{
    public class Chefs
    {
        public int Id { get; set; }

        [Required]
        public string ChefsFIO { get; set; }

        [ForeignKey("ChefsId")]
        public virtual List<BasketCourse> Baskets { get; set; }
    }
}
