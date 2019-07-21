using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace Reseacher
{
    public partial class DataViewPage : UserControl
    {
        public DataViewPage()
        {
            InitializeComponent();
            _initializeComponent();
        }

        private TabContent _parentTabContent;

        public TabContent ParentTabContent
        {
            get => _parentTabContent;
            set
            {
                tabNameText.Text = value.Header;
                _parentTabContent = value;
                tabNameText.TextChanged += TabNameText_TextChanged;
            }
        }

        public DataViewPage(string server, string schemaName, string tableName)
        {
            InitializeComponent();
            _initializeComponent();

            serverComboBox.Text = server;
            serverComboBox.SelectedValue = Nucleus.ServerRack[server];

            Editor.Text = $"SELECT * FROM {schemaName}.{tableName}";
            GetData();
        }

        private void _initializeComponent()
        {
            using (var reader = new XmlTextReader("./Resources/FbSql.xshd"))
            {
                Editor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            }

            var model = new ComboBoxViewModel(Nucleus.ServerRack);
            DataContext = model;
        }

        private DataTable result;

        private void Editor_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F5)
            {
                GetData();
            }
        }

        private void GetData()
        {
            result = null;
            var param = new object[2] { serverComboBox.SelectedValue, Editor.Text };

            var worker = new BackgroundWorker();
            worker.DoWork += GetDataStart;
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(GetDataComplate);
            worker.RunWorkerAsync(param);

            ToggleProgressRing();
        }

        private void GetDataStart(object sender, DoWorkEventArgs e)
        {
            var server = (e.Argument as object[])[0] as Server;
            var query = (e.Argument as object[])[1] as string;

            try
            {
                result = server.Fill(query, "xxx");         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetDataComplate(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGrid.DataContext = result?.DefaultView;
            ToggleProgressRing();
        }

        /* https://www.doraxdora.com/blog/2017/07/28/post-1873/ */
        private void ToggleProgressRing()
        {
            if (loading_image.IsActive)
            {
                loading_image.IsActive = false;
            }
            else
            {
                loading_image.IsActive = true;
            }
        }

        private void TabNameText_TextChanged(object sender, TextChangedEventArgs e) => ParentTabContent.Header = ((TextBox)sender).Text;
        
    }
}
