using MetroRadiance.UI;
using MetroRadiance.UI.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Reseacher
{
    public partial class MainWindow : MetroWindow
    {
        private static bool initialized = false;

        public MainWindow()
        {
            InitializeComponent();

            if (initialized)
            {
                _initializeSubComponent();
            }
            else // draw subwindow
            {
                _initializeMainComponent();
            }
            /* http://iyemon018.hatenablog.com/entry/2016/03/04/150330 */
        }

        private void _initializeMainComponent()
        {
            /* メニューアイテムの描画 */
            var titleBarArea = new ColumnDefinition
            {
                Width = new GridLength(400)
            };
            var catchableArea = new ColumnDefinition();
            CaptionBarArea.ColumnDefinitions.Add(titleBarArea);
            CaptionBarArea.ColumnDefinitions.Add(catchableArea);

            var menuArea = new MenuArea();
            menuArea.SetValue(Grid.ColumnProperty, 0);
            CaptionBarArea.Children.Add(menuArea);

            // システムボタンの描画
            CaptionBarArea.Children.Add(_systemButtons);

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
            var dragblzArea = new ColumnDefinition();
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
            splitter.SetResourceReference(BackgroundProperty, "ThemeBrushKey");
            splitter.SetValue(Grid.RowProperty, 1);
            splitter.SetValue(Grid.ColumnProperty, 1);

            var dragblzControl = new DragablzControl();
            dragblzControl.SetValue(Grid.ColumnProperty, 2);
            ContentArea.Children.Add(manageArea);
            ContentArea.Children.Add(splitter);
            ContentArea.Children.Add(dragblzControl);

            dragblzControl.FormLoadEnded();
            manageArea.DataContext = new TreeModelView(Nucleus.ServerRack);

            /* イベントのハンドル */
            menuArea.newTab.Click += (_object, _e) => dragblzControl.AddServerAddPage();

            initialized = true;
        }

        private void _initializeSubComponent()
        {
            /* サブウィンドウ用タイトルの描画 */

            // メインタイトルを非表示
            title.Visibility = Visibility.Hidden;
            // サブウィンドウ用タイトルを描画    
            var titleBlock = new TextBlock
            {
                Text = "Reseacher Aurora",
                TextTrimming = TextTrimming.CharacterEllipsis,
                FontFamily = new FontFamily("Segoe UI Light"),
                FontSize = 16,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5, 0, 0, 0),
            };
            titleBlock.SetResourceReference(ForegroundProperty, "ForegroundBrushKey");
            CaptionBarArea.Children.Add(titleBlock);

            /* システムボタンの描画 */
            CaptionBarArea.Children.Add(_systemButtons);

            /* メインコンテンツの描画 */
            ContentArea.Children.Add(new DragablzControl());
        }

        private SystemButtons _systemButtons
        {
            get
            {
                var systemButtons = new SystemButtons
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(0, 0, 0, 5)
                };
                systemButtons.SetValue(Grid.ColumnProperty, 3);
                return systemButtons;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
