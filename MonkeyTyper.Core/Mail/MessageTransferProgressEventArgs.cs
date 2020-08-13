using MimeKit;
using System;

namespace MonkeyTyper.Core.Mail
{
    /// <summary>
    /// The <see cref="EventArgs"/> for <see cref="IMailClient.MessageTransferProgress"/>.
    /// </summary>
    public class MessageTransferProgressEventArgs : EventArgs
    {
        #region Var
        /// <summary>
        /// The message.
        /// </summary>
        public MimeMessage Message { get; }

        /// <summary>
        /// The number of bytes transferred.
        /// </summary>
        public long BytesTransferred { get; }

        /// <summary>
        /// The total size, in bytes, of the message being transferred.
        /// </summary>
        /// <remarks>
        /// If the message size isn't known,
        /// the value of this property will be -1.
        /// </remarks>
        public long TotalSize { get; }
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="MessageTransferProgressEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="bytesTransferred">The number of bytes transferred.</param>
        /// <param name="totalSize">The total size, in bytes, of the message being transferred.</param>
        public MessageTransferProgressEventArgs(MimeMessage message, long bytesTransferred, long totalSize)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            BytesTransferred = bytesTransferred >= 0 ? bytesTransferred : throw new ArgumentOutOfRangeException(nameof(bytesTransferred));
            TotalSize = totalSize >= -1 ? totalSize : throw new ArgumentOutOfRangeException(nameof(totalSize));
        }

        /// <inheritdoc cref="MessageTransferProgressEventArgs(MimeMessage, long, long)"/>
        public MessageTransferProgressEventArgs(MimeMessage message, long bytesTransferred) : this(message, bytesTransferred, -1) { }
        #endregion
    }
}
