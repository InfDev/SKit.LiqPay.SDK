# SKit.LiqPay.SDK [⇑](../../README.md)

## Загальні відомості

SDK для клієнтів платіжного сервісу "[LiqPay](https://www.liqpay.ua/documentation/api/aquiring)".

SDK пропонує базові функції для модулів інтеграції прийому платежів на сайті. Підтримуваний функціонал:

- генерація HTML-форми або гіперпосилання для переходу на [платіжну сторінку LiqPay](https://www.liqpay.ua/documentation/api/aquiring/checkout).
  Підтримуються також шаблони користувача
- декодування [PDT та IPN](pdt-ipn.md) повідомлень від LiqPay, перевірка по сигнатурі та перетворення в типізовану форму
- [оплата Server-Server](https://www.liqpay.ua/documentation/api/aquiring/pay) через API без переходу на платіжну сторінку LiqPay
- [отримання стану платежу](https://www.liqpay.ua/documentation/api/information/status)
- [виставлення рахунку](https://www.liqpay.ua/documentation/api/aquiring/invoice/doc) на e-mail замовника в режимі Server-Server
- [cкасування рахунку](https://www.liqpay.ua/documentation/api/aquiring/invoice_cancel/doc)

Загальні обмеження: **API версії 3**, **платформа .NET 6.0+**

Переваги:

- сувора типізація
- наявність тестів
- легка розширюваність
- можливість протоколювання обміну на низькому рівні
- код із коментарями до документації
- проект включає приклади використання SDK
  - **ConsoleApp** - формування кнопок оплати, оплата карткою через API та отримання стану платежу. [Докладніше...](console-app.md)
  - **BlazorApp** - веб-додаток на платформі Blazor Server з Minimal API архітектурою. [Докладніше...](blazor-app.md)

  Результати операцій додатків (html кнопок, запити та відповіді в json) розміщуються в папці рішення **assets/outputs**.
