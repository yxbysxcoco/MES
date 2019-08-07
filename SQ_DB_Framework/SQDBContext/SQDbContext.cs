
using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.EntityConfigures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.EFDbAccess;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace SQ_DB_Framework.SQDBContext
{
   public class SQDbSet<TEntity> where TEntity : EntityBase
    {
        
        private readonly EFDbContext _EFDbContext ;
        private readonly DbSet<TEntity> _dbSet;

        public SQDbSet()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            MyConnection.Initialize(new ServiceCollection());
            _EFDbContext = MyConnection.Context;

            Debug.WriteLine("EFDbContext()执行时间：" + sw.Elapsed.TotalMilliseconds + " 毫秒");

            _dbSet = _EFDbContext.Set<TEntity>();

            Debug.WriteLine("EFDbContext()执行时间：" + sw.Elapsed.TotalMilliseconds + " 毫秒");

        }

        public TEntity FindByEntity(object id)
        {
            return _EFDbContext.Find<TEntity>(id);
        }

        public void AddRange(ParamDataTable paramdataTable)
        {
            var transaction = _EFDbContext.Database.BeginTransaction();

            var entity = Activator.CreateInstance<TEntity>();
            paramdataTable = entity.CheckIllegalData(paramdataTable);
            IEnumerable<TEntity> entities = paramdataTable.DecodeResult<TEntity>();

            _dbSet.AddRange(entities);
            _EFDbContext.SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            _EFDbContext.SaveChanges();
        }

        public int Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return _EFDbContext.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            _dbSet.Update(entity);
            if (SaveChange())
            {
                return 1;
            }
            return 0;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            _EFDbContext.SaveChanges();
        }
        
        public IQueryable<TEntity> GetAllEntities()
        {
            var queryable = _dbSet.AsNoTracking().AsQueryable();

            foreach (var prop in typeof(TEntity).GetProperties().
                GetPropertysWhereAttr<ForeignKeyAttribute>())
            {
                queryable = queryable.Include($".{prop.Name}");
            }
            return queryable;
        }

        public List<TEntity> GetEntitiesByContion(Dictionary<string, string> entityInfoDic)
        {
            var entities =FindByCondition(entityInfoDic);
            if (entities==null)
            {
                return new List<TEntity>();
            }
            return entities.ToList();
        }

        public int Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            return _EFDbContext.SaveChanges();
        }
    
        public PageHelper<TEntity> GetEntitiesByCondition(int pageIndex, int pageSize, Dictionary<string, string> entityInfoDic,string prefix)
        {
            var entities =SelectByWhere(entityInfoDic,  prefix);

            //分页entities.ToList()
            var pageEntities = new PageHelper<TEntity>(entities.ToList(), pageIndex - 1, pageSize);
            
            return pageEntities;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            _EFDbContext.SaveChanges();
        }

        private IQueryable<TEntity> FindByCondition(Dictionary<string, string> entityInfoDic)
        {

            var queryable = GetAllEntities();

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

        private IEnumerable<TEntity> SelectByWhere(Dictionary<string, string> entityInfoDic, string prefix)
        {
            var entity = GetAllEntities();
            var type = typeof(TEntity);

            foreach (var searchCondition in entityInfoDic)
            {
                if (searchCondition.Value != null && !searchCondition.Value.Equals(""))
                {
                    foreach (var property in type.GetProperties().Where(prop => prop.IsDefined(typeof(Attributes.IndexAttribute)) || prop.IsDefined(typeof(KeyAttribute))))
                    {

                        if (searchCondition.Key.Equals(prefix + property.Name))
                        {
                            entity = entity.Where(en => property.GetValue(en).ToString() == searchCondition.Value);
                            continue;
                        }
                        if (searchCondition.Key.Equals(prefix + "Start" + property.Name))
                        {
                            entity = entity.Where(en => ((DateTime)property.GetValue(en)) > DateTime.Parse(searchCondition.Value));
                            continue;
                        }
                        if (searchCondition.Key.Equals(prefix + "End" + property.Name))
                        {
                            entity = entity.Where(en => ((DateTime)property.GetValue(en)) <= DateTime.Parse(searchCondition.Value));
                            continue;
                        }
                    }
                }
            }
            return entity;
        }

        private bool SaveChange()
        {
            var saved = false;
            while (!saved)
            {
                try
                {
                    _EFDbContext.SaveChanges();
                    saved = true;
                    return saved;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is TEntity)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues[property];

                                // TODO: decide which value should be written to database
                                proposedValues[property] = databaseValues[property];

                            }
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                    }
                }
            }
            return saved;
        }

    }
}
