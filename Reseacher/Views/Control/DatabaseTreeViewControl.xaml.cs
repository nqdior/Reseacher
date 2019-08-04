using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;

namespace Reseacher
{

    public partial class DatabaseTreeViewControl : ScrollViewer
    {
        public DatabaseTreeViewControl()
        {
            InitializeComponent();
            DataContext = new TreeModelView(Nucleus.ServerRack);
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void TreeView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (treeView1.SelectedValue == null) return;
            if (treeView1.SelectedValue is Server == true)
            {
                ServerClickAction();
                return;
            }
            var kind = ((Child)treeView1.SelectedValue).Kind;
            if (kind == "database")
            {
                SchemaClickAction();
                return;
            }
            TableClickAction();
        }

        private void ServerClickAction()
        {
            var server = (Server)treeView1.SelectedValue;
            if (server.State == ConnectionState.Open)
            {
                server.Close();
                server.RemoveAllChildren();

                return;
            }

            try
            {
                server.Open();
                server.FillSchemas();
            }
            catch (Exception ex)
            {
                server.Close();
                MessageBox.Show(ex.Message);
            }  
        }

        private void SchemaClickAction()
        {
            var child = (Child)treeView1.SelectedValue;
            var server = Nucleus.ServerRack[child.Parent];
            server.FillTables(child.Name);
        }

        private void TableClickAction()
        {
            var child = (Child)treeView1.SelectedValue;
            MainWindowViewModel.CreateWithDataView(child.Server, child.Parent, child.Name);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (treeView1.SelectedValue is Server == true)
            {
                var server = treeView1.SelectedValue as Server;
                MainWindowViewModel.CreateWithEdits(server);
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (treeView1.SelectedValue is Server == true)
            {
                var server = treeView1.SelectedValue as Server;
                Nucleus.ServerRack.Remove(server);
                Nucleus.WriteConfig();
            }
        }
    }
}
