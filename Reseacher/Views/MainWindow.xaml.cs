using Reseacher.Properties;
using System.Windows;

namespace Reseacher
{
    public partial class MainWindow : MetroRadiance.UI.Controls.MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            /* http://iyemon018.hatenablog.com/entry/2016/03/04/150330 */
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dragblzControl.AddServerAddPage();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (dragblzControl.Hacky == false)
            {
                stripArea.MinWidth = 0;
                aaaaa.Height = 
                bbbbb.Height =
                stripArea.Width =
                ccccccccc.Width = new GridLength(0);
                dddddd.Visibility = Visibility.Hidden;

                return;
            }
            dragblzControl.FormLoadEnded();

            Nucleus.ReadConfig();
            managePage.DataContext = new TreeModelView(Nucleus.ServerRack);
        }

        private void GridSplitter_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Settings.Default.StripWidth = stripArea.Width.Value;
            Settings.Default.Save();
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
