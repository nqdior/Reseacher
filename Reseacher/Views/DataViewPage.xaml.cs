using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Windows.Controls;
using System.Xml;

namespace Reseacher
{
    /// <summary>
    /// DataViewPage.xaml の相互作用ロジック
    /// </summary>
    public partial class DataViewPage : UserControl
    {
        public DataViewPage()
        {
            InitializeComponent();
            using (var reader = new XmlTextReader("./Resources/FbSql.xshd"))
            {
                Editor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            }
        }
    }
}
