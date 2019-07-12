using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Reseacher.Core;

namespace Reseacher.Core.Demo
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var server = new Server("test", Engine.MySQL);

            var conStr = ConnectionStringBuilderProvider.MySqlConnectionStringBuilder;
            conStr.Server = "127.0.0.1";
            conStr.Port = 3306;
            conStr.UserID = "root";
            conStr.Password = "";
            conStr.Database = "mysql";
            server.ConnectionString = conStr.ToString();

            server.BridgeServer = new BridgeServer()
            {
                Host = "172.16.1.110",
                UserName = "root",
                Password = "",
                Port = 22,
                
            };
            server.UseBridgeServer = true;

            server.Open();

            var selectTest = server.GetData("select * from db;");
            var fillTest = server.Fill("select * from db;", "db");

            server.Close();
        }
    }
}
