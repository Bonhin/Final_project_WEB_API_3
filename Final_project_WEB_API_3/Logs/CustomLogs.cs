using System.Text.Json;

namespace Final_project_WEB_API_3.Logs
{
    public class CustomLogs
    {
        const string PUT = "put";
        const string PATCH = "patch";
        const string DELETE = "delete";

        public static void SaveLog(string method, int? id, string title, object? entityBefore = null, object entityAfter = null)
        {
            var now = DateTime.Now.ToString("G");

            if (method.Equals(PUT, StringComparison.InvariantCultureIgnoreCase) || method.Equals(PATCH, StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine($"{now} - Board Game {id} - Alterado de {JsonSerializer.Serialize(entityBefore)} para {JsonSerializer.Serialize(entityAfter)}");
            }
            else if (method.Equals(DELETE, StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine($"{now} - Board Game {id} - {title} - Removido");
            }
        }
    }
}
