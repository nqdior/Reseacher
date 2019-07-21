using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Dragablz;
using System.Collections.Specialized;

namespace Reseacher
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            InterTabClient = new DefaultInterTabClient();
        }

        public static MainWindowViewModel result;

        public static MainWindowViewModel CreateWithIntroduction()
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

        public static MainWindowViewModel CreateWithDataView(string server, string schemaName, string tableName)
        {
            var page = new DataViewPage(server, schemaName, tableName);
            var tabContent = new TabContent($"{server}.{schemaName}.{tableName}", page);
            result.TabContents.Add(tabContent);
            page.ParentTabContent = tabContent;

            return result;
        }

        public ObservableCollection<TabContent> TabContents { get; } = new ObservableCollection<TabContent>();

        public static Func<object> NewItemFactory => () =>
        {
            var page = new DataViewPage();
            var tabContent = new TabContent("新規ビュー", page);
            page.ParentTabContent = tabContent;

            return tabContent;
        };

        public IInterTabClient InterTabClient { get; }
    }
}
