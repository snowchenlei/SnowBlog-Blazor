using System;
using System.Collections.Generic;
using System.Text;
using Snow.Blog.Model.Enum;

namespace Snow.Blog.Service.Bloggers.Dto
{
    public class BloggerEditDto
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 来源类型
        /// </summary>
        public int SourceType { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 编码后的Html
        /// </summary>
        public string HtmlEncoded { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}