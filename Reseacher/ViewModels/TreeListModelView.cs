using Reseacher.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Reseacher
{

    public class TreeListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Category> TreeViewRoot { get; set; }

        public TreeListViewModel()
        {
            TreeViewRoot = new ObservableCollection<Category>();
        }

        public void DrawTreeView(ServerRack serverRack)
        {
            var serverList = serverRack.ToList();
            foreach (var _server in serverList)
            {
                try
                {
                    // まずはサーバをツリーに追加する。
                    var serverChildren = new Category(_server.Name);
                    serverChildren.Children = new List<Category>();
                    serverChildren.Column1 = _server.Engine.ToString();
                    TreeViewRoot.Add(serverChildren);

                    // こっそり接続してみる。
                    _server.Open();
                    var service = new DatabaseService(_server);
                    var schemaList = service.GetSchemaList();

                    foreach (var _schema in schemaList)
                    {
                        var schemaChildren = new Category(_schema.Name)
                        {
                            Children = new List<Category>()
                        };
                        var tableList = service.GetTableList(_schema.Name);
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
                catch { /* ignore */ }
                finally
                {
                    _server.Close();
                }
            }
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
