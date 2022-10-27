using BlazorApp.Models;
using System.Runtime.CompilerServices;

namespace BlazorApp.Extensions
{
    public static class PaymentStatusExtensions
    {
        private record StatusName(string lang, PaymentStatus status, string name);

        private static StatusName[] statusNames = new StatusName[] {
            new StatusName("en", PaymentStatus.Wait, "Wait"),
            new StatusName("en", PaymentStatus.InProcess, "In the process of payment"),
            new StatusName("en", PaymentStatus.Paid, "Paid"),
            new StatusName("en", PaymentStatus.Error, "Payment error"),

            new StatusName("uk", PaymentStatus.Wait, "Очікування"),
            new StatusName("uk", PaymentStatus.InProcess, "У процесі оплати"),
            new StatusName("uk", PaymentStatus.Paid, "Сплачено"),
            new StatusName("uk", PaymentStatus.Error, "Помилка при оплаті"),
        };

        public static string Display(this PaymentStatus value, string twoLetterISOLanguageName = "en")
        {
            if (!new string[] { "en", "uk" }.Contains(twoLetterISOLanguageName))
                throw new ArgumentException(nameof(twoLetterISOLanguageName));
            return statusNames.First(i => i.lang == twoLetterISOLanguageName && i.status == value).name;
        }


    }
}
