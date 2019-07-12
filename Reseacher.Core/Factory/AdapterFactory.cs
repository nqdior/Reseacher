using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Reseacher.Core
{
    internal sealed class AdapterFactory
    {
        private readonly Engine _engine;

        internal AdapterFactory(Engine engine) => _engine = engine;

        internal DbDataAdapter CreateAdapter()
        {
            switch (_engine)
            {
                case Engine.SQLServer:
                    return new SqlDataAdapter();

                case Engine.PostgreSQL:
                    return new NpgsqlDataAdapter();

                case Engine.MySQL:
                    return new MySqlDataAdapter();

                case Engine.MariaDB:
                    return new MySqlDataAdapter();

                case Engine.SQLite:
                    return new SQLiteDataAdapter();

                case Engine.Oracle:
                    #pragma warning disable CS0618
                    return new OracleDataAdapter();

                default:
                    throw new ArgumentOutOfRangeException("engine");
            }
        }
    }
}
