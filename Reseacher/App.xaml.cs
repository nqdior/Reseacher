using MahApps.Metro;
using MetroRadiance.UI;
using Reseacher.Properties;
using System.Windows;

namespace Reseacher
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            System.Console.WriteLine("Hello,World!!");

            // Dragablz Theme
            var mahTheme = ThemeManager.GetAppTheme(Settings.Default.ThemeColor);
            var mahAccent = ThemeManager.GetAccent(Settings.Default.AccentColor);
            ThemeManager.ChangeAppStyle(this, mahAccent, mahTheme);

            // Application Theme
            var radTheme = StyleConverter.FromMahTheme(mahTheme);
            var radAccent = StyleConverter.FromMahAccent(mahAccent);
            ThemeService.Current.Register(this, radTheme, radAccent);

            // Splash screen show
#if !DEBUG
            new LoadingWindow().ShowDialog();
#else
            Nucleus.ReadConfig();
#endif
            ShutdownMode = ShutdownMode.OnMainWindowClose;

            /*
            // style change https://mahapps.com/guides/styles.html 
            ThemeManager.AddAccent("Reseacher", new Uri("pack://application:,,,/Reseacher;component/ReseacherTheme.xaml"));

            Tuple<AppTheme, MahApps.Metro.Accent> theme = ThemeManager.DetectAppStyle(Current);
            ThemeManager.ChangeAppStyle(Current, ThemeManager.GetAccent("Reseacher"), theme.Item1);
            */
        }
    }
}
