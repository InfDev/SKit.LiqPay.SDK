using SKit.LiqPay.SDK;
using System.Text.Json;

namespace BlazorApp
{
    public static class AppUtils
    {
        public static string TestsDirectory = Path.Combine(Environment.CurrentDirectory, "./../../assets/outputs/BlazorApp");

        static AppUtils()
        {
            if (!Directory.Exists(TestsDirectory))
                Directory.CreateDirectory(TestsDirectory);
        }

        public static string ToJson(object obj)
        {
            if (obj != null)
            {
                var json = JsonSerializer.Serialize(obj, LiqPayGatewayBase.JsonSerializerOptions);
                return json;
            }
            return "{}";
        }

        public static void SaveText(string text, string fileName)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            var path = Path.Combine(TestsDirectory, fileName);
            File.WriteAllText(path, text);
        }

    }
}
