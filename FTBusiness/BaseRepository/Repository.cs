using FTData.BaseTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FTBusiness.BaseRepository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {

        internal readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            this.ctx = _dbContext;
        }

        public virtual IQueryable<TEntity> GetWithCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        
        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>();
        }
        public async Task<TEntity> Get<T>(T id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }


        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return _dbContext.Set<TEntity>()
                       .Where(filter);
        }
        public async Task Add(TEntity entity, bool isSaveChanges = true)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            if (isSaveChanges)
            {
                await Save();
            }
        }


        public async Task Change(TEntity entity, bool isSaveChanges = true)
        {
            _dbContext.Set<TEntity>().Update(entity);
            if (isSaveChanges)
            {
                await Save();
            }
        }

        public async Task Delete<T>(T id, bool isSaveChanges = true)
        {

            var entity = await Get(id);

          
            if (entity != null)
            {
               
                _dbContext.Set<TEntity>().Remove(entity);
                if (isSaveChanges)
                {
                    await Save();
                }
            }

        }
        public async Task DeleteRange<T>(List<T> ids, bool isSaveChanges = true)
        {
            foreach (var item in ids)
            {
                await Delete(item, isSaveChanges);
            }
        }
        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public DbContext GetDb()
        {
            return ctx;
        }

        public IQueryable<TEntity> GetNotDeleted()
        {
            object deleted = true;
            return GetAll().Where(x => typeof(TEntity).GetProperty("IsDeleted").GetValue(x) != deleted);
        }

        public async Task SoftDelete<T>(BaseTrackable<T> entity, string id)
        {
            entity.IsDeleted = true;
            entity.DeletedBy = id;
            entity.DeletedDate = DateTime.Now.ToUniversalTime();
            await Save();
        }

        public async Task RemoveRangeAsync(List<TEntity> entities, bool isSaveChanges = true)
        {
            _dbContext.RemoveRange(entities);
            if (isSaveChanges)
                await Save();


        }

        public async Task AddRangeAsync(List<TEntity> entities, bool isSaveChanges = true)
        {
            await _dbContext.AddRangeAsync(entities);
            if (isSaveChanges)
                await Save();
        }


        //Generic Crud Action
        //private void CrudActions(TEntity entity, ActionTypes ActionType)
        //{
        //    PropertyInfo property = null;
        //    var entityProperties = entity.GetType().GetProperties();
        //    switch (ActionType)
        //    {
        //        case ActionTypes.Created:
        //            property = entityProperties.FirstOrDefault(p => p.Name.ToLower() == "createdby");
        //            if (property != null)
        //                property.SetValue(entity, LmsSession.UserGuid.ToString());

        //            property = entityProperties.FirstOrDefault(p => p.Name.ToLower() == "createddate");
        //            if (property != null)
        //                property.SetValue(entity, DateTime.UtcNow);

        //            property = entityProperties.FirstOrDefault(p => p.Name.ToLower() == "isdeleted");
        //            if (property != null)
        //                property.SetValue(entity, false);
        //            break;


        //        case ActionTypes.Updated:
        //            property = entityProperties.FirstOrDefault(p => p.Name.ToLower() == "updatedby");
        //            if (property != null)
        //                property.SetValue(entity, LmsSession.UserGuid.ToString());

        //            property = entityProperties.FirstOrDefault(p => p.Name.ToLower() == "updateddate");
        //            if (property != null)
        //                property.SetValue(entity, DateTime.UtcNow);

        //            property = entityProperties.FirstOrDefault(p => p.Name.ToLower() == "isdeleted");
        //            if (property != null)
        //                property.SetValue(entity, false);
        //            break;


        //        case ActionTypes.Deleted:
        //            property = entityProperties.FirstOrDefault(p => p.Name.ToLower() == "deletedby");
        //            if (property != null)
        //                property.SetValue(entity, LmsSession.UserGuid.ToString());

        //            property = entityProperties.FirstOrDefault(p => p.Name.ToLower() == "deleteddate");
        //            if (property != null)
        //                property.SetValue(entity, DateTime.UtcNow);

        //            property = entityProperties.FirstOrDefault(p => p.Name.ToLower() == "isdeleted");
        //            if (property != null)
        //                property.SetValue(entity, true);
        //            break;

        //    }
        //}

        public DbContext ctx { get; set; }

        
    }
}
