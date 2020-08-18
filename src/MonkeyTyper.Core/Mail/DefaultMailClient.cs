using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using MonkeyTyper.Core.Data;
using MonkeyTyper.Core.Format;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonkeyTyper.Core.Mail
{
    /// <summary>
    /// Default implementation of the <see cref="IMailClient"/>.
    /// </summary>
    public class DefaultMailClient : IMailClient
    {
        #region Var
        /// <inheritdoc/>
        public virtual string Protocol => "smtp";

        /// <inheritdoc/>
        public virtual event EventHandler<BeforeMessageSentEventArgs>? BeforeMessageSent;

        /// <inheritdoc/>
        public virtual event EventHandler<MessageTransferProgressEventArgs>? MessageTransferProgress;

        /// <inheritdoc/>
        public virtual event EventHandler<MessageSentEventArgs>? MessageSent;

        /// <inheritdoc/>
        public event EventHandler<MessageProcessedEventArgs>? MessageProcessed;

        /// <inheritdoc/>
        public virtual event EventHandler<MessageMutationErrorEventArgs>? MessageMutationError;

        /// <inheritdoc/>
        public virtual event EventHandler<MessageTransferErrorEventArgs>? MessageTransferError;

        private IStringFormatter Formatter { get; }

        private SmtpClient Client { get; }
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="DefaultMailClient"/> class.
        /// </summary>
        /// <param name="formatter">Custom string formatter.</param>
        public DefaultMailClient(IStringFormatter? formatter = null)
        {
            Formatter = formatter ?? new DefaultStringFormatter();
            Client = new SmtpClient();
        }

        /// <inheritdoc/>
        ~DefaultMailClient() => Dispose(false);
        #endregion

        #region Functions
        /// <inheritdoc/>
        public virtual void Authenticate(Uri uri, string userName, string password, Encoding? encoding = null)
        {
            _ = uri ?? throw new ArgumentNullException(nameof(uri));
            _ = userName ?? throw new ArgumentNullException(nameof(userName));
            _ = password ?? throw new ArgumentNullException(nameof(password));

            Client.Connect(uri);
            Client.Authenticate(encoding ?? Encoding.UTF8, userName, password);
        }

        /// <inheritdoc/>
        public Task AuthenticateAsync(Uri uri, string userName, string password, Encoding? encoding = null, CancellationToken cancellationToken = default)
        {
            _ = uri ?? throw new ArgumentNullException(nameof(uri));
            _ = userName ?? throw new ArgumentNullException(nameof(userName));
            _ = password ?? throw new ArgumentNullException(nameof(password));

            return AuthenticateAsyncImpl(uri, userName, password, encoding, cancellationToken);
        }

        /// <inheritdoc cref="IMailClient.AuthenticateAsync(Uri, string, string, Encoding, CancellationToken)"/>
        protected virtual async Task AuthenticateAsyncImpl(Uri uri, string userName, string password, Encoding? encoding, CancellationToken cancellationToken)
        {
            await Client.ConnectAsync(uri, cancellationToken).ConfigureAwait(false);
            await Client.AuthenticateAsync(encoding ?? Encoding.UTF8, userName, password, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public MimeMessage? Send(IMessageBuilder messageBuilder, IDataRecord record, IEnumerable<IMessageMutator>? messageMutators = null, IFormatProvider? formatProvider = null)
        {
            _ = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
            _ = record ?? throw new ArgumentNullException(nameof(record));
            messageMutators ??= Array.Empty<IMessageMutator>();

            return SendImpl(messageBuilder, record, messageMutators, formatProvider ?? CultureInfo.InvariantCulture, 0, 1, out _);
        }

        /// <param name="index">Zero-based message ordinal.</param>
        /// <param name="count">
        /// The total number of messages to be
        /// sent by the calling method.
        /// </param>
        /// <param name="stop">Indicates whether the calling method should terminate execution.</param>
        /// <inheritdoc cref="IMailClient.Send(IMessageBuilder, IDataRecord, IEnumerable{IMessageMutator}, IFormatProvider)"/>
        protected virtual MimeMessage? SendImpl(IMessageBuilder messageBuilder, IDataRecord record, IEnumerable<IMessageMutator> messageMutators, IFormatProvider formatProvider, int index, int count, out bool stop)
        {
            stop = false;
            IMessageBuilder message = messageBuilder.Clone();
            FormatMessage(message, record, formatProvider);
            foreach (IMessageMutator mutator in messageMutators)
                try
                {
                    message = mutator.Mutate(message, record);
                }
                catch (Exception e)
                {
                    MessageMutationErrorEventArgs errorArgs = new MessageMutationErrorEventArgs(message, mutator, e);
                    MessageMutationError?.Invoke(this, errorArgs);
                    if (errorArgs.Stop)
                    {
                        stop = true;
                        return null;
                    }
                    else if (errorArgs.Skip)
                    {
                        MessageProcessed?.Invoke(this, new MessageProcessedEventArgs(message.ToMessage(), index, count, ProcessedType.Skipped));
                        return null;
                    }
                    else if (errorArgs.SkipMutation)
                    {
                        continue;
                    }
                    throw;
                }

            BeforeMessageSentEventArgs beforeArgs = new BeforeMessageSentEventArgs(message);
            BeforeMessageSent?.Invoke(this, beforeArgs);
            if (beforeArgs.Stop)
            {
                stop = true;
                return null;
            }
            else if (beforeArgs.Skip)
            {
                MessageProcessed?.Invoke(this, new MessageProcessedEventArgs(message.ToMessage(), index, count, ProcessedType.Skipped));
                return null;
            }

            MimeMessage result = message.ToMessage();
            int attempt = 0;
            while (true)
                try
                {
                    ITransferProgress transferProgress = new TransferProgress((bytesTransferred, totalSize) =>
                        MessageTransferProgress?.Invoke(this, new MessageTransferProgressEventArgs(result, bytesTransferred, totalSize)));

                    Client.Send(result, progress: transferProgress);
                    MessageSent?.Invoke(this, new MessageSentEventArgs(result));
                    MessageProcessed?.Invoke(this, new MessageProcessedEventArgs(result, index, count, ProcessedType.Sent));
                    return result;
                }
                catch (Exception e)
                {
                    MessageTransferErrorEventArgs errorArgs = new MessageTransferErrorEventArgs(result, e, ++attempt);
                    MessageTransferError?.Invoke(this, errorArgs);
                    if (errorArgs.Stop)
                    {
                        stop = true;
                        return null;
                    }
                    else if (errorArgs.Skip)
                    {
                        MessageProcessed?.Invoke(this, new MessageProcessedEventArgs(result, index, count, ProcessedType.Skipped));
                        return null;
                    }
                    else if (errorArgs.Retry)
                    {
                        continue;
                    }
                    throw;
                }
        }

        /// <inheritdoc/>
        public Task<MimeMessage?> SendAsync(IMessageBuilder messageBuilder, IDataRecord record, IEnumerable<IMessageMutator>? messageMutators = null, IFormatProvider? formatProvider = null, CancellationToken cancellationToken = default)
        {
            _ = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
            _ = record ?? throw new ArgumentNullException(nameof(record));
            messageMutators ??= Array.Empty<IMessageMutator>();

            return SendAsyncImpl(messageBuilder, record, messageMutators, formatProvider ?? CultureInfo.InvariantCulture, 0, 1, cancellationToken).ContinueWith(x => x.Result.Message);
        }

        /// <inheritdoc cref="IMailClient.SendAsync(IMessageBuilder, IDataRecord, IEnumerable{IMessageMutator}, CancellationToken)"/>
        protected virtual async Task<(MimeMessage? Message, bool Stop)> SendAsyncImpl(IMessageBuilder messageBuilder, IDataRecord record, IEnumerable<IMessageMutator> messageMutators, IFormatProvider formatProvider, int index, int count, CancellationToken cancellationToken)
        {
            IMessageBuilder message = messageBuilder.Clone();
            FormatMessage(message, record, formatProvider);
            foreach (IMessageMutator mutator in messageMutators)
                try
                {
                    message = mutator.Mutate(message, record);
                }
                catch (Exception e)
                {
                    MessageMutationErrorEventArgs errorArgs = new MessageMutationErrorEventArgs(message, mutator, e);
                    MessageMutationError?.Invoke(this, errorArgs);
                    if (errorArgs.Stop)
                    {
                        return (null, true);
                    }
                    else if (errorArgs.Skip)
                    {
                        MessageProcessed?.Invoke(this, new MessageProcessedEventArgs(message.ToMessage(), index, count, ProcessedType.Skipped));
                        return (null, false);
                    }
                    else if (errorArgs.SkipMutation)
                    {
                        continue;
                    }
                    throw;
                }

            BeforeMessageSentEventArgs beforeArgs = new BeforeMessageSentEventArgs(message);
            BeforeMessageSent?.Invoke(this, beforeArgs);
            if (beforeArgs.Stop)
            {
                return (null, true);
            }
            else if (beforeArgs.Skip)
            {
                MessageProcessed?.Invoke(this, new MessageProcessedEventArgs(message.ToMessage(), index, count, ProcessedType.Skipped));
                return (null, false);
            }

            MimeMessage result = message.ToMessage();
            int attempt = 0;
            while (true)
                try
                {
                    ITransferProgress transferProgress = new TransferProgress((bytesTransferred, totalSize) =>
                        MessageTransferProgress?.Invoke(this, new MessageTransferProgressEventArgs(result, bytesTransferred, totalSize)));

                    await Client.SendAsync(result, cancellationToken, progress: transferProgress).ConfigureAwait(false);
                    MessageSent?.Invoke(this, new MessageSentEventArgs(result));
                    MessageProcessed?.Invoke(this, new MessageProcessedEventArgs(result, index, count, ProcessedType.Sent));
                    return (result, false);
                }
                catch (Exception e)
                {
                    MessageTransferErrorEventArgs errorArgs = new MessageTransferErrorEventArgs(result, e, ++attempt);
                    MessageTransferError?.Invoke(this, errorArgs);
                    if (errorArgs.Stop)
                    {
                        return (null, true);
                    }
                    else if (errorArgs.Skip)
                    {
                        MessageProcessed?.Invoke(this, new MessageProcessedEventArgs(result, index, count, ProcessedType.Skipped));
                        return (null, false);
                    }
                    else if (errorArgs.Retry)
                    {
                        continue;
                    }
                    throw;
                }
        }

        /// <inheritdoc/>
        public IEnumerable<MimeMessage> BulkSend(IMessageBuilder messageBuilder, IDataReader reader, IEnumerable<IMessageMutator>? messageMutators = null, IFormatProvider? formatProvider = null)
        {
            _ = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
            _ = reader ?? throw new ArgumentNullException(nameof(reader));
            messageMutators ??= Array.Empty<IMessageMutator>();
            formatProvider ??= CultureInfo.InvariantCulture;

            return BulkSendImpl(messageBuilder, reader, messageMutators, formatProvider);
        }

        /// <inheritdoc cref="IMailClient.BulkSend(IMessageBuilder, IDataReader, IEnumerable{IMessageMutator}, IFormatProvider)"/>
        protected virtual IEnumerable<MimeMessage> BulkSendImpl(IMessageBuilder messageBuilder, IDataReader reader, IEnumerable<IMessageMutator> messageMutators, IFormatProvider formatProvider)
        {
            do
            {
                int i = -1;
                while (reader.Read())
                {
                    MimeMessage? message = SendImpl(messageBuilder, reader, messageMutators, formatProvider, ++i, reader.Count, out bool stop);
                    switch ((message, stop))
                    {
                        case (_, true):
                            yield break;
                        case ({ }, false):
                            yield return message!;
                            break;
                        default:
                            continue;
                    }
                }
            }
            while (reader.NextResult());
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<MimeMessage> BulkSendAsync(IMessageBuilder messageBuilder, IDataReader reader, IEnumerable<IMessageMutator>? messageMutators = null, IFormatProvider? formatProvider = null, CancellationToken cancellationToken = default)
        {
            _ = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
            _ = reader ?? throw new ArgumentNullException(nameof(reader));
            messageMutators ??= Array.Empty<IMessageMutator>();
            formatProvider ??= CultureInfo.InvariantCulture;

            return BulkSendAsyncImpl(messageBuilder, reader, messageMutators, formatProvider, cancellationToken);
        }

        /// <inheritdoc cref="IMailClient.BulkSendAsync(IMessageBuilder, IDataReader, IEnumerable{IMessageMutator}, IFormatProvider, CancellationToken)"/>
        protected virtual async IAsyncEnumerable<MimeMessage> BulkSendAsyncImpl(IMessageBuilder messageBuilder, IDataReader reader, IEnumerable<IMessageMutator> messageMutators, IFormatProvider formatProvider, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            do
            {
                int i = -1;
                while (await reader.ReadAsync().ConfigureAwait(false))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    switch (await SendAsyncImpl(messageBuilder, reader, messageMutators, formatProvider, ++i, reader.Count, cancellationToken).ConfigureAwait(false))
                    {
                        case (_, true):
                            yield break;
                        case (MimeMessage message, false):
                            yield return message;
                            break;
                        default:
                            continue;
                    }
                }
            }
            while (await reader.NextResultAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Substitutes dynamic values ​​into <see cref="IMessageBuilder"/>.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="record">The data record.</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        protected virtual void FormatMessage(IMessageBuilder message, IDataRecord record, IFormatProvider formatProvider)
        {
            if (!string.IsNullOrEmpty(message.Subject))
                message.Subject = Formatter.Format(message.Subject!, record, formatProvider);
            if (!string.IsNullOrEmpty(message.TextBody))
                message.TextBody = Formatter.Format(message.TextBody!, record, formatProvider);
            if (!string.IsNullOrEmpty(message.HtmlBody))
                message.HtmlBody = Formatter.Format(message.HtmlBody!, record, formatProvider);
            FormatAddresses(message.To, record, formatProvider);
            FormatAddresses(message.From, record, formatProvider);
        }

        /// <summary>
        /// Substitutes dynamic values ​​into <paramref name="addresses"/>.
        /// </summary>
        /// <param name="addresses">Collection of internet addresses.</param>
        /// <param name="record">The data record.</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        protected virtual void FormatAddresses(IList<InternetAddress> addresses, IDataRecord record, IFormatProvider formatProvider)
        {
            for (int i = 0; i < addresses.Count; ++i)
            {
                if (!(addresses[i] is MailboxAddress mailbox))
                    continue;

                string address = Formatter.Format(mailbox.Address, record, formatProvider);
                if (string.IsNullOrEmpty(mailbox.Name))
                {
                    addresses[i] = MailboxAddress.Parse(address);
                }
                else
                {
                    string name = Formatter.Format(mailbox.Name, record, formatProvider);
                    addresses[i] = new MailboxAddress(name, address);
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous dispose operation.
        /// </returns>
        public virtual async ValueTask DisposeAsync()
        {
            try
            {
                await Client.DisconnectAsync(true);
            }
            catch { }

            try
            {
                Client.Dispose();
            }
            catch { }
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <param name="disposing">
        /// Indicates whether this method was called from 
        /// managed code (<see langword="true"/>)
        /// or from the finalizer (<see langword="false"/>).
        /// </param>
        /// <inheritdoc cref="IDisposable.Dispose"/>
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                Client.Dispose();
            }
            catch { }
        }
        #endregion
    }
}
