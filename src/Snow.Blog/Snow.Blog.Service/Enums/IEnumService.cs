using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snow.Blog.Model.Enum;

namespace Snow.Blog.Service.Enums
{
    public interface IEnumService
    {
        Task<Dictionary<int, string>> GetSortTypeSelectListAsync();
    }
}