
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

namespace SQ_DB_Framework.SQDBContext
{
   public class SQDbSet<TEntity> where TEntity : EntityBase
    {
        private readonly EFDataAccess<TEntity> _EFDataAccess;


        public SQDbSet()
        {
            _EFDataAccess = new EFDataAccess<TEntity>();
        }

        public object FindByEntity()
        {
            return _EFDataAccess.FindByEntity();
        }

        public void AddRange(DataTable dataTable)
        {
            var entity = Activator.CreateInstance<TEntity>();
            dataTable = entity.CheckIllegalData(dataTable);
            IEnumerable<TEntity> entities = dataTable.DecodeResult<TEntity>();
            _EFDataAccess.AddRange(entities);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _EFDataAccess.AddRange(entities);
        }
        public int Add(TEntity entities)
        {
            return _EFDataAccess.Add(entities);
        }
        public void Update(TEntity entity)
        {
            _EFDataAccess.Update(entity);
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _EFDataAccess.UpdateRange(entities);
        }
        
        public IQueryable<TEntity> GetAllEntities()
        {
            var entities = _EFDataAccess.Find();
            return entities;
        }

        public List<TEntity> GetEntitiesByContion(Dictionary<string, string> entityInfoDic)
        {
            var entities = _EFDataAccess.FindByCondition(entityInfoDic);
            if (entities==null)
            {
                return new List<TEntity>();
            }
            return entities.ToList();
        }

        public void Remove(TEntity entity)
        {
            _EFDataAccess.Remove(entity);
        }
    
        public PageHelper<TEntity> GetEntities(int pageIndex, int pageSize, Dictionary<string, string> entityInfoDic,string prefix)
        {
            var entities =SelectByWhere(entityInfoDic,  prefix);
            //分页entities.ToList()
            var pageEntities = new PageHelper<TEntity>(entities.ToList(), pageIndex - 1, pageSize);
            return pageEntities;
        }
        public IEnumerable<TEntity> SelectByWhere(Dictionary<string, string> entityInfoDic, string prefix)
        {
            var entity = _EFDataAccess.Find();
            var type = typeof(TEntity);
            foreach (var searchCondition in entityInfoDic)
            {
                if (searchCondition.Value != null && !searchCondition.Value.Equals(""))
                {
                    foreach (var property in type.GetProperties().Where(prop => prop.IsDefined(typeof(IndexAttribute)) || prop.IsDefined(typeof(KeyAttribute))))
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
    }
}
