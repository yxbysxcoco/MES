using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text;

namespace SQ_DB_Framework.DataModel
{
    public class DataTable : List<Row>
    {
        public List<Column> Columns { get; private set; }
        //不合法行集合
        public List<Row> ErrorDataList { get; set; }
        //合法行集合
        public List<Row> LegalDataList { get; set; }

        public DataTable()
        {
            ErrorDataList = new List<Row>();
            LegalDataList = new List<Row>();
        }
        public List<Column> AddColumn(Column column)
        {
            if(Columns == null)
            {
                Columns = new List<Column>();
            }
            Columns.Add(column);
            return Columns;
        }
        public List<Column> AddColumns(IEnumerable<Column> columns)
        {
            foreach(var column in columns)
            {
                AddColumn(column);
            }
            return Columns;
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
