using BlazorApp.Models;
using BlazorApp.Repositories;
using BlazorApp.Services;
using BlazorApp.Extensions;
using SKit.LiqPay.SDK;

namespace BlazorApp.Endpoints
{
    public static class Endpoints
    {
        public static async Task PostLiqPayPdtHandler(LiqPayPackCallback pack,
            HttpContext context, ILiqPayPdtListener listener, OrderManager orderManager, ConsoleService consoleService)
        {
            (bool ok, LpPdtResponse? lpPdtResponse) = listener.DecodingParser(pack);
            if (ok)
            {
                var json = AppUtils.ToJson(lpPdtResponse!);
                consoleService.ToConsole("Received PDT Notification", MsgType.Operation);
                consoleService.ToConsole(json, MsgType.Response);
                AppUtils.SaveText(json, "PdtNotification.json");
                //if (lpPdtResponse?.Status == LpPaymentStatus.Success)
                //{
                await UpdateOrderState(lpPdtResponse, orderManager);
                context.Response.Redirect($"/checkout");
                //}
            }
            else
            {
                var text = String.Join(Environment.NewLine, listener.Errors.ToArray());
                AppUtils.SaveText(text, "PdtNotificationErrors.txt");
            }
            context.Response.Redirect($"/checkout");
        }

        public static async Task PostLiqPayIpnHandler(LiqPayPackCallback pack, 
            HttpContext context, ILiqPayIpnListener listener, OrderManager orderManager, ConsoleService consoleService)
        {
            (bool ok, LpIpnResponse? lpIpnResponse) = listener.DecodingParser(pack);
            if (ok)
            {
                var json = AppUtils.ToJson(lpIpnResponse!);
                consoleService.ToConsole("Received PDT Notification", MsgType.Operation);
                consoleService.ToConsole(json, MsgType.Response);
                AppUtils.SaveText(AppUtils.ToJson(lpIpnResponse!), "IpnNotification.json");
                //if (lpIpnResponse?.Status == LpPaymentStatus.Success)
                //{
                await UpdateOrderState(lpIpnResponse, orderManager);
                //context.Response.Redirect($"/checkout/{lpIpnResponse!.OrderId}");
                //}
            }
            else
            {
                var text = String.Join(Environment.NewLine, listener.Errors.ToArray());
                AppUtils.SaveText(text, "IpnNotificationErrors.txt");
            }
            //context.Response.Redirect($"/checkout");
        }

        public static async Task UpdateOrderState(LpPaymentStateBase? response, OrderManager orderManager)
        {
            if (int.TryParse(response?.OrderId, out var orderId))
            {
                var lpState = response?.Status;
                var order = await orderManager.FindOrder(orderId);
                if (order != null)
                {
                    order.LiqPayPaymentStatus = lpState!.Name;
                    order.PaymentStatus = StatusFromLiqPayStatus(lpState);
                    await orderManager.UpdateOrder(order);
                }
            }
        }

        private static PaymentStatus StatusFromLiqPayStatus(LpPaymentStatus lpStatus)
        {
            if (lpStatus == LpPaymentStatus.Error || lpStatus == LpPaymentStatus.Failure)
                return PaymentStatus.Error;
            if (LpPaymentStatus.FinalStatuses.Contains(lpStatus))
                return PaymentStatus.Paid;
            return PaymentStatus.InProcess;
        }

    }
}
