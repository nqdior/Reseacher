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

        public new void Open()
        {
            base.Open();

            Kind = State == System.Data.ConnectionState.Open ? "serverNetwork" : "serverNetworkOff";
            OnPropertyChanged("Kind");
        }

        public new void Close()
        {
            base.Close();

            Kind = State == System.Data.ConnectionState.Open ? "serverNetwork" : "serverNetworkOff";
            OnPropertyChanged("Kind");
        }

        public string Kind { get; set; } = "serverNetworkOff";

        public ObservableCollection<Child> Children { get; set; } = new ObservableCollection<Child>();

        public void RemoveAllChildren()
        {
            foreach (var index in Enumerable.Range(0, Children.Count))
            {
                Children.RemoveAt(0);
            }
        }

        public void FillSchemas()
        {
            var service = new DatabaseService(this);
            var schemaList = service.GetSchemaList();
            RemoveAllChildren();

            foreach (var _schema in schemaList)
            {
                var schemaChildren = new Child(_schema.Name, "database");
                schemaChildren.Server = Name;
                schemaChildren.Parent = Name;
                Children.Add(schemaChildren);
            }
        }

        public void FillTables(string schemaName)
        {
            var service = new DatabaseService(this);
            var tableList = service.GetTableList(schemaName);
            var schemaChildren = Children.FirstOrDefault(c => c.Name == schemaName);
            schemaChildren.RemoveAllChildren();

            foreach (var _table in tableList)
            {
                var tableChildren = new Child(_table.Name, "table");
                tableChildren.Server = Name;
                tableChildren.Parent = schemaName;
                schemaChildren.Children.Add(tableChildren);
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

        public string Parent { get; set; }

        public string Kind { get; set; }

        public string Name { get; set; }

        public ObservableCollection<Child> Children { get; set; } = new ObservableCollection<Child>();

        public void RemoveAllChildren()
        {
            foreach (var index in Enumerable.Range(0, Children.Count))
            {
                Children.RemoveAt(0);
            }
        }
    }
}
