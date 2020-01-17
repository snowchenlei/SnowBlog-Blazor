using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snow.Blog.Common;

namespace Snow.Blog.Service.Enums
{
    public class EnumService : IEnumService
    {
        public async Task<Dictionary<int, string>> GetSortTypeSelectListAsync()
        {
            return await Task.FromResult(new Dictionary<int, string>()
            {
                [1] = "原创",
                [2] = "转载",
                [3] = "翻译"
            });
        }
    }
}