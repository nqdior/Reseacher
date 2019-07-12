using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro;
using MetroRadiance.UI;
using Reseacher.Properties;

namespace Reseacher
{
    //lifted most of this code from MahApps demo, to help illustrate the Dragablz themese complying with MahApps.
    public class ThemeSelectionViewModel
    {
        public List<AccentColorMenuData> AccentColors { get; set; }
        public List<AppThemeMenuData> AppThemes { get; set; }

        public ThemeSelectionViewModel()
        {
            // create accent color menu items for the demo
            AccentColors = ThemeManager.Accents
                .Select(
                    a =>
                        new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                .ToList();

            // create metro theme color menu items for the demo
            AppThemes = ThemeManager.AppThemes
                .Select(
                    a =>
                        new AppThemeMenuData()
                        {
                            Name = a.Name,
                            BorderColorBrush = a.Resources["BlackColorBrush"] as Brush,
                            ColorBrush = a.Resources["WhiteColorBrush"] as Brush
                        })
                .ToList();
        }
    }

    public class AccentColorMenuData
    {
        public string Name { get; set; }
        public Brush BorderColorBrush { get; set; }
        public Brush ColorBrush { get; set; }

        private ICommand changeAccentCommand;

        public ICommand ChangeAccentCommand
        {
            get { return this.changeAccentCommand ?? (changeAccentCommand = new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = x => this.DoChangeTheme(x) }); }
        }

        protected virtual void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var accent = ThemeManager.GetAccent(Name);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);

            try
            {
                var metroAccent = StyleConverter.FromMahAccent(accent);
                ThemeService.Current.Register(Application.Current, ThemeService.Current.Theme, metroAccent);
            }
            catch { /* ignore */ }

            Settings.Default.AccentColor = Name;
            Settings.Default.ThemeColor = theme.Item1.Name;
            Settings.Default.Save();
        }
    }

    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(Name);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);

            var metroTheme = StyleConverter.FromMahTheme(appTheme);
            ThemeService.Current.Register(Application.Current, metroTheme, ThemeService.Current.Accent);

            Settings.Default.AccentColor = theme.Item2.Name;
            Settings.Default.ThemeColor = appTheme.Name;
            Settings.Default.Save();
        }
    }

    public class SimpleCommand : ICommand
    {
        public Predicate<object> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegate { get; set; }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
                return CanExecuteDelegate(parameter);
            return true; // if there is no can execute default to true
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            ExecuteDelegate?.Invoke(parameter);
        }
    }
}
