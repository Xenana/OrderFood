using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractOrderFood
{
    public class Customers
    {
        public int Id { get; set; }

        [Required]
        public string CustomersFIO { get; set; }

        [ForeignKey("CustomersId")]
        public virtual List<BasketCourse> Basket { get; set; }
    }
}
