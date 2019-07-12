using MetroRadiance.UI;
using MahApps.Metro;
using System.Windows.Media;

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
                return MetroRadiance.UI.Accent.GetAccentFromString(accent.Name.ToString());
            }
            catch
            {
                var color = (Color)ColorConverter.ConvertFromString(accent.Name.ToString());
                return MetroRadiance.UI.Accent.FromColor(color);
            }
        }
    }
}
