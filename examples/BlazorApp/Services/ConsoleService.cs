using BootstrapBlazor.Components;
using System.Collections.Concurrent;

namespace BlazorApp.Services
{
    public enum MsgType { Operation, Output, Request, Response };

    public class ConsoleService
    {
        public ConcurrentQueue<ConsoleMessageItem> Messages { get; set; } = new();

        public void ToConsole(string message, MsgType msgType)
        {
            if (message != null)
            {
                var msg = msgType == MsgType.Operation ? $"\r\n{DateTime.Now.ToString("HH:mm:ss")} {message}" : message;
                Messages.Enqueue(new ConsoleMessageItem
                {
                    Message = msg,
                    Color = MsgColor(msgType)
                });
                if (Messages.Count > 100)
                    Messages.TryDequeue(out var _);
            }
        }

        public void OnConsoleClearHandler()
        {
            _locker.WaitOne();
            while (!Messages.IsEmpty)
            {
                Messages.TryDequeue(out var _);
            }
            _locker.Set();
        }

        private static Color MsgColor(MsgType msgType) => msgType switch
        {
            MsgType.Request => Color.Info,
            MsgType.Response => Color.Warning,
            MsgType.Operation => Color.Danger,
            _ => Color.None
        };

        private CancellationTokenSource? CancelTokenSource { get; set; }
        private readonly AutoResetEvent _locker = new(true);
    }
}
