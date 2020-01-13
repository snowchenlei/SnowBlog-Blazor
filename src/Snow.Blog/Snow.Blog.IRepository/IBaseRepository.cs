using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Snow.Blog.IRepository
{
    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, int> where TEntity : class
    {
    }

    public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        /// <summary>
        /// 获取Model-Key为int类型
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>实体</returns>
        Task<TEntity> GetAsync(TPrimaryKey id, IDbTransaction transaction = null, int? commandTimeout = null);

        Task<(IEnumerable<TEntity> items, int totalCount)> GetPageLoadAsync(Dictionary<string, object> wheres
             , int pageIndex, int pageSize, string[] asc = null, string[] desc = null);
    }
}