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

        public bool Hacky => _hackyIsFirstWindow;

        public void FormLoadEnded()
        {
            _hackyIsFirstWindow = false;
        }

        public DragablzControl()
        {
            InitializeComponent();

            if (_hackyIsFirstWindow)
            {
                DataContext = MainWindowViewModel.CreateWithSamples();
            }
        }

        public void AddServerAddPage()
        {
            DataContext = MainWindowViewModel.CreateWithAdds();
            InitialTabablzControl.SelectedIndex = InitialTabablzControl.Items.Count - 1;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
