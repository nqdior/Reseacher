using System.Collections.Generic;

namespace Reseacher.Core
{
    public class DatabaseService
    {
        private readonly Server _server;

        public DatabaseService(Server server) => _server = server;

        public IEnumerable<Schema> GetSchemaList()
        {
            var query = new QueryFactory(_server.Engine).SchemaListCommand();
            return _server.GetData<Schema>(query);
        }

        public IEnumerable<Table> GetTableList(string schemaName)
        {
            var query = new QueryFactory(_server.Engine).TableListCommand(schemaName);
            return _server.GetData<Table>(query);
        }
    }
}
