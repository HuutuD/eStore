using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitInstock { get; set; }
        public string CategoryName { get; set; }
    }
}
