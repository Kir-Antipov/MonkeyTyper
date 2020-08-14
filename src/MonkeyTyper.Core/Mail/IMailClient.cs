using MailKit;
using MailKit.Security;
using MimeKit;
using MonkeyTyper.Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonkeyTyper.Core.Mail
{
    /// <summary>
    /// A mail client that can be used to send email messages.
    /// </summary>
    public interface IMailClient : IAsyncDisposable, IDisposable
    {
        /// <summary>
        /// Gets the protocol supported by the message service.
        /// </summary>
        string Protocol { get; }

        /// <summary>
        /// Occurs before sending the message.
        /// </summary>
        /// <remarks>
        /// Message events occur in the following order: 
        /// <list type="number">
        /// <item><see cref="MessageMutationError"/> (if something went wrong)</item>
        /// <item><see cref="BeforeMessageSent"/></item>
        /// <item><see cref="MessageTransferError"/> (if something went wrong)</item>
        /// <item><see cref="MessageTransferProgress"/></item>
        /// <item><see cref="MessageTransferError"/> (if something went wrong)</item>
        /// <item><see cref="MessageSent"/></item>
        /// </list>
        /// </remarks>
        event EventHandler<BeforeMessageSentEventArgs> BeforeMessageSent;

        /// <summary>
        /// Occurs during message's being sent.
        /// </summary>
        /// <inheritdoc cref="BeforeMessageSent"/>
        event EventHandler<MessageTransferProgressEventArgs> MessageTransferProgress;

        /// <summary>
        /// Occurs after the message is sent.
        /// </summary>
        /// <inheritdoc cref="BeforeMessageSent"/>
        event EventHandler<MessageSentEventArgs> MessageSent;

        /// <summary>
        /// Occurs after message mutation error.
        /// </summary>
        /// <inheritdoc cref="BeforeMessageSent"/>
        event EventHandler<MessageMutationErrorEventArgs> MessageMutationError;

        /// <summary>
        /// Occurs after message transfer error.
        /// </summary>
        /// <inheritdoc cref="BeforeMessageSent"/>
        event EventHandler<MessageTransferErrorEventArgs> MessageTransferError;

        /// <summary>
        /// Authenticate using the specified user name and password.
        /// </summary>
        /// <param name="uri">The server URI.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <param name="encoding">The encoding to use for the user's credentials.</param>
        /// <exception cref="InvalidOperationException">
        /// The client is not connected or is already authenticated.
        /// </exception>
        /// <exception cref="AuthenticationException">
        /// Authentication using the supplied credentials has failed.
        /// </exception>
        /// <exception cref="SocketException">
        /// A socket error occurred trying to connect to the remote host.
        /// </exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <exception cref="ProtocolException">A protocol error occurred.</exception>
        void Authenticate(Uri uri, string userName, string password, Encoding? encoding = null);

        /// <summary>
        /// Asynchronously authenticate using the specified user name and password.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An asynchronous task context.</returns>
        /// <exception cref="OperationCanceledException">
        /// The operation was canceled via the cancellation token.
        /// </exception>
        /// <inheritdoc cref="Authenticate(Uri, string, string, Encoding)"/>
        Task AuthenticateAsync(Uri uri, string userName, string password, Encoding? encoding = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="InvalidOperationException">
        /// The client is not connected.
        /// </exception>
        void Send(MimeMessage message);

        /// <summary>
        /// Asynchronously sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An asynchronous task context.</returns>
        /// <exception cref="InvalidOperationException">
        /// The client is not connected.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// The operation was canceled via the cancellation token.
        /// </exception>
        Task SendAsync(MimeMessage message, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="messageBuilder">The message.</param>
        /// <param name="record">The data record.</param>
        /// <param name="messageMutators"> Mutators transforming the message.</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        /// <returns>
        /// If the message was successfully sent, returns its instance;
        /// otherwise, <see langword="null"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The client is not connected.
        /// </exception>
        MimeMessage? Send(IMessageBuilder messageBuilder, IDataRecord record, IEnumerable<IMessageMutator>? messageMutators = null, IFormatProvider? formatProvider = null);

        /// <summary>
        /// Asynchronously sends the specified message.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="OperationCanceledException">
        /// The operation was canceled via the cancellation token.
        /// </exception>
        /// <inheritdoc cref="Send(IMessageBuilder, IDataRecord, IEnumerable{IMessageMutator}, IFormatProvider)"/>
        Task<MimeMessage?> SendAsync(IMessageBuilder messageBuilder, IDataRecord record, IEnumerable<IMessageMutator>? messageMutators = null, IFormatProvider? formatProvider = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a message for each entry of <see cref="IDataReader"/>.
        /// </summary>
        /// <param name="messageBuilder">The message.</param>
        /// <param name="reader">The data reader.</param>
        /// <param name="messageMutators">
        /// Mutators transforming the message for each entry of <see cref="IDataReader"/>.
        /// </param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        /// <returns>Sent messages.</returns>
        /// <exception cref="InvalidOperationException">
        /// The client is not connected.
        /// </exception>
        IEnumerable<MimeMessage> BulkSend(IMessageBuilder messageBuilder, IDataReader reader, IEnumerable<IMessageMutator>? messageMutators = null, IFormatProvider? formatProvider = null);

        /// <summary>
        /// Asynchronously sends a message
        /// for each entry of <see cref="IDataReader"/>.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="OperationCanceledException">
        /// The operation was canceled via the cancellation token.
        /// </exception>
        /// <inheritdoc cref="BulkSend(IMessageBuilder, IDataReader, IEnumerable{IMessageMutator}, IFormatProvider)"/>
        IAsyncEnumerable<MimeMessage> BulkSendAsync(IMessageBuilder messageBuilder, IDataReader reader, IEnumerable<IMessageMutator>? messageMutators = null, IFormatProvider? formatProvider = null, CancellationToken cancellationToken = default);
    }
}
