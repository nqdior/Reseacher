using Reseacher.Core;
using System;
using System.Windows;

namespace Reseacher
{
    public partial class MainWindow : MetroRadiance.UI.Controls.MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine(Properties.Settings.Default.Opacitys);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Opacity = Properties.Settings.Default.Opacitys;
            new ManageWindow().Show();
        }

        /// <summary> 最大化時にステータスバーが隠れないようにする対応。対応策模索中。 </summary>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double PSH = SystemParameters.PrimaryScreenHeight;
            int PSBH = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            double ratio = PSH / PSBH;
            int TaskBarHeight = PSBH - System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            TaskBarHeight *= (int)ratio;

            margin.Height = WindowState == WindowState.Maximized ? new GridLength(TaskBarHeight) : new GridLength(0);
        }
    }
}
