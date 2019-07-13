using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reseacher.Core;

namespace Reseacher
{
    public class TreeViewModel
    {
        public List<ServerModel> Servers => _servers.Select(r => r.Value).ToList();

        public Dictionary<string, ServerModel> _servers { get; private set; } = new Dictionary<string, ServerModel>();

        public TreeViewModel(List<Server> serverRack)
        {
            foreach (var server in serverRack)
            {
                _servers.Add(server.Name, new ServerModel(server));
            }
        }

        public void GetSchemas()
        {
            foreach (var server in _servers)
            {
                var schemas = server.Value.service.GetSchemaList();
                foreach (var schema in schemas)
                {
                    server.Value._schemas.Add(schema.Name, new SchemaModels(schema.Name));
                }
            }
        }
    }

    public class ServerModel
    {
        public string Name;

        public DatabaseService service;

        public ServerModel(Server server)
        {
            Name = server.Name;
            service = new DatabaseService(server);
        }

        public List<SchemaModels> Schemas => _schemas.Select(r => r.Value).ToList();

        public Dictionary<string, SchemaModels> _schemas { get; set; } = new Dictionary<string, SchemaModels>();

        public void GetTables(string schemaName)
        {
            var tables = service.GetTableList(schemaName);
            _schemas[schemaName].Tables = tables.Select(v => v.Name).ToList();
        }
    }

    public class SchemaModels
    {
        public SchemaModels(string name) => Name = name;

        public string Name;

        public List<string> Tables { get; set; } = new List<string>();
    }
}
