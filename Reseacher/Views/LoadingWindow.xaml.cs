using MetroRadiance.UI;
using MetroRadiance.UI.Controls;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Reseacher
{
    public partial class LoadingWindow : MetroWindow
    {
        public LoadingWindow()
        {
            InitializeComponent();

            Background = (ThemeService.Current.Theme == Theme.Dark) ? Brushes.Black : Brushes.White;
            var animeName = (ThemeService.Current.Theme == Theme.Dark) ? "loading_b" : "loading_w";
            drawer.anime.Source = new Uri($"Resources/{animeName}.gif", UriKind.RelativeOrAbsolute);
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

        private void LogoDrawer_MediaEnded(object sender, RoutedEventArgs e)
        {

        }
    }
}
