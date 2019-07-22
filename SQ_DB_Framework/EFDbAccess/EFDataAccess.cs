using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using SQ_DB_Framework.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQ_DB_Framework.EFDbAccess
{
    public class EFDataAccess<TEntity> where TEntity : EntityBase
    {
        private readonly EFDbContext _EFDbContext;
        private readonly DbSet<TEntity> _dbSet;
        
        public EFDataAccess()
        {
            _EFDbContext = new EFDbContext();
            _dbSet = _EFDbContext.Set<TEntity>();
        }
        public int Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return _EFDbContext.SaveChanges();
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            _EFDbContext.SaveChanges();
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            _EFDbContext.SaveChanges();
        }
        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            _EFDbContext.SaveChanges();
            Debug.WriteLine("删除成功");
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            _EFDbContext.SaveChanges();
        }
        public void Update(TEntity entities)
        {
            _dbSet.Update(entities);
            _EFDbContext.SaveChanges();
        }

        public IEnumerable<TEntity> Find()
        {
            var queryable = _dbSet.AsQueryable();
            foreach (var prop in typeof(TEntity).GetProperties().
                GetPropertysWhereAttr<ForeignKeyAttribute>())
            {
                queryable = queryable.Include($".{prop.Name}");
            }
            return queryable.AsEnumerable();
        }

        public IEnumerable<TEntity> FindAll()
        {
            var queryable = _dbSet.AsQueryable();
            foreach (var prop in typeof(TEntity).GetProperties().
                GetPropertysWhereAttr<ForeignKeyAttribute>())
            {
                queryable = queryable.Include($".{prop.Name}");
            }
            return queryable.ToList();
        }
    }
}
