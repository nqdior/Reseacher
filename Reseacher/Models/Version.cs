using System.Diagnostics;
using System.Reflection;

namespace Reseacher
{
    internal static class Version
    {
        private static string _self => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

        public static string Self => $"System version : {_self}";

        private static string _aurora => "";// FileVersionInfo.GetVersionInfo(@"./Aurora.dll").FileVersion;

        public static string Aurora => $"Engine version : {_aurora}";

        public static string Information => $"{Self} | {Aurora}";
    }
}
