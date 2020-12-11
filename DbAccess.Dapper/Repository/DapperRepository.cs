using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess.Dapper.Repository
{
    public class DapperRepository<T>
    {
        public System.Data.IDbConnection _connection;

        public DapperRepository()
        {
			//_connection = new System.Data.SqlClient.SqlConnection(@"Server=77.245.159.18;Database=IHK;User Id=tdihk;Password=*F56dwa7;");
			_connection = new System.Data.SqlClient.SqlConnection(@"Server=DESKTOP-IUFI4CJ\SQLEXPRESS;Database=IHK;Trusted_Connection=True;");

		}

        public System.Data.IDbConnection Connection
        {
            get { return _connection; }
        }


        public List<T> GetList(string query, object param)
        {
            return Connection.Query<T>(query, param).ToList();
        }

        public T Get(string query, object param)
        {
            return Connection.Query<T>(query, param).FirstOrDefault();
        }

        public int Execute(string query, object param)
        {
            return Connection.Execute(query, param);
        }

        public void Dispose()
        {
            if (_connection != null)
                _connection.Dispose();
        }

    }
}
