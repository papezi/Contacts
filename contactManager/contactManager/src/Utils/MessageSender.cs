using contactManager.src.Messages;
using GalaSoft.MvvmLight.Messaging;

namespace contactManager.src.Utils
{
    /// <summary>
    /// The class for sending messages.
    /// </summary>
    public static class MessageSender
    {
        /// <summary>
        /// Send report to main window.
        /// </summary>
        /// <param name="text">Message content.</param>
        public static void SendReport(string text)
        {
            TextMessage msg = new TextMessage { Text = text};
            Messenger.Default.Send(msg);
        }
    }
}
