using Dragablz;
using Dragablz.Dockablz;

namespace Reseacher
{
    /// <summary>
    /// DragablzControl.xaml の相互作用ロジック
    /// </summary>
    public partial class DragablzControl : Layout
    {
        private static bool _hackyIsFirstWindow = true;

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
            dragblzControl.SelectedIndex = dragblzControl.Items.Count - 1;
        }
    }
}
