using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snow.Blog.Service.Bloggers.Dto;
using Snow.Blog.Service.Dto;

namespace Snow.Blog.Service.Bloggers
{
    public interface IBloggerService
    {
        Task<PagedResultDto<BloggerListDto>> GetBloggersPagedAsync(GetBloggerInput input);

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<BloggerDetailDto> GetBloggerDetailAsync(int id);

        /// <summary>
        /// 获取修改数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<BloggerEditDto> GetBloggerForEditAsync(int id);

        Task<int> GetCountAsync();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BloggerListDto> CreateBloggerAsync(BloggerEditDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BloggerListDto> UpdateBloggerAsync(BloggerEditDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task DeleteBloggerAsync(int id);
    }
}