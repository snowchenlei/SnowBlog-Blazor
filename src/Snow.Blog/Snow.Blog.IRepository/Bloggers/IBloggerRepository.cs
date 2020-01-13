using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snow.Blog.Model.DataBase;

namespace Snow.Blog.IRepository.Bloggers
{
    public interface IBloggerRepository : IBaseRepository<Blogger>
    {
        Task<int> GetCountAsync();

        Task<Blogger> GetDetailAsync(int id);
    }
}