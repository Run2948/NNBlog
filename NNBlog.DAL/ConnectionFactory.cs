
using System.Data.Common;
using System.Data.SqlClient;

namespace NNBlog.DAL
{
    /// <summary>
    /// 数据库连接工厂
    /// </summary>
    public class ConnectionFactory
    {
        public static DbConnection GetOpenConnection(string connStr)
        {
            var connection = new SqlConnection(connStr); // @"Server=.;uid=sa;pwd=sa1994sa;database=NNBlog;"

            connection.Open();

            return connection;
        }



    }
}