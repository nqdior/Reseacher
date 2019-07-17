using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System;
using System.Data;
using System.Windows;
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
            var model = new ComboBoxViewModel();
            model.DrawComboBox(Nucleus.ServerRack);
            DataContext = model;
        }

        DataTable result;

        private void Editor_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F5)
            {
                try
                {
                    Nucleus.ServerRack[serverComboBox.Text].Open();
                    result = Nucleus.ServerRack[serverComboBox.Text].Fill(Editor.Text, "xxx");
                    dataGrid.DataContext = result.DefaultView;           
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    try { Nucleus.ServerRack["test"].Close(); } catch { /* ignore */}
                }
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Nucleus.ServerRack[serverComboBox.Text].Update(Editor.Text, result);
        }
    }
}
