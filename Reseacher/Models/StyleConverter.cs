using MetroRadiance.UI;
using MahApps.Metro;
using System.Windows.Media;
using System.Linq;

namespace Reseacher
{
    public static class StyleConverter
    {
        public static Theme FromMahTheme(AppTheme appTheme)
        {
            return appTheme.Name == "BaseDark" ? Theme.Dark : Theme.Light;
        }
        
        public static MetroRadiance.UI.Accent FromMahAccent(MahApps.Metro.Accent accent)
        {
            try
            {
                return MetroRadiance.UI.Accent.GetAccentFromString(accent.Name);
            }
            catch /* if metroRadiance hasn't provided accent by mahApp's, then marApp's accent convert to metroRadiance color; */
            {
                var colorList = ThemeManager.Accents.Select(a => new { a.Name, Brush = a.Resources["AccentColorBrush"] as Brush });
                var selectedColor = colorList.FirstOrDefault(r => r.Name == accent.Name).Brush;
                var color = ((SolidColorBrush)selectedColor).Color;

                return MetroRadiance.UI.Accent.FromColor(color);
            }
        }
    }
}
