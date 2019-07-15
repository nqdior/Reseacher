using MySql.Data.MySqlClient;
using Reseacher.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Reseacher
{
    /// <summary>
    /// AddPage.xaml の相互作用ロジック
    /// </summary>
    public partial class AddPage : UserControl
    {
        public AddPage()
        {
            InitializeComponent();
        }

        private void InputIntegerOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            var text = ((TextBox)sender).Text + e.Text;
            e.Handled = regex.IsMatch(text);
        }

        private void InputIntegerOrPeriodOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            var text = ((TextBox)sender).Text + e.Text;
            e.Handled = regex.IsMatch(text);
        }

        private void InputIntegerOrAlphaOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9a-zA-Z]+");
            var text = ((TextBox)sender).Text + e.Text;
            e.Handled = regex.IsMatch(text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var server = CreateServer();
            Nucleus.Servers.Add(server);
        }

        private Server CreateServer()
        {
            var server = new Server(connectionName.Text, engine.Text.ToEngine())
            {
                ConnectionString = CreateConnectionString()
            };
            if (visibleSwitch.IsChecked == true)
            {
                server = CreateSshConnectionString(server);
            }
            return server;
        }

        private string CreateConnectionString()
        {
            
            switch (engine.Text.ToEngine())
            {
                case Engine.MySQL:
                    var constr = ConnectionStringBuilderProvider.MySqlConnectionStringBuilder;
                    constr.Server = address.Text;
                    constr.Port = string.IsNullOrEmpty(port.Text) ? 3306 : Convert.ToUInt32(port.Text);
                    constr.UserID = userID.Text;
                    constr.Password = password.Password;
                    return constr.ToString();
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        private Server CreateSshConnectionString(Server server)
        {
            server.BridgeServer = new BridgeServer()
            {
                Host = sshAddress.Text,
                Port = string.IsNullOrEmpty(sshPort.Text) ? 22 : Convert.ToInt32(sshPort.Text),
                UserName = sshUserID.Text,
                Password = sshPassword.Password
            };
            server.UseBridgeServer = true;
            return server;
        }

        private Type aaa()
        {
            return typeof(MySqlConnectionStringBuilder);
        }
    }
}
