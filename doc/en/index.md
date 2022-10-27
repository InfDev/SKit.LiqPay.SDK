# SKit.LiqPay.SDK [⇑](../../README.md)

## General information

SDK for payment service clients "[LiqPay](https://www.liqpay.ua/en/documentation/api/aquiring)".

The SDK provides basic functions for payment acceptance integration modules on the site. Supported functionality:

- generating an HTML form or a hyperlink to go to the [LiqPay payment page](https://www.liqpay.ua/en/documentation/api/aquiring/checkout).
  Custom templates are also supported
- decoding [PDT and IPN](pdt-ipn.md) notifications from LiqPay, checking by signature and converting to a typed form
- [Server-Server payment](https://www.liqpay.ua/en/documentation/api/aquiring/pay) via API without going to the LiqPay payment page
- [getting payment state](https://www.liqpay.ua/en/documentation/api/information/status)
- [issuing the invoice](https://www.liqpay.ua/en/documentation/api/aquiring/invoice/doc) to the client's email in Sever-Server mode
- [invoice cancelation](https://www.liqpay.ua/en/documentation/api/aquiring/invoice_cancel/doc)

General restrictions: **API version 3**, **.NET platform 6.0+**

Advantages:

- strong typing
- availability of tests
- easy expandability
- possibility of low-level logging
- code with documentation comments
- solution includes SDK usage examples
  - **ConsoleApp** - formation of payment buttons, payment by card through the API and receipt of the payment state. [More...](console-app.md)
  - **BlazorApp** - web application on the Blazor Server platform with Minimal API architecture. [More...](blazor-app.md)

  Results of application operations (button html, requests and responses in json) is placed in the solution folder **assets/outputs**.
