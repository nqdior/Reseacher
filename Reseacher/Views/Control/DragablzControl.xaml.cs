using Dragablz;
using Dragablz.Dockablz;
using System.Collections.Specialized;

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
            MainWindowViewModel.result.TabContents.CollectionChanged += TabContents_CollectionChanged;
        }

        private void TabContents_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (dragblzControl.Items.Count == 1) return;
            dragblzControl.SelectedIndex = e.NewStartingIndex;
        }

        public DragablzControl()
        {
            InitializeComponent();

            if (_hackyIsFirstWindow)
            {
                DataContext = MainWindowViewModel.CreateWithIntroduction();
            }
        }

        public void AddServerAddPage()
        {
            DataContext = MainWindowViewModel.CreateWithAdds();
            
        }
    }
}
