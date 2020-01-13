using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snow.Blog.IRepository;
using Snow.Blog.Model.DataBase;

namespace Snow.Blog.IDAL.Categories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Dictionary<int, string>> GetCategoriesAsync(bool isNav = true);
    }
}