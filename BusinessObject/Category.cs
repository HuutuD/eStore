using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class Category
    {
        [Required(ErrorMessage = "The ID cannot the blank.")]
        public int Id { get; set; }

        public string Name { get; set; } 
        public virtual ICollection<Product>? Products { get; set; }

    }
}
