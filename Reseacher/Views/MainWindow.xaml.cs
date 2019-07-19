using MetroRadiance.UI.Controls;
using Reseacher.Properties;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;

namespace Reseacher
{
    public partial class MainWindow : MetroRadiance.UI.Controls.MetroWindow
    {
        private static bool initialized = false;

        public MainWindow()
        {
            InitializeComponent();

            if (initialized)
            {
                _initializeSubComponent();
            }
            else
            {
                _initializeMainComponent();
            }
            /* http://iyemon018.hatenablog.com/entry/2016/03/04/150330 */
        }

        private void _initializeMainComponent()
        {
            /*  */

            /* テーマアイテムの描画 */
            var appThemeMenu = new AppThemeMenu
            {
                Width = 60,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            appThemeMenu.SetValue(Grid.ColumnProperty, 2);
            appThemeMenu.SetValue(WindowChrome.IsHitTestVisibleInChromeProperty, true);
            CaptionBarArea.Children.Add(appThemeMenu);

            /* システムボタンの描画 */
            var systemButtons = new SystemButtons
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 0, 0, 5)
            };
            systemButtons.SetValue(Grid.ColumnProperty, 3);
            CaptionBarArea.Children.Add(systemButtons);

            /* メインコンテンツの描画 */
            // グリッドの描画
            var treeViewArea = new ColumnDefinition
            {
                MinWidth = 30,
                Width = new GridLength(200)
            };
            var splitterArea = new ColumnDefinition
            {
                Width = new GridLength(4)
            };
            var dragblzArea = new ColumnDefinition
            {
            };
            ContentArea.ColumnDefinitions.Add(treeViewArea);
            ContentArea.ColumnDefinitions.Add(splitterArea);
            ContentArea.ColumnDefinitions.Add(dragblzArea);

            // コンテンツの描画
            var manageArea = new ManageArea();
            manageArea.SetValue(Grid.ColumnProperty, 0);

            var splitter = new GridSplitter
            {
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            splitter.SetValue(Grid.RowProperty, 1);
            splitter.SetValue(Grid.ColumnProperty, 1);

            var dragblzControl = new DragablzControl();
            dragblzControl.SetValue(Grid.ColumnProperty, 2);
            ContentArea.Children.Add(manageArea);
            ContentArea.Children.Add(splitter);
            ContentArea.Children.Add(dragblzControl);

            dragblzControl.FormLoadEnded();
            manageArea.DataContext = new TreeModelView(Nucleus.ServerRack);

            initialized = true;
        }

        private void _initializeSubComponent()
        {
            var dragblzControl = new DragablzControl();
            ContentArea.Children.Add(dragblzControl);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // dragblzControl.AddServerAddPage();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void GridSplitter_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // Settings.Default.StripWidth = stripArea.Width.Value;
           // Settings.Default.Save();
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
