using MySql.Data.MySqlClient;
using Npgsql;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Reseacher.Core
{
    public static class ConnectionStringBuilderProvider
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder => new SqlConnectionStringBuilder();

        #pragma warning disable CS3003
        public static MySqlConnectionStringBuilder MySqlConnectionStringBuilder => new MySqlConnectionStringBuilder();
        #pragma warning restore CS3003

        #pragma warning disable CS3003
        public static NpgsqlConnectionStringBuilder PGSqlConnectionStringBuilder => new NpgsqlConnectionStringBuilder();
        #pragma warning restore CS3003

        #pragma warning disable CS3003
        public static MySqlConnectionStringBuilder MariaDBConnectionStringBuilder => new MySqlConnectionStringBuilder();
        #pragma warning restore CS3003

        public static SQLiteConnectionStringBuilder SqliteConnectionStringBuilder => new SQLiteConnectionStringBuilder();

        #pragma warning disable CS0618
        public static OracleConnectionStringBuilder OracleConnectionStringBuilder => new OracleConnectionStringBuilder();
    }
}
