using MimeKit;
using System;

namespace MonkeyTyper.Core.Mail
{
    /// <summary>
    /// The <see cref="EventArgs"/> for <see cref="IMailClient.MessageSent"/>.
    /// </summary>
    public class MessageSentEventArgs : EventArgs
    {
        /// <summary>
        /// The message.
        /// </summary>
        public MimeMessage Message { get; }

        /// <summary>
        /// Initialize a new instance of the <see cref="MessageSentEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public MessageSentEventArgs(MimeMessage message) => Message = message ?? throw new ArgumentNullException(nameof(message));
    }
}
