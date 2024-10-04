using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }

        public decimal UnitPrice { get; set; }
        public int UnitInstock { get; set; }
        public virtual Category? Categories { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
