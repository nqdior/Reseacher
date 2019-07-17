using Reseacher.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Reseacher
{

    public class ServerRack : ObservableCollection<Server>
    {
        public ServerRack() { }

        public Server this[string key]
        {
            get => this.FirstOrDefault(s => s.Name == key);
            set
            {
                var server = this.FirstOrDefault(s => s.Name == key);
                server = value;
            }
        }
    }

    /* for reseacher customize class */
    public class Server : Core.Server, INotifyPropertyChanged
    {
        public Server(string name, Engine engine, string connectionString = "") : base (name, engine, connectionString) { }

        public string Kind => "server";

        public ObservableCollection<Child> Children { get; set; } = new ObservableCollection<Child>();

        public void FillSchemas()
        {
            try
            {
                Open();

                var service = new DatabaseService(this);
                var schemaList = service.GetSchemaList();

                foreach (var _schema in schemaList)
                {
                    var schemaChildren = new Child(_schema.Name, "database");
                    schemaChildren.Server = Name;
                    Children.Add(schemaChildren);
                }
            }
            finally
            {
                Close();
            }
        }

        public void FillTables(string schemaName)
        {
            try
            {
                Open();

                var service = new DatabaseService(this);
                var tableList = service.GetTableList(schemaName);

                foreach (var _table in tableList)
                {
                    var tableChildren = new Child(_table.Name, "table");
                    tableChildren.Server = Name;
                    Children.FirstOrDefault(c => c.Name == schemaName).Children.Add(tableChildren);
                }
            }
            finally
            {
                Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = null;

        protected void OnPropertyChanged(string info) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
    }

    public class Child
    {
        public Child(string name, string kind)
        {
            Name = name;
            Kind = kind;
        }

        public string Server { get; set; }

        public string Kind { get; set; }

        public string Name { get; set; }

        public ObservableCollection<Child> Children { get; set; } = new ObservableCollection<Child>();
    }
}
