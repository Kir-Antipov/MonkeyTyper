using System;

namespace MonkeyTyper.Core.Mail
{
    /// <summary>
    /// The <see cref="EventArgs"/> for <see cref="IMailClient.BeforeMessageSent"/>.
    /// </summary>
    public class BeforeMessageSentEventArgs : EventArgs, IControllableMessageEvent
    {
        #region Var
        /// <summary>
        /// The message.
        /// </summary>
        public IMessageBuilder Message { get; }

        /// <summary>
        /// If this property is set to <see langword="true"/>,
        /// the <see cref="Message"/> won't be sent.
        /// </summary>
        public bool Skip { get; set; }

        /// <summary>
        /// If this property is set to <see langword="true"/>,
        /// the <see cref="Message"/> won't be sent,
        /// and the send method will be terminated.
        /// </summary>
        public bool Stop { get; set; }
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="BeforeMessageSentEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public BeforeMessageSentEventArgs(IMessageBuilder message) => Message = message ?? throw new ArgumentNullException(nameof(message));
        #endregion
    }
}
