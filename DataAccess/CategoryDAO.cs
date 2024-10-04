using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDAO
    {
        public static async Task<List<Category>> GetCategoriesAsync()
        {
            List<Category> Categorys = new List<Category>();
            try
            {
                using (var context = new MyDbContext())
                {
                    Categorys = await context.Categories.AsNoTracking().ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Categorys;
        }
       
    }
}
