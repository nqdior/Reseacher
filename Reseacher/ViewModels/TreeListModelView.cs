using MahApps.Metro.IconPacks;
using Reseacher.Core;
using System;
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

        /* 二回目以降はつりーびゅーるーとさわるほうがよきよきだよ */
        public void DrawTreeView(ServerRack serverRack)
        {
            var serverList = serverRack.ToList();
            foreach (var _server in serverList)
            {
                var serverChildren = new Category(_server.Name);
                serverChildren.Kind = "server";

                try
                {
                    // まずはサーバをツリーに追加する。 
                    serverChildren.Children = new List<Category>();
                    serverChildren.Column1 = _server.Engine.ToString();

                    // こっそり接続してみる。
                    _server.Open();
                    var service = new DatabaseService(_server);
                    var schemaList = service.GetSchemaList();

                    foreach (var _schema in schemaList)
                    {
                        var schemaChildren = new Category(_schema.Name)
                        {
                            Kind = "database",
                            Children = new List<Category>()
                        };
                        serverChildren.Children.Add(schemaChildren);
                    }

                }
                catch { /* ignore */ }
                finally
                {
                    TreeViewRoot.Add(serverChildren);
                    _server.Close();
                }
            }
        }

        public void Test(ServerRack serverRack)
        {
            serverRack["eve-dev-10"].Open();
            var service = new DatabaseService(serverRack["eve-dev-10"]);

            var tableList = service.GetTableList("Master18");
            foreach (var _table in tableList)
            {
                var tableChildren = new Category(_table.Name)
                {
                    Column1 = _table.RowCount.ToString(),
                    Column2 = _table.ColumnCount.ToString()
                };
                TreeViewRoot[0].Children[0].Children.Add(tableChildren);
            }
            serverRack["eve-dev-10"].Close();
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

        public string Kind { get; set; }

        public string Name { get; set; }

        public string Column1 { get; set; }

        public string Column2 { get; set; }

        public List<Category> Children { get; set; }
    }
}
