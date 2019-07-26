using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;

namespace Reseacher
{
    /// <summary>
    /// ManagePage.xaml の相互作用ロジック
    /// </summary>
    public partial class ManageArea : UserControl
    {
        public ManageArea()
        {
            InitializeComponent();
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

        private void TreeView1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete)
            {
                return;
            }
            if (treeView1.SelectedValue is Server != true)
            {
                return;
            }
            var res = MessageBox.Show("このサーバを登録から削除してよろしいですか？", "確認", MessageBoxButton.OKCancel);
            if (res != MessageBoxResult.OK)
            {
                return;
            }

            var server = (Server)treeView1.SelectedValue;

            if (server.State == ConnectionState.Open)
            {
                server.Close();
            }
            Nucleus.ServerRack.Remove(server);
            Nucleus.WriteConfig();
        }
    }
}
