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

        TreeViewModelView modelView = new TreeViewModelView();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = modelView;
            modelView.Modeltest();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }

    public class TreeViewModelView : INotifyPropertyChanged
    {
        public ObservableCollection<Category> TreeViewRoot { get; set; }

        public Server server;

        public TreeViewModelView()
        {
            TreeViewRoot = new ObservableCollection<Category>();
        }

        public void Modeltest()
        {
            server = new Server("test", Engine.MySQL);

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
            server.Open();
            
            var schemaList = test.GetSchemaList();
            foreach (var _server in serverRack)
            {
                var serverChildren = new Category(_server.Name)
                {
                    Children = new List<Category>()
                };
                serverChildren.Column1 = _server.Engine.ToString();

                TreeViewRoot.Add(serverChildren);

                foreach (var _schema in schemaList)
                {
                    var schemaChildren = new Category(_schema.Name)
                    {
                        Children = new List<Category>()
                    };
                    var tableList = test.GetTableList(_schema.Name);
                    foreach (var _table in tableList)
                    {
                        var tableChildren = new Category(_table.Name)
                        {
                            Column1 = _table.RowCount.ToString(),
                            Column2 = _table.ColumnCount.ToString()
                        };
                        schemaChildren.Children.Add(tableChildren);
                    }
                    schemaChildren.Column1 = _schema.TableCount.ToString();
                    serverChildren.Children.Add(schemaChildren);
                }
            }
            server.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged = null;
        protected void OnPropertyChanged(string info) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
    }

    public class Category
    {
        public Category(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

        public string Column1 { get; set; }

        public string Column2 { get; set; }

        public List<Category> Children { get; set; }
    }
}
