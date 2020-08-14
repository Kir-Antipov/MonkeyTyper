using MonkeyTyper.Core.Mail;
using System;

namespace MonkeyTyper.Core.Extensions
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) helpful methods for
    /// <see cref="IMailClient"/>'s <see cref="EventArgs"/>.
    /// </summary>
    public static class MessageEventArgsExtensions
    {
        /// <summary>
        /// Sets <see cref="IControllableMessageEvent.Skip"/> to <see langword="true"/>.
        /// </summary>
        /// <typeparam name="TEventArgs">
        /// The <see cref="IControllableMessageEvent"/> instance's type.
        /// </typeparam>
        /// <param name="args">
        /// The <see cref="IControllableMessageEvent"/> instance.
        /// </param>
        /// <returns>
        /// The <see cref="IControllableMessageEvent"/> instance.
        /// </returns>
        public static TEventArgs Skip<TEventArgs>(this TEventArgs args) where TEventArgs : IControllableMessageEvent
        {
            _ = args ?? throw new ArgumentNullException(nameof(args));

            args.Skip = true;
            return args;
        }

        /// <summary>
        /// Sets <see cref="IControllableMessageEvent.Stop"/> to <see langword="true"/>.
        /// </summary>
        /// <typeparam name="TEventArgs">
        /// The <see cref="IControllableMessageEvent"/> instance's type.
        /// </typeparam>
        /// <param name="args">
        /// The <see cref="IControllableMessageEvent"/> instance.
        /// </param>
        /// <returns>
        /// The <see cref="IControllableMessageEvent"/> instance.
        /// </returns>
        public static TEventArgs Stop<TEventArgs>(this TEventArgs args) where TEventArgs : IControllableMessageEvent
        {
            _ = args ?? throw new ArgumentNullException(nameof(args));

            args.Stop = true;
            return args;
        }

        /// <summary>
        /// Sets <see cref="MessageTransferErrorEventArgs.Retry"/> to <see langword="true"/>.
        /// </summary>
        /// <typeparam name="TEventArgs">
        /// The <see cref="MessageTransferErrorEventArgs"/> instance's type.
        /// </typeparam>
        /// <param name="args">
        /// The <see cref="MessageTransferErrorEventArgs"/> instance.
        /// </param>
        /// <returns>
        /// The <see cref="MessageTransferErrorEventArgs"/> instance.
        /// </returns>
        public static TEventArgs Retry<TEventArgs>(this TEventArgs args) where TEventArgs : MessageTransferErrorEventArgs
        {
            _ = args ?? throw new ArgumentNullException(nameof(args));

            args.Retry = true;
            return args;
        }

        /// <summary>
        /// Sets <see cref="MessageMutationErrorEventArgs.SkipMutation"/> to <see langword="true"/>.
        /// </summary>
        /// <typeparam name="TEventArgs">
        /// The <see cref="MessageMutationErrorEventArgs"/> instance's type.
        /// </typeparam>
        /// <param name="args">
        /// The <see cref="MessageMutationErrorEventArgs"/> instance.
        /// </param>
        /// <returns>
        /// The <see cref="MessageMutationErrorEventArgs"/> instance.
        /// </returns>
        public static TEventArgs SkipMutation<TEventArgs>(this TEventArgs args) where TEventArgs : MessageMutationErrorEventArgs
        {
            _ = args ?? throw new ArgumentNullException(nameof(args));

            args.SkipMutation = true;
            return args;
        }
    }
}
