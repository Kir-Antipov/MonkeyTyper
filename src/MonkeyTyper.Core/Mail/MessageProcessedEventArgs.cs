using MimeKit;
using System;

namespace MonkeyTyper.Core.Mail
{
    /// <summary>
    /// The <see cref="EventArgs"/> for <see cref="IMailClient.MessageProcessed"/>.
    /// </summary>
    public class MessageProcessedEventArgs : EventArgs
    {
        #region Var
        /// <summary>
        /// The message.
        /// </summary>
        public MimeMessage Message { get; }

        /// <summary>
        /// Zero-based message ordinal.
        /// </summary>
        public int Ordinal { get; }

        /// <summary>
        /// The total number of messages to be
        /// sent during the current iteration.
        /// </summary>
        /// <remarks>
        /// If the total number of messages isn't known,
        /// the value of this property will be -1.
        /// </remarks>
        public int Count { get; }

        /// <summary>
        /// Indicates whether the message was sent or skipped.
        /// </summary>
        public ProcessedType ProcessedType { get; }
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="MessageProcessedEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ordinal">Zero-based message ordinal.</param>
        /// <param name="count">
        /// The total number of messages to be
        /// sent by the calling method.
        /// </param>
        /// <param name="processedType">Indicates whether the message was sent or skipped.</param>
        public MessageProcessedEventArgs(MimeMessage message, int ordinal, int count, ProcessedType processedType)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Count = count >= -1 ? count : throw new ArgumentOutOfRangeException(nameof(count));
            Ordinal = ordinal >= 0 ? ordinal : throw new ArgumentOutOfRangeException(nameof(ordinal));
            ProcessedType = processedType;
        }
        #endregion
    }
}
