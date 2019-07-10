using MES.Const;
using MES.Models;
using MES.Table;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MES.Config
{
    public class MyDbConnection : SqlDbConnection
    {
        public  string ConnString = "Data Source=(DESCRIPTION=" +
                "(ADDRESS=" +
                "(PROTOCOL=TCP)(HOST=192.168.0.109)(PORT=1521))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));User Id=C##SXCQ_V1;Password=Welcome2414";

        public OracleConnection Con { get; }
        public MyDbConnection()
        {
            Con =(OracleConnection)GetDbConnection(DataBaseType.Oracle, ConnString);
        }
        public  List<object> SelectValueByField(string table, string primary, IEnumerable<object> column)
        {
            string sql = AppendSelectSql(table, primary, column).ToString();
            OracleCommand command = new OracleCommand(sql.ToString(), Con);
            Con.Open();
            OracleDataReader odr = command.ExecuteReader();
            //定义返回的数据字典
            var dataDictionary = new List<object>();
            while (odr.Read())
            {
                ReadValue(odr, dataDictionary,primary);
            }
            Con.Close();
            return dataDictionary;
        }
        //返回存储的数据对应的外键值
        public Dictionary<object, object> SelectFKValueByField(string table, string primary, IEnumerable<object> column)
        {
            string sql = AppendSelectSql(table, primary, column).ToString();
            OracleCommand command = new OracleCommand(sql.ToString(), Con);
            Con.Open();
            OracleDataReader odr = command.ExecuteReader();
            //定义返回的数据字典
            var dataDictionary = new Dictionary<object, object>();
            while (odr.Read())
            {
                ReadFkValue(odr, dataDictionary, column);
            }
            Con.Close();
            return dataDictionary;
        }

        internal MyDataTable SelectAll(Dictionary<string, string> poreignKeyAndFrimaryKey, Dictionary<string, List<string>> foreignTableNameAndfieldName, MyDataTable dataTable, Dictionary<string, string> filedAndType, string tableName)
        {
            string sql = AppendSelectSql(poreignKeyAndFrimaryKey, foreignTableNameAndfieldName, filedAndType, tableName);
            OracleCommand command = new OracleCommand(sql.ToString(), Con);
            Con.Open();
            OracleDataReader odr = command.ExecuteReader();
            while (odr.Read())
            {
                dataTable = ReadAllData(dataTable, odr);
            }
            Con.Close();
            return dataTable;
        }
        private static MyDataTable ReadAllData(MyDataTable dataTable, OracleDataReader odr)
        {
            DataTable DbDataTable = odr.GetSchemaTable();
            int i = 0;
            Row valueRow = new Row();
            foreach (DataRow dataRow in DbDataTable.Rows)
            {
                valueRow.Add(odr.GetValue(i));
                i++;
            }
            dataTable.Add(valueRow);
            return dataTable;
        }

        private string AppendSelectSql(Dictionary<string, string> poreignKeyAndFrimaryKey, Dictionary<string, List<string>> foreignTableNameAndfieldName, Dictionary<string, string> filedAndType, string tableName)
        {
            StringBuilder sql = new StringBuilder("SELECT");
            if (poreignKeyAndFrimaryKey.Keys.Count==0)
            {
                sql.Append(" * from \""+tableName+"\" ");
            }
            else
            {
                foreach (var field in filedAndType.Keys)
                {
                    sql.Append("\""+tableName + "\".\"" + field+"\",");
                }
                foreach (var field in foreignTableNameAndfieldName.Keys)
                {
                    sql.Append("\""+field+"\".\""+ foreignTableNameAndfieldName[field][1]+"\",");
                }
                sql.Remove(sql.Length - 1, 1);
                sql.Append(" from  ");
                int n = 0;
                for (int i = 1; i < foreignTableNameAndfieldName.Keys.Count; i++)
                {
                    sql.Append("(");
                    n++;
                }
                sql.Append("\"" + tableName + "\" ");
                foreach (var field in foreignTableNameAndfieldName.Keys)
                {
                    sql.Append("INNER JOIN \""+ field + "\" ON " +
                        "\"" + tableName + "\".\""+poreignKeyAndFrimaryKey[foreignTableNameAndfieldName[field][0]]+"\"" +
                        "=\""+ field+"\".\""+foreignTableNameAndfieldName[field][0]+"\"");
                    if (n!=0)
                    {
                        sql.Append(")");
                        n--;
                    }
                }
            }
            return sql.ToString();
        }

        //构建外键表对应的外键值集合
        private static Dictionary<object, object> ReadFkValue(OracleDataReader odr, Dictionary<object, object> dataDictionary, IEnumerable<object> column)
        {
            DataTable dataTable = odr.GetSchemaTable();
            int i = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var v in column)
                {
                    if (v.Equals(odr.GetValue(i)))
                    {
                        if (!dataDictionary.ContainsKey(v))
                        {
                            dataDictionary.Add(v, odr.GetValue(0));
                        }
                    }
                }
                i++;
            }
            return dataDictionary;
        }

        //读取查询的字段与值集合
        private static  List<object> ReadValue(OracleDataReader odr, List<object> valueList, string primary)
        {
            DataTable dataTable = odr.GetSchemaTable();
            int i = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                if (row[0].Equals(primary))
                {
                    valueList.Add(odr.GetValue(i));
                    return valueList;
                }
                i++;
            }
            return valueList;
        }
        //批量插入数据
        public bool InsertData(Type foreignTable, Dictionary<string, Column> fieldAndValue, Dictionary<string, string> fieldAndType)
        {
            string sql = AppendInsertSql(foreignTable, fieldAndValue.Keys).ToString();
            OracleCommand command = new OracleCommand(sql, Con);
            //这个参数需要指定每次批插入的记录数 
            command.ArrayBindCount = fieldAndValue.Values.ToList().ToArray()[0].Count;
            AddParam(fieldAndValue, fieldAndType, command);
            Con.Open();
            command.ExecuteNonQuery();
            Con.Close();
            return true;
        }

        //增加批量插入的参数
        private static void AddParam(Dictionary<string, Column> fieldAndValue, Dictionary<string, string> fieldAndType, OracleCommand command)
        {
            foreach (string fieldName in fieldAndValue.Keys)
            {
                object[] objectValue = fieldAndValue[fieldName].ToArray();
                OracleParameter nameParam = new OracleParameter()
                {
                    Direction = ParameterDirection.Input,
                };
                nameParam.ParameterName = fieldName;
                switch (fieldAndType[fieldName])
                {
                    case "Varchar2":
                        nameParam.OracleDbType = OracleDbType.Varchar2;
                        string[] ValueArray = new string[objectValue.Length];
                        objectValue.CopyTo(ValueArray, 0);
                        nameParam.Value = ValueArray;
                        break;
                    case "TIMESTAMP":
                        nameParam.OracleDbType = OracleDbType.TimeStamp;
                        DateTime[] ValueArrayDateTime = new DateTime[objectValue.Length];
                        for (int i = 0; i < objectValue.Length; i++)
                        {
                            ValueArrayDateTime[i] = DateTime.Parse(objectValue[i].ToString());
                        }
                        nameParam.Value = ValueArrayDateTime;
                        break;
                    case "Int":
                        nameParam.OracleDbType = OracleDbType.Int32;
                        int[] ValueArrayInt = new int[objectValue.Length];
                        for (int i = 0; i < objectValue.Length; i++)
                        {
                            ValueArrayInt[i] = int.Parse(objectValue[i].ToString());
                        }
                        nameParam.Value = ValueArrayInt;
                        break;
                    case "Decimal":
                        nameParam.OracleDbType = OracleDbType.Decimal;
                        break;
                    case "Double":
                        nameParam.OracleDbType = OracleDbType.BinaryDouble;
                        double[] ValueArrayDouble = new double[objectValue.Length];
                        for (int i = 0; i < objectValue.Length; i++)
                        {
                            ValueArrayDouble[i] = double.Parse(objectValue[i].ToString());
                        }
                        nameParam.Value = ValueArrayDouble;
                        break;
                    default:
                        break;
                }
                command.Parameters.Add(nameParam);
            }
        }

        public static StringBuilder AppendSelectSql(string tableName, string primary, IEnumerable<object> column)
        {
            object[] arrayColumn = column.ToArray();
            StringBuilder sql = new StringBuilder("SELECT  * FROM  \"C##SXCQ_V1\".\"" + tableName + "\"  WHERE  \"" + primary + "\" in ( ");
            for (int i = 0; i < arrayColumn.Length; i++)
            {
                if (i == arrayColumn.Length - 1)
                {
                    sql.Append(" \'" + arrayColumn[i] + "\' ) ");
                }
                else if (arrayColumn.Length % 1000 == 0 && i != arrayColumn.Length - 1)
                {
                    sql.Append(" \'" + arrayColumn[i] + "\' )  or \"" + primary + "\" in (");
                }
                else
                {
                    sql.Append(" \'" + arrayColumn[i] + "\', ");
                }
            }
            return sql;
        }
        //拼接批量插入的sql语句
        public static string AppendInsertSql(Type foreignTable, Dictionary<string, Column>.KeyCollection keys)
        {
            StringBuilder sql = new StringBuilder("INSERT  INTO  \"C##SXCQ_V1\".\"" + foreignTable.Name + "\" (");
            foreach (var field in keys)
            {
                sql.Append("\"" + field + "\",");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" ) values(");
            foreach (var field in keys)
            {
                sql.Append(":" + field + ", ");
            }
            sql.Remove(sql.Length - 2, 2);
            sql.Append(")");
            return sql.ToString();
        }

    }


    public class DateBaseContext : DbContext
    {
        public DbSet<Models.Storehouse> Storehouse { get; set; }
        public DbSet<Models.ToolEquipment> ToolEquipment { get; set; }
        public DbSet<Models.Material> Material { get; set; }
        public DbSet<Models.MeterageUnit> MeterageUnit { get; set; }
        public DbSet<Models.MoneyUnit> MoneyUnit { get; set; }
        public DbSet<Models.ToolEquipmentType> ToolEquipmentType { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //需下载Oracle.EntityFrameworkCore包
            optionsBuilder.UseOracle(@"User Id=C##SXCQ_V1;Password=Welcome2414;Data Source=192.168.0.109:1521/ORCL");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Storehouse>()
                .HasAlternateKey(c => c.Name);
            modelBuilder.Entity<Models.ToolEquipment>()
                .HasAlternateKey(c => c.Name);
            modelBuilder.Entity<Models.Material>()
                .HasAlternateKey(c => c.Name);
            modelBuilder.Entity<Models.MeterageUnit>()
                .HasAlternateKey(c => c.Name);
            modelBuilder.Entity<Models.ToolEquipmentType>()
                .HasAlternateKey(c => c.Name);
        }
    }
}
