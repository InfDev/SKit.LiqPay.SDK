
namespace SKit.LiqPay.SDK.Tests
{
    internal static class Utils
    {
        public static string OrderId { get; } = Guid.NewGuid().ToString();

        public static string OutputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "./../../../../../assets/outputs/Tests");

        static Utils()
        {
            if (!Directory.Exists(OutputDirectory))
                Directory.CreateDirectory(OutputDirectory);
        }

        public static void SaveToFile(string text, string filename)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (filename == null)
                throw new ArgumentNullException(nameof(filename));

            var path = Path.Combine(OutputDirectory, filename);
            File.WriteAllText(path, text);
        }
    }


}
