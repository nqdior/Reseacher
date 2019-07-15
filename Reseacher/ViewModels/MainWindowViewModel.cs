using System;
using System.Collections.ObjectModel;
using Dragablz;

namespace Reseacher
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            InterTabClient1 = new DefaultInterTabClient();
        }

        static MainWindowViewModel result;

        public static MainWindowViewModel CreateWithSamples()
        {
            result = new MainWindowViewModel();

            result.TabContents.Add(new TabContent("ようこそ", new IntroductionPage()));

            return result;
        }

        public static MainWindowViewModel CreateWithAdds()
        {
            result.TabContents.Add(new TabContent("サーバ追加ビュー", new AddPage()));

            return result;
        }

        public ObservableCollection<TabContent> TabContents { get; } = new ObservableCollection<TabContent>();

        public IInterTabClient InterTabClient => InterTabClient1;

        public static Func<object> NewItemFactory => () => new TabContent("新規ビュー", new DataViewPage());

        public IInterTabClient InterTabClient1 { get; }
    }
}
