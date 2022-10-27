namespace BlazorApp
{
    public class LiqPayCallbackOptions
    {
        public static string LiqPayCallbackSection = "LiqPayCallback";

        public string PdtUrl { get; set; } = string.Empty;
        public string IpnUrl { get; set; } = string.Empty;
    }
}
