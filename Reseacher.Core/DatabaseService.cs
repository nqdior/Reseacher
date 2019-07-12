using System.Collections.Generic;

namespace Reseacher.Core
{
    public class DatabaseService
    {
        private readonly Server _server;

        public DatabaseService(Server server) => _server = server;

        public IEnumerable<SchemaModel> GetSchemaList()
        {
            var query = new QueryFactory(_server.Engine).SchemaListCommand();
            return _server.GetData<SchemaModel>(query);
        }

        public IEnumerable<TableModel> GetTableList(string schemaName)
        {
            var query = new QueryFactory(_server.Engine).TableListCommand(schemaName);
            return _server.GetData<TableModel>(query);
        }
    }
}
