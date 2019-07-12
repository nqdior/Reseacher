using System;
using System.Windows;
using System.Windows.Controls;

namespace Reseacher
{
    public partial class Title : UserControl
    {
        // public string LogoLabel { set => label.Content = value; }
        public Title()
        {
            InitializeComponent();
        }

        private void Anime_MediaEnded(object sender, RoutedEventArgs e)
        {
            anime.Position = new TimeSpan(0, 0, 1);
            anime.Play();

            // welcome.Content = "Welcome to 'Reseacher'";
            MediaEnded(sender, e);
        }
        public delegate void MediaEndedHandler(object sender, RoutedEventArgs e);
        public event MediaEndedHandler MediaEnded;
    }
}
