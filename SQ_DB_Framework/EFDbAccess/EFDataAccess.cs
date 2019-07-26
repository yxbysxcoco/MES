using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using SQ_DB_Framework.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Reflection;

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

        public object FindByEntity()
        {
            return _EFDbContext.Find<TEntity>(1);
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

        public IQueryable<TEntity> Find()
        {
            var queryable = _dbSet.AsQueryable();

            foreach (var prop in typeof(TEntity).GetProperties().
                GetPropertysWhereAttr<ForeignKeyAttribute>())
            {
                queryable = queryable.Include($".{prop.Name}");
            }
            return queryable;
        }

        public IQueryable<TEntity> FindByCondition(Dictionary<string, string> entityInfoDic)
        {
            
            var queryable = Find();

            var type = typeof(TEntity);

            foreach (var searchCondition in entityInfoDic)
            {
                if (searchCondition.Value != null && !searchCondition.Value.Equals(""))
                {
                    foreach (var property in type.GetProperties().Where(prop => prop.IsDefined(typeof(ColumnAttribute))))
                    {
                        if (searchCondition.Key.Equals(property.Name))
                        {
                            queryable = queryable.Where(en => property.GetValue(en).ToString() == searchCondition.Value).AsQueryable();

                            continue;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }

            return queryable;
        }

    }
}
