using Reseacher.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ServerRack serverRack = new ServerRack();

        public ManagePage()
        {
            InitializeComponent();
        }

        public void OSAndBrowser()
        {
            //Categories = new Category() {
            //new Category("OS") {
            //    Children = new Category {
            //        new Category("Windows") {
            //            Children = new Category {
            //                new Category("Windows 8"),
            //                new Category("Windows 7"),
            //                new Category("Windows Vista"),
            //                new Category("Windows XP"),
            //            }
            //        },
            //    }
            //},
            //new Category("ブラウザ") {
            //    Children = new Category {
            //        new Category("Internet Explorer") {
            //            Children = new Category {
            //                new Category("IE 11.0"),
            //                new Category("IE 10.0"),
            //                new Category("IE 9.0"),
            //                new Category("IE 8.0"),
            //                new Category("IE 7.0"),
            //            }
            //        },
            //        new Category("Firefox"),
            //        new Category("Chrome"),
            //        new Category("Opera"),
            //        new Category("Safari"),
            //    }
            //}
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = serverRack;
            /*
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
            serverRack.Add(server);
            server.Open();

            var tree = new TreeViewModel(serverRack);
            tree.GetSchemas();
            tree._servers["test"].GetTables("mysql");
            */
        }
    }

    public class ServerRack
    {
        public ServerList _serverList { get; set; }

        public ServerRack()
        {
            _serverList = new ServerList() {
                new SchemaList("server1")
                {
                    Children = new TableList("schema1")
                    {
                        new Table("1"),
                        new Table("2")
                    }
                },
                new SchemaList("server2")
                {
                    Children = new TableList("schema2")
                    {
                        new Table("1"),
                        new Table("2")
                    }
                },
                new SchemaList("server3")
                {
                    Children = new TableList("schema3")
                    {
                        new Table("1"),
                        new Table("2")
                    }
                }
            };
        }
    }

    public class OSAndBrowser
    {
        public Category Categories { get; set; }
        public OSAndBrowser()
        {
            Categories = new Category()
            {
                new Category("OS") {
                    Children = new Category {
                        new Category("Windows") {
                            Children = new Category {
                                new Category("Windows 8"),
                                new Category("Windows 7"),
                                new Category("Windows Vista"),
                                new Category("Windows XP"),
                            }
                        },
                        new Category("Mac OS X"),
                        new Category("Linux")
                    }
                },
                new Category("ブラウザ") {
                    Children = new Category {
                        new Category("Internet Explorer") {
                            Children = new Category {
                                new Category("IE 11.0"),
                                new Category("IE 10.0"),
                                new Category("IE 9.0"),
                                new Category("IE 8.0"),
                                new Category("IE 7.0"),
                            }
                        },
                        new Category("Firefox"),
                        new Category("Chrome"),
                        new Category("Opera"),
                        new Category("Safari"),
                    }
                }
            };
        }
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
