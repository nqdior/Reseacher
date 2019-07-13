using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Reseacher.Core
{
    internal sealed class CommandBuilderFactory
    {
        private readonly Engine _engine;

        internal CommandBuilderFactory(Engine engine) => _engine = engine;

        public DbCommandBuilder CreateCommandBuilder()
        {
            switch (_engine)
            {
                case Engine.SQLServer:
                    return new SqlCommandBuilder();

                case Engine.PostgreSQL:
                    return new NpgsqlCommandBuilder();

                case Engine.MySQL:
                    return new MySqlCommandBuilder();

                case Engine.MariaDB:
                    return new MySqlCommandBuilder();

                case Engine.SQLite:
                    return new SQLiteCommandBuilder();

                case Engine.OracleDatabase:
                    #pragma warning disable CS0618
                    return new OracleCommandBuilder();

                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
}
