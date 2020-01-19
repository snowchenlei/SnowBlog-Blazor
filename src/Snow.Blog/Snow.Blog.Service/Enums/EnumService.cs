using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snow.Blog.Common;
using Snow.Blog.Model.Enum;

namespace Snow.Blog.Service.Enums
{
    public class EnumService : IEnumService
    {
        public async Task<Dictionary<int, string>> GetSortTypeSelectListAsync()
        {
            return await Task.FromResult(new Dictionary<int, string>()
            {
                [(int)SourceType.creator] = "原创",
                [(int)SourceType.reprinted] = "转载",
                [(int)SourceType.translate] = "翻译"
            });
        }
    }
}