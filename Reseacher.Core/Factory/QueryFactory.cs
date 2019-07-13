using Reseacher.Core.Properties;
using System;

namespace Reseacher.Core
{
    internal sealed class QueryFactory
    {
        private readonly Engine _engine;

        internal QueryFactory(Engine engine) => _engine = engine;       

        internal string SchemaListCommand()
        {
            switch (_engine)
            {
                case Engine.SQLServer:
                    return ""; //Resources.MSSQL_DATABASES;

                case Engine.PostgreSQL:
                    return ""; // Resources.PGSQL_DATABASES;

                case Engine.MySQL:
                    return MyResource.SCHEMAS;

                case Engine.MariaDB:
                    return ""; //  Resources.MYSQL_DATABASES;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal string TableListCommand(string schemaName)
        {
            switch (_engine)
            {
                case Engine.SQLServer:
                    return ""; //  Resources.MSSQL_TABLES;

                case Engine.PostgreSQL:
                    return ""; //  Resources.PGSQL_TABLES;

                case Engine.MySQL:
                    return string.Format(MyResource.TABLES, schemaName);

                case Engine.MariaDB:
                    return ""; //  Resources.MYSQL_TABLES;

                case Engine.SQLite:
                    return ""; //  Resources.SQLITE_TABLES;

                case Engine.OracleDatabase:
#pragma warning disable CS0618
                    return ""; //  Resources.ORACLE_TABLES;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}