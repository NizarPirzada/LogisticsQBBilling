using FTData.BaseTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FTBusiness.BaseRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetNotDeleted();
        Task<TEntity> Get<T>(T id);

        Task Add(TEntity entity, bool isSaveChanges = true);
        IQueryable<T> GetAll<T>() where T : class;

        Task Change(TEntity entity, bool isSaveChanges = true);

        Task Delete<T>(T id, bool isSaveChanges = true);
        Task DeleteRange<T>(List<T> ids, bool isSaveChanges = true);
        Task RemoveRangeAsync(List<TEntity> entities, bool isSaveChanges = true);
        Task AddRangeAsync(List<TEntity> entities, bool isSaveChanges = true);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        Task Save();
        DbContext GetDb();
        Task SoftDelete<T>(BaseTrackable<T> entity, string id);
    }
}
