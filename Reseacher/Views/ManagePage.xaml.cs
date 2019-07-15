using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
