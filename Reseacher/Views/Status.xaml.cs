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

namespace Reseacher
{
    /// <summary>
    /// DatabaseState.xaml の相互作用ロジック
    /// </summary>
    public partial class DatabaseState : UserControl
    {
        public DatabaseState()
        {
            InitializeComponent();
        }

        /*
        async void Main()
        {
            foreach (var index in Enumerable.Range(1, 2))
            {
                if (index == 1)
                {
                    var heavyResult = await RunTask(index);
                    model.BindText1 = heavyResult;
                }
                else
                {
                    var heavyResult = await RunTask(index);
                    model.BindText2 = heavyResult;
                }
            }
        }
        async Task<string> RunTask(int index)
        {
            await Task.Delay(1000 * index); // 2.
            return "hogehoge"; // 3.
        }
        */
    }
}
