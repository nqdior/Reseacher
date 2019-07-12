using Reseacher.Properties;
using System.Windows;
using System.Windows.Controls;

namespace Reseacher
{
    /// <summary>
    /// DragablzControl.xaml の相互作用ロジック
    /// </summary>
    public partial class DragablzControl : UserControl
    {
        private static bool _hackyIsFirstWindow = true;

        public DragablzControl()
        {
            InitializeComponent();

            if (_hackyIsFirstWindow)
            {
                DataContext = MainWindowViewModel.CreateWithSamples();
            }
            _hackyIsFirstWindow = false;
        }
    }
}
