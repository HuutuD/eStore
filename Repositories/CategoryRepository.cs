using BussinessObject;
using DataAccess;
using DataAccess.DTO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        async Task<List<Category>> ICategoryRepository.GetCategoriesAsync() => await CategoryDAO.GetCategoriesAsync();
       

    }
}
