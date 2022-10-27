
using BlazorApp.Models;
using BootstrapBlazor.Components;
using System.Collections.Concurrent;

namespace BlazorApp.Services
{
    public class CheckoutStateService
    {
        private TestCard _card = Cards[0];

        public string Language { get; set; } = "uk";
        public string CardNumber 
        { 
            get => _card.Number;
            set => SetCardNumber(value); 
        }

        public event Action? OnChange;

        private void SetCardNumber(string value)
        {
            if (_card.Number != value)
            {
                var card = Cards.Find(x => x.Number == value);
                if (card != null)
                    _card = card;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public static List<TestCard> Cards = new List<TestCard>
            {
                new TestCard("4242424242424242", "Successful payment ⇓ cards", "Успішна оплата ⇓ картки"),
                new TestCard("4000000000003063", "Successful payment with 3DS", "Успішна оплата з 3DS"),
                new TestCard("4000000000003089", "Successful payment with OTP", "Успішна оплата з OTP"),
                new TestCard("4000000000003055", "Successful payment with CVV", "Успішна оплата з CVV"),
                new TestCard("4000000000000002", "Failure payment. Error code - limit", "Не успішна оплата. Код помилки - limit"),
                new TestCard("4000000000009995", "Failure payment. Error code - 9859", "Не успішна оплата. Код помилки - 9859"),
                new TestCard("sandbox_token", "Successful payment with token", "Успішна оплата по токені")
            };


    }
}
