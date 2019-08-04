using MetroRadiance.UI;
using MetroRadiance.UI.Controls;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Reseacher
{
    public partial class LoadingWindow : MetroWindow
    {
        public string SelectedImage { get; set; }

        public RepeatBehavior RepeatBehavior = RepeatBehavior.Forever;

        private Timer timer = new Timer{ Interval = 10 };

        private readonly string message;

        int count = -1;
        int secCount = 0;

        public LoadingWindow()
        {
            InitializeComponent();
            
            label_red.Content = Properties.Resources.AURORA_HEAD;
            label_def.Content = Properties.Resources.AURORA_DETAIL;
            message = Properties.Resources.AURORA_DETAIL;
            animetion.DataContext = this;

            Background = (ThemeService.Current.Theme == Theme.Dark) ? Brushes.Black : Brushes.White;
            var animeName = (ThemeService.Current.Theme == Theme.Dark) ? "loading_b" : "loading_w";
            SelectedImage = $@"pack://application:,,,/images/{animeName}.gif";

            timer.Tick += Timer_Tick;
            timer.Start();      
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secCount += 1;
            if (secCount <= 10)
            {
                return;
            }
            if (count < 0)
            {
                label.Content = "";
            }
            else if (count >= message.Length - 1)
            {
                label_def.Visibility = Visibility.Hidden;
                label_red.Visibility = Visibility.Hidden;
                label.FontSize = 22;
                timer.Stop();
                timer.Interval = 300;
                timer.Start();
                logo_main.Visibility = Visibility.Visible;
                return;
            }
            count += 1;
            label.Content = label.Content.ToString() + message[count];
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            new Task(() => Nucleus.ReadConfig()).Start();

            ver_label.Content = Version.Information;
            Percent();
        }

        async void Percent()
        {
            for (var i = 0; i <= 100; i++)
            {
                label_per.Content = "Checking databases status ... " + await progress(i);
            }
            for (var i = 0; i <= 100; i++)
            {
                label_per.Content = "Awaking main process ... " + await progress(i);
            }
            label_per.Content = "Stand by.";
            Close();
        }

        async Task<string> progress(int percent)
        {
            await Task.Delay(20);
            return $"{percent}% ";
        }
    }
}
