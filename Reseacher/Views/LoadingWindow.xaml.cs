using MetroRadiance.UI;
using MetroRadiance.UI.Controls;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace Reseacher
{
    public partial class LoadingWindow : MetroWindow
    {
        Timer timer = new Timer();

        string message = @"
A ll-Range
U nited
R egional
O n-memory
R edeploys
A pplication";

        string message_init = @"
A
U
R
O
R
A";
        int count = -1;
        int secCount = 0;

        public LoadingWindow()
        {
            InitializeComponent();

            label_def.Content = message;
            label_red.Content = message_init;

            Background = (ThemeService.Current.Theme == Theme.Dark) ? Brushes.Black : Brushes.White;
            var animeName = (ThemeService.Current.Theme == Theme.Dark) ? "loading_b" : "loading_w";
            anime.Source = new Uri($"Resources/{animeName}.gif", UriKind.RelativeOrAbsolute);

            timer.Tick += Timer_Tick;
            timer.Interval = 30;
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
            else if (count == message.Length - 1)
            {
                Visibility = Visibility.Visible;
                timer.Stop();
                return;
            }
            count += 1;
            label.Content = label.Content.ToString() + message[count];
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
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
            Console.WriteLine("orattaaaaaa");
            Close();
        }

        async Task<string> progress(int percent)
        {
            await Task.Delay(20);
            return $"{percent}% ";
        }

        private void Anime_MediaEnded(object sender, RoutedEventArgs e)
        {

        }
    }
}
