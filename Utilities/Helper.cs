using System.Configuration;

namespace TodoList.Utilities
{
    public static class Helper
    {
        public static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
