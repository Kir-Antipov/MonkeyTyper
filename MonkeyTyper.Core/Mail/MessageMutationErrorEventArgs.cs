using System;

namespace MonkeyTyper.Core.Mail
{
    /// <summary>
    /// The <see cref="EventArgs"/> for <see cref="IMailClient.MessageMutationError"/>.
    /// </summary>
    public class MessageMutationErrorEventArgs : IControllableMessageEvent
    {
        #region Var
        /// <summary>
        /// The message.
        /// </summary>
        public IMessageBuilder Message { get; }

        /// <summary>
        /// The problematic mutator.
        /// </summary>
        public IMessageMutator Mutator { get; }

        /// <summary>
        /// The exception thrown by the <see cref="Mutator"/>.
        /// </summary>
        /// <remarks>
        /// If none of the values (<see cref="Skip"/>, <see cref="SkipMutation"/> or
        /// <see cref="Stop"/>) ​​are true, this exception
        /// will be thrown by the active message sending method.
        /// </remarks>
        public Exception Exception { get; }

        /// <summary>
        /// If this property is set to <see langword="true"/>,
        /// the <see cref="Message"/> won't be sent.
        /// </summary>
        public virtual bool Skip { get; set; }

        /// <summary>
        /// If this property is set to <see langword="true"/>,
        /// the problematic mutator will be skipped.
        /// </summary>
        public virtual bool SkipMutation { get; set; }

        /// <summary>
        /// If this property is set to <see langword="true"/>,
        /// the <see cref="Message"/> won't be sent,
        /// and the send method will be terminated.
        /// </summary>
        public virtual bool Stop { get; set; }
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="MessageMutationErrorEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="mutator">The problematic mutator.</param>
        /// <param name="exception">The exception thrown by the <paramref name="mutator"/>.</param>
        public MessageMutationErrorEventArgs(IMessageBuilder message, IMessageMutator mutator, Exception exception)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Mutator = mutator ?? throw new ArgumentNullException(nameof(mutator));
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }
        #endregion
    }
}
