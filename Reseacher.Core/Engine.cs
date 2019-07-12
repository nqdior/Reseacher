using System;

namespace Reseacher.Core
{
    public enum Engine
    {
        SQLServer = 0,
        MySQL = 1,
        PostgreSQL = 2,
        MariaDB = 3,
        SQLite = 4,
        Oracle = 5
    }

    public static partial class EngineExtend
    {
        public static int ToIndex(this Engine engine) => (int)engine;

        public static Engine ToEngine(this int index) => (Engine)Enum.ToObject(typeof(Engine), index);

        public static Engine ToEngine(this string value) => (Engine)Enum.Parse(typeof(Engine), value, true);
    }
}
