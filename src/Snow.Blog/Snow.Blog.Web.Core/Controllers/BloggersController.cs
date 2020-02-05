using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snow.Blog.Common.Exceptions;
using Snow.Blog.Service.Bloggers;
using Snow.Blog.Service.Bloggers.Dto;
using Snow.Blog.Service.Dto;

namespace Snow.Blog.Web.Core.Controllers
{
    [ApiController]
    [Route("api/bloggers")]
    public class BloggersController : BaseController
    {
        private readonly IBloggerService _bloggerService;

        public BloggersController(
            IBloggerService bloggerService)
        {
            this._bloggerService = bloggerService;
        }

        [HttpGet(Name = "GetBloggerPage")]
        public async Task<IActionResult> GetPaged([FromQuery]GetBloggerInput input)
        {
            PagedResultDto<BloggerListDto> result = await _bloggerService.GetBloggersPagedAsync(input);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetBlogger")]
        public async Task<IActionResult> Get(int id)
        {
            BloggerEditDto blogger = await _bloggerService.GetBloggerForEditAsync(id);
            return Ok(blogger);
        }

        [HttpPost(Name = "PostBlogger")]
        public async Task<IActionResult> Post([FromBody]BloggerEditDto input)
        {
            BloggerListDto blogger = await _bloggerService.CreateBloggerAsync(input);
            return CreatedAtRoute("GetBlogger", new { id = blogger.Id }, blogger);
        }

        [HttpPut("{id}", Name = "PutBlogger")]
        public async Task<IActionResult> Put(int id, [FromBody]BloggerEditDto input)
        {
            input.Id = id;
            BloggerListDto blogger = await _bloggerService.UpdateBloggerAsync(input);
            return Ok(blogger);
        }

        [HttpDelete("{id}", Name = "DeleteBlogger")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bloggerService.DeleteBloggerAsync(id);
            return NoContent();
        }
    }
}
