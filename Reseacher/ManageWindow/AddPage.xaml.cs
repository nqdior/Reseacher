using System;
using System.Collections.Generic;
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
    }
}
