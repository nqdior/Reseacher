using Dapper;
using Microsoft.Win32.SafeHandles;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace Reseacher.Core
{
    public class Server : IDisposable
    {
        internal DbConnection _connection;

        private DbTransaction _transaction;

        private SshClient _client;

        private ForwardedPortLocal _forward;

        public string Name { get; }

        public Engine Engine { get; }

        public string ConnectionString
        {
            get => _connection.ConnectionString;
            set => _connection.ConnectionString = value;
        }

        private bool _useBridgeServer = false;

        public bool UseBridgeServer
        {
            get => _useBridgeServer;
            set
            {
                if (Engine == Engine.SQLServer || Engine == Engine.SQLite || Engine == Engine.OracleDatabase)
                {
                    _useBridgeServer = false;
                    return;
                }
                _useBridgeServer = value;
            }
        }

        private BridgeServer _bridgeServer = new BridgeServer();

        public BridgeServer BridgeServer
        {
            get => _bridgeServer;
            set
            {
                if (Engine == Engine.SQLServer || Engine == Engine.SQLite || Engine == Engine.OracleDatabase)
                {
                    _bridgeServer = null;
                    return;
                }
                _bridgeServer = value;
            }
        }

        public Server(string name, Engine engine, string connectionString = "")
        {
            Name = name;
            Engine = engine;
            _connection = new ConnectionFactory(engine).CreateConnection();
            _connection.ConnectionString = connectionString;
        }

        public void Open()
        {
            if (_useBridgeServer)
            {
                BridgeOpen();
            }
            _connection.Open();
        }

        public void Close()
        {
            if (_useBridgeServer)
            {
                BridgeClose();
            }
            _connection.Close();
        }

        public bool TestOpen()
        {
            try
            {
                Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Close();
            }
        }

        public bool TestOpenClient()
        {
            try
            {
                BridgeOpen();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                BridgeClose();
            }
        }

        public void BeginTransaction(IsolationLevel isolationLevel) => _transaction = _connection.BeginTransaction(isolationLevel);

        public void CommitTransaction() => _transaction.Commit();

        public void RollbackTransaction() => _transaction.Rollback();

        public ConnectionState State => _connection.State;

        public int Execute(string query, object param = null, IDbTransaction transaction = null) => _connection.Execute(query, param, transaction);

        public IEnumerable<dynamic> GetData(string query, object param = null, IDbTransaction transaction = null) => _connection.Query<dynamic>(query, param, transaction);

        public IEnumerable<T> GetData<T>(string query, object param = null, IDbTransaction transaction = null) => _connection.Query<T>(query, param, transaction);

        private void BridgeOpen()
        {
            if (_bridgeServer.HasKeyFile)
            {
                var pkfile = new PrivateKeyFile(_bridgeServer.KeyFilePath, _bridgeServer.Password);
                _client = new SshClient(_bridgeServer.Host, _bridgeServer.Port, _bridgeServer.UserName, pkfile);
            }
            else
            {
                _client = new SshClient(_bridgeServer.Host, _bridgeServer.Port, _bridgeServer.UserName, _bridgeServer.Password);
            }
            _client.Connect();

            _forward = new ForwardedPortLocal(_connection.DataSource, ConnectionStringsPort, "127.0.0.1", ConnectionStringsPort);
            _client.AddForwardedPort(_forward);
            _forward.Start();
        }

        private void BridgeClose()
        {
            _forward.Stop();
            _forward.Dispose();
            _client.Disconnect();
            _client.Dispose();
        }

        private uint ConnectionStringsPort
        {
            get 
            {
                switch (Engine)
                {
                    case Engine.SQLServer:
                        string source = new System.Data.SqlClient.SqlConnectionStringBuilder(ConnectionString).DataSource;
                        return source.Contains(",") ? Convert.ToUInt32(source.Split(',')[1]) : 1433;

                    case Engine.MySQL:
                    case Engine.MariaDB:
                        return new MySql.Data.MySqlClient.MySqlConnectionStringBuilder(ConnectionString).Port;

                    case Engine.PostgreSQL:
                        int port = new Npgsql.NpgsqlConnectionStringBuilder(ConnectionString).Port;
                        return Convert.ToUInt32(port);

                    default:
                        return 0;
                }
            }
        }

        public DataTable Fill(string query, string tableName)
        {
            using (var command = new CommandFactory(Engine).CreateCommand(query))
            {
                command.Connection = _connection;
                return Fill(command, tableName);
            }
        }

        private DataTable Fill(DbCommand query, string tableName)
        {
            var dataTable = new DataTable(tableName);
            using (var adapter = new AdapterFactory(Engine).CreateAdapter())
            {
                adapter.SelectCommand = query;
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public int Update(string selectQuery, DataTable table)
        {
            using (var command = new CommandFactory(Engine).CreateCommand(selectQuery))
            {
                command.Connection = _connection;
                return Update(command, table);
            }
        }

        private int Update(DbCommand selectQuery, DataTable table)
        {
            using (var builder = new CommandBuilderFactory(Engine).CreateCommandBuilder())
            using (var adapter = new AdapterFactory(Engine).CreateAdapter())
            {
                adapter.SelectCommand = selectQuery;
                builder.DataAdapter = adapter;
                builder.GetUpdateCommand();
                return adapter.Update(table);
            }
        }

        private bool disposed = false;

        private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            _connection.Close();
            _transaction.Dispose();
            _client.Dispose();
            _forward.Dispose();

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                handle.Dispose();
            }
            disposed = true;
        }

        public Server Clone()
        {
            var clone = new Server(Name, Engine);
            clone._connection = new ConnectionFactory(clone.Engine).CreateConnection();
            clone._connection.ConnectionString = _connection.ConnectionString;
            
            return clone;
        }
    }
}
