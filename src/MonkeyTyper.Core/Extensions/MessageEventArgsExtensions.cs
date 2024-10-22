﻿using MonkeyTyper.Core.Mail;
using System;
using System.Threading;
using System.Threading.Tasks;

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
        /// Sets <see cref="MessageTransferErrorEventArgs.Retry"/> to <see langword="true"/>
        /// after the specified timeout.
        /// </summary>
        /// <typeparam name="TEventArgs">
        /// The <see cref="MessageTransferErrorEventArgs"/> instance's type.
        /// </typeparam>
        /// <param name="args">
        /// The <see cref="MessageTransferErrorEventArgs"/> instance.
        /// </param>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds for which the thread is suspended.
        /// </param>
        /// <returns>
        /// The <see cref="MessageTransferErrorEventArgs"/> instance.
        /// </returns>
        public static TEventArgs RetryAfter<TEventArgs>(this TEventArgs args, int millisecondsTimeout) where TEventArgs : MessageTransferErrorEventArgs
        {
            _ = args ?? throw new ArgumentNullException(nameof(args));

            Thread.Sleep(millisecondsTimeout);
            args.Retry = true;
            return args;
        }

        /// <inheritdoc cref="RetryAfter{TEventArgs}(TEventArgs, int)"/>
        public static async Task<TEventArgs> RetryAfterAsync<TEventArgs>(this TEventArgs args, int millisecondsTimeout) where TEventArgs : MessageTransferErrorEventArgs
        {
            _ = args ?? throw new ArgumentNullException(nameof(args));

            await Task.Delay(millisecondsTimeout).ConfigureAwait(false);
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
