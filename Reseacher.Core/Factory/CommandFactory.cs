using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Reseacher.Core
{
    internal sealed class CommandFactory
    {
        private readonly Engine _engine;

        internal CommandFactory(Engine engine) => _engine = engine;     

        public DbCommand CreateCommand(string command)
        {
            switch (_engine)
            {
                case Engine.SQLServer:
                    return new SqlCommand(command);

                case Engine.PostgreSQL:
                    return new NpgsqlCommand(command);

                case Engine.MySQL:
                    return new MySqlCommand(command);

                case Engine.MariaDB:
                    return new MySqlCommand(command);

                case Engine.SQLite:
                    return new SQLiteCommand(command);

                case Engine.OracleDatabase:
                    #pragma warning disable CS0618
                    return new OracleCommand(command);

                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
}
