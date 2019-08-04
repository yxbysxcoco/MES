using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQ_DB_Framework.DataModel
{
    public class ParamDataTable
    {
        public  List<List<object>> ErrorDataList { get; set; }
        //合法行集合
        public List<List<object>> LegalDataList { get; set; }
        public List<List<object>> Rows { get; set; }
        public ParamDataTable()
        {
            ErrorDataList = new List<List<object>>();
            LegalDataList = new List<List<object>>();
            Rows= new List<List<object>>();
        }

        //将数据转换成对象集合
        public IEnumerable<TEntity> DecodeResult<TEntity>() where TEntity : EntityBase
        {
            var result = new List<TEntity>();
            foreach (var row in this.LegalDataList)
            {
                var entity = Activator.CreateInstance(typeof(TEntity));
                int i = 0;
                foreach (var prop in entity.GetType().GetProperties().GetPropertysWhereAttr<ColumnAttribute>())
                {
                    prop.SetValue(entity, prop.Convert(row[i].ToString()));
                    i++;
                }
                result.Add(entity as TEntity);
            }
            return result;
        }
    }
}
