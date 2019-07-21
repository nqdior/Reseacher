using FreeView.Sql;

namespace Reseacher
{
    public static class CommandFormatter
    {
        public static string Format(string query)
        {
            var rule = new SqlRule();
            var formatter = new SqlFormatter(rule);

            return formatter.Format(query);
        }
    }
}
