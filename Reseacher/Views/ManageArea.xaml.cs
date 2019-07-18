using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            if (treeView1.SelectedValue is Server == true)
            {
                var server = (Server)treeView1.SelectedValue;
                server.FillSchemas();
            }
            else
            {
                var child = (Child)treeView1.SelectedValue;
                Nucleus.ServerRack[child.Server].FillTables(child.Name);
            }
        }
    }
}
