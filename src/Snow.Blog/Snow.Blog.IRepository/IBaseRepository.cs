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

        #region 增删改

        #region Dapper.Contrib

        /// <summary>
        /// 批量插入 Entity
        /// </summary>
        /// <param name="entity">实体集合</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>受影响的行数</returns>
        long Insert(TEntity entity, IDbConnection connection = null, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 批量插入 Entity
        /// </summary>
        /// <param name="entity">实体集合</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>受影响的行数</returns>
        Task<long> InsertAsync(TEntity entity, IDbConnection connection = null, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 批量插入 Entity
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>受影响的行数</returns>
        long Insert(IEnumerable<TEntity> entities, IDbConnection connection = null, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 批量插入 Entity
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>受影响的行数</returns>
        Task<long> InsertAsync(IEnumerable<TEntity> entities, IDbConnection connection = null, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 批量更新 Entity
        /// </summary>
        /// <param name="entity">实体集合</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>是否成功</returns>
        bool Update(TEntity entity, IDbConnection connection = null, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 批量更新 Entity
        /// </summary>
        /// <param name="entity">实体集合</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TEntity entity, IDbConnection connection = null, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 批量更新 Entity
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>是否成功</returns>
        bool Update(IEnumerable<TEntity> entities, IDbConnection connection = null, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 批量更新 Entity
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(IEnumerable<TEntity> entities, IDbConnection connection = null, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 批量删除 Entity
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>是否成功</returns>
        bool Delete(TEntity entity, IDbConnection connection = null, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 批量删除 Entity
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(TEntity entity, IDbConnection connection = null, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 批量删除 Entity
        /// </summary>
        /// <param name="entity">实体集合</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>是否成功</returns>
        bool Delete(IEnumerable<TEntity> entities, IDbConnection connection = null, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 批量删除 Entity
        /// </summary>
        /// <param name="entity">实体集合</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(IEnumerable<TEntity> entities, IDbConnection connection = null, IDbTransaction transaction = null, int? commandTimeout = null);

        #endregion Dapper.Contrib

        #endregion 增删改
    }
}