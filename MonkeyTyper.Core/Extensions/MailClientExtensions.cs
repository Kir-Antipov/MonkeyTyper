using MonkeyTyper.Core.Data;
using MonkeyTyper.Core.Mail;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonkeyTyper.Core.Extensions
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) helpful methods for objects
    /// that implement <see cref="IMailClient"/>.
    /// </summary>
    public static class MailClientExtensions
    {
        /// <param name="client">The <see cref="IMailClient"/> instance.</param>
        /// <param name="host">The server host.</param>
        /// <param name="port">The server port.</param>
        /// <param name="useSsl">Indicates whether to use ssl.</param>
        /// <inheritdoc cref="IMailClient.Authenticate(Uri, string, string, Encoding)"/>
        public static void Authenticate(this IMailClient client, string host, int port, bool useSsl, string userName, string password, Encoding? encoding = null)
        {
            _ = client ?? throw new ArgumentNullException(nameof(client));
            _ = host ?? throw new ArgumentNullException(nameof(host));
            _ = userName ?? throw new ArgumentNullException(nameof(userName));
            _ = password ?? throw new ArgumentNullException(nameof(password));

            Uri uri = new UriBuilder(useSsl ? $"{client.Protocol}s" : client.Protocol, host, port).Uri;
            client.Authenticate(uri, userName, password, encoding);
        }

        /// <inheritdoc cref="Authenticate(IMailClient, string, int, bool, string, string, Encoding)"/>
        public static void Authenticate(this IMailClient client, string host, int port, string userName, string password, Encoding? encoding = null) =>
            Authenticate(client, host, port, useSsl: false, userName, password, encoding);

        /// <inheritdoc cref="Authenticate(IMailClient, string, int, bool, string, string, Encoding)"/>
        public static void Authenticate(this IMailClient client, string host, string userName, string password, Encoding? encoding = null) =>
            Authenticate(client, host, port: 0, useSsl: false, userName, password, encoding);

        /// <param name="client">The <see cref="IMailClient"/> instance.</param>
        /// <param name="host">The server host.</param>
        /// <param name="port">The server port.</param>
        /// <param name="useSsl">Indicates whether to use ssl.</param>
        /// <inheritdoc cref="IMailClient.AuthenticateAsync(Uri, string, string, Encoding, CancellationToken)"/>
        public static Task AuthenticateAsync(this IMailClient client, string host, int port, bool useSsl, string userName, string password, Encoding? encoding = null, CancellationToken cancellationToken = default)
        {
            _ = client ?? throw new ArgumentNullException(nameof(client));
            _ = host ?? throw new ArgumentNullException(nameof(host));
            _ = userName ?? throw new ArgumentNullException(nameof(userName));
            _ = password ?? throw new ArgumentNullException(nameof(password));

            Uri uri = new UriBuilder(useSsl ? $"{client.Protocol}s" : client.Protocol, host, port).Uri;
            return client.AuthenticateAsync(uri, userName, password, encoding, cancellationToken);
        }

        /// <inheritdoc cref="AuthenticateAsync(IMailClient, string, int, bool, string, string, Encoding, CancellationToken)"/>
        public static Task AuthenticateAsync(this IMailClient client, string host, int port, string userName, string password, Encoding? encoding = null, CancellationToken cancellationToken = default) =>
            AuthenticateAsync(client, host, port, useSsl: false, userName, password, encoding, cancellationToken);

        /// <inheritdoc cref="AuthenticateAsync(IMailClient, string, int, bool, string, string, Encoding, CancellationToken)"/>
        public static Task AuthenticateAsync(this IMailClient client, string host, string userName, string password, Encoding? encoding = null, CancellationToken cancellationToken = default) =>
            AuthenticateAsync(client, host, port: 0, useSsl: false, userName, password, encoding, cancellationToken);

        /// <returns></returns>
        /// <inheritdoc cref="IMailClient.BulkSend(IMessageBuilder, IDataReader, IEnumerable{IMessageMutator}, IFormatProvider)"/>
        public static void BulkSendAll(this IMailClient client, IMessageBuilder messageBuilder, IDataReader reader, IEnumerable<IMessageMutator>? messageMutators = null, IFormatProvider? formatProvider = null)
        {
            _ = client ?? throw new ArgumentNullException(nameof(client));
            _ = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
            _ = reader ?? throw new ArgumentNullException(nameof(reader));

            client.BulkSend(messageBuilder, reader, messageMutators, formatProvider).Consume();
        }

        /// <returns>An asynchronous task context.</returns>
        /// <inheritdoc cref="IMailClient.BulkSendAsync(IMessageBuilder, IDataReader, IEnumerable{IMessageMutator}, IFormatProvider, CancellationToken)"/>
        public static Task BulkSendAllAsync(this IMailClient client, IMessageBuilder messageBuilder, IDataReader reader, IEnumerable<IMessageMutator>? messageMutators = null, IFormatProvider? formatProvider = null, CancellationToken cancellationToken = default)
        {
            _ = client ?? throw new ArgumentNullException(nameof(client));
            _ = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
            _ = reader ?? throw new ArgumentNullException(nameof(reader));

            return client.BulkSendAsync(messageBuilder, reader, messageMutators, formatProvider, cancellationToken).ConsumeAsync();
        }

        private static void Consume(this IEnumerable enumerable)
        {
            foreach (var _ in enumerable) ;
        }

        private static async Task ConsumeAsync(this IAsyncEnumerable<object?> enumerable)
        {
            await foreach (var _ in enumerable) ;
        }
    }
}
