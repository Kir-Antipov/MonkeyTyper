using MimeKit;
using System;

namespace MonkeyTyper.Core.Mail
{
    /// <summary>
    /// The <see cref="EventArgs"/> for <see cref="IMailClient.MessageTransferError"/>.
    /// </summary>
    public class MessageTransferErrorEventArgs : EventArgs, IControllableMessageEvent
    {
        #region Var
        /// <summary>
        /// The message.
        /// </summary>
        public MimeMessage Message { get; }

        /// <summary>
        /// An exception occurred during an attempt to send a message.
        /// </summary>
        /// <remarks>
        /// If none of the values (<see cref="Skip"/>, <see cref="Retry"/> or
        /// <see cref="Stop"/>) ​​are true, this exception
        /// will be thrown by the active message sending method.
        /// </remarks>
        public Exception Exception { get; }

        /// <summary>
        /// The sequence one-based number of a failed attempt to send a message.
        /// </summary>
        public int Attempt { get; }

        /// <summary>
        /// If this property is set to <see langword="true"/>,
        /// the <see cref="Message"/> won't be sent.
        /// </summary>
        public virtual bool Skip { get; set; }

        /// <summary>
        /// If this property is set to <see langword="true"/>,
        /// will be made an attempt to resend the same message.
        /// </summary>
        public virtual bool Retry { get; set; }

        /// <summary>
        /// If this property is set to <see langword="true"/>,
        /// the <see cref="Message"/> won't be sent,
        /// and the send method will be terminated.
        /// </summary>
        public virtual bool Stop { get; set; }
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="MessageTransferErrorEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">
        /// An exception occurred during an attempt to send a <paramref name="message"/>.
        /// </param>
        /// <param name="attempt">
        /// The sequence one-based number of a failed attempt to send a message.
        /// </param>
        public MessageTransferErrorEventArgs(MimeMessage message, Exception exception, int attempt)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
            Attempt = attempt > 0 ? attempt : throw new ArgumentOutOfRangeException(nameof(attempt));
        }

        /// <inheritdoc cref="MessageTransferErrorEventArgs(MimeMessage, Exception, int)"/>
        public MessageTransferErrorEventArgs(MimeMessage message, Exception exception) : this(message, exception, 1) { }
        #endregion
    }
}
