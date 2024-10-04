using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class Order
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShipDate { get; set; }
        public int Freight { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }  
        public virtual Member? Members { get; set; }
    }
}
