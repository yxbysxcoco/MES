using MES.Const;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Config
{
    public abstract class SqlDbConnection
    {
        public DbConnection GetDbConnection(DataBaseType dataBaseType, string ConnString)
        {
            switch (dataBaseType)
            {
                case DataBaseType.SqlServer:
                    return new SqlConnection(ConnString);
                case DataBaseType.MySql:
                    return new MySqlConnection(ConnString);
                case DataBaseType.Oracle:
                    return new OracleConnection(ConnString);
                default:
                    return new SqlConnection(ConnString);
            }
        }
    }
}
