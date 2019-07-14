using Reseacher.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reseacher
{
    /// <summary>
    /// ManagePage.xaml の相互作用ロジック
    /// </summary>
    public partial class ManagePage : UserControl
    {
        public ManagePage()
        {
            InitializeComponent();
        }

        TreeViewModelView model = new TreeViewModelView();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            model.Modeltest();
        }
    }

    public class TreeViewModelView : INotifyPropertyChanged
    {
        public Category TreeViewRoot { get; set; }
        public TreeViewModelView()
        {
            var server = new Server("test", Engine.MySQL);

            var constr = ConnectionStringBuilderProvider.MySqlConnectionStringBuilder;
            constr.Server = "127.0.0.1";
            constr.Port = 3306;
            constr.UserID = "root";
            constr.Password = "password";
            server.ConnectionString = constr.ToString();

            server.UseBridgeServer = true;
            server.BridgeServer = new BridgeServer
            {
                Host = "192.168.1.3",
                Port = 22,
                UserName = "root",
                Password = "2006079aA"
            };
            var serverRack = new List<Server>
            {
                server
            };
            var test = new DatabaseService(server);

            TreeViewRoot = new Category();
            var schemaList = test.GetSchemaList();
            foreach (var _server in serverRack)
            {
                var serverChildren = new Category(_server.Name)
                {
                    Children = new Category()
                };
                TreeViewRoot.Add(serverChildren);

                foreach (var _schema in schemaList)
                {
                    var schemaChildren = new Category(_schema.Name)
                    {
                        Children = new Category()
                    };
                    var tableList = test.GetTableList(_schema.Name);
                    foreach (var _table in tableList)
                    {
                        var tableChildren = new Category(_table.Name);
                        schemaChildren.Children.Add(tableChildren);
                    }
                    serverChildren.Children.Add(schemaChildren);
                }
            }
        }

        public void Modeltest()
        {
            var serverChildren = new Category("ばばば")
            {
                Children = new Category()
            };
            TreeViewRoot.Add(serverChildren);
        }

        public event PropertyChangedEventHandler PropertyChanged = null;
        protected void OnPropertyChanged(string info) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
    }

    public class Category : ObservableCollection<Category>
    {
        public Category()
        {
        }
        public Category(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

        public Category Children { get; set; }
    }

    public class ServerList : ObservableCollection<SchemaList>
    {
        public ServerList()
        {
        }
        public ServerList(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

        public SchemaList Children { get; set; }
    }

    public class SchemaList : ObservableCollection<TableList>
    {
        public SchemaList()
        {
        }
        public SchemaList(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

        public TableList Children { get; set; }
    }

    public class TableList : ObservableCollection<Table>
    {
        public TableList()
        {
        }
        public TableList(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }

    public class Table
    {
        public string Name;

        public Table(string name)
        {
            Name = name;
        }
    }
}
