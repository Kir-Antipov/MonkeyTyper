using MimeKit;
using MonkeyTyper.Core.Extensions;
using System;
using System.Text.RegularExpressions;

namespace MonkeyTyper.Core.Mail
{
    /// <summary>
    /// Default implementation of the <see cref="IMessageBuilder"/>.
    /// </summary>
    public sealed class DefaultMessageBuilder : IMessageBuilder
    {
        private static Regex TagExtractor { get; } = new Regex(@"<[^>]*>", RegexOptions.Compiled | RegexOptions.Multiline);
        private static Regex WhitespaceExtractor { get; } = new Regex(@" {2,}", RegexOptions.Compiled | RegexOptions.Multiline);

        private MimeMessage Message { get; }
        private BodyBuilder Builder { get; }

        /// <inheritdoc cref="DefaultMessageBuilder(MimeMessage, BodyBuilder)"/>
        public DefaultMessageBuilder() : this(new MimeMessage(), new BodyBuilder()) { }

        /// <summary>
        /// Initialize a new instance of the <see cref="DefaultMessageBuilder"/> class.
        /// </summary>
        /// <param name="message">A MIME message container.</param>
        /// <param name="builder">A message body builder.</param>
        public DefaultMessageBuilder(MimeMessage message, BodyBuilder builder)
        {
            _ = message ?? throw new ArgumentNullException(nameof(message));
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            (Message, Builder) = (message, builder);
        }

        /// <inheritdoc cref="IMessageBuilder.Importance"/>
        public MessageImportance Importance 
        {
            get => Message.Importance;
            set => Message.Importance = value;
        }

        /// <inheritdoc cref="IMessageBuilder.Priority"/>
        public MessagePriority Priority
        {
            get => Message.Priority;
            set => Message.Priority = value;
        }

        /// <inheritdoc cref="IMessageBuilder.XPriority"/>
        public XMessagePriority XPriority
        {
            get => Message.XPriority;
            set => Message.XPriority = value;
        }

        /// <inheritdoc cref="IMessageBuilder.Sender"/>
        public MailboxAddress? Sender
        {
            get => Message.Sender;
            set => Message.Sender = value;
        }

        /// <inheritdoc cref="IMessageBuilder.ResentSender"/>
        public MailboxAddress? ResentSender
        {
            get => Message.ResentSender;
            set => Message.ResentSender = value;
        }

        /// <inheritdoc cref="IMessageBuilder.From"/>
        public InternetAddressList From => Message.From;

        /// <inheritdoc cref="IMessageBuilder.ResentFrom"/>
        public InternetAddressList ResentFrom => Message.ResentFrom;

        /// <inheritdoc cref="IMessageBuilder.ReplyTo"/>
        public InternetAddressList ReplyTo => Message.ReplyTo;

        /// <inheritdoc cref="IMessageBuilder.ResentReplyTo"/>
        public InternetAddressList ResentReplyTo => Message.ResentReplyTo;

        /// <inheritdoc cref="IMessageBuilder.To"/>
        public InternetAddressList To => Message.To;

        /// <inheritdoc cref="IMessageBuilder.ResentTo"/>
        public InternetAddressList ResentTo => Message.ResentTo;

        /// <inheritdoc cref="IMessageBuilder.Cc"/>
        public InternetAddressList Cc => Message.Cc;

        /// <inheritdoc cref="IMessageBuilder.ResentCc"/>
        public InternetAddressList ResentCc => Message.ResentCc;

        /// <inheritdoc cref="IMessageBuilder.Bcc"/>
        public InternetAddressList Bcc => Message.Bcc;

        /// <inheritdoc cref="IMessageBuilder.ResentBcc"/>
        public InternetAddressList ResentBcc => Message.ResentBcc;

        /// <inheritdoc cref="IMessageBuilder.Subject"/>
        public string? Subject
        {
            get => Message.Subject;
            set => Message.Subject = value;
        }

        /// <inheritdoc cref="IMessageBuilder.Date"/>
        public DateTimeOffset Date
        {
            get => Message.Date;
            set => Message.Date = value;
        }

        /// <inheritdoc cref="IMessageBuilder.ResentDate"/>
        public DateTimeOffset ResentDate
        {
            get => Message.ResentDate;
            set => Message.ResentDate = value;
        }

        /// <inheritdoc cref="IMessageBuilder.References"/>
        public MessageIdList References => Message.References;

        /// <inheritdoc cref="IMessageBuilder.InReplyTo"/>
        public string? InReplyTo
        {
            get => Message.InReplyTo;
            set => Message.InReplyTo = value;
        }

        /// <inheritdoc cref="IMessageBuilder.MessageId"/>
        public string? MessageId
        {
            get => Message.MessageId;
            set => Message.MessageId = value;
        }

        /// <inheritdoc cref="IMessageBuilder.ResentMessageId"/>
        public string? ResentMessageId
        {
            get => Message.ResentMessageId;
            set => Message.ResentMessageId = value;
        }

        /// <inheritdoc cref="IMessageBuilder.MimeVersion"/>
        public Version? MimeVersion
        {
            get => Message.MimeVersion;
            set => Message.MimeVersion = value;
        }

        /// <inheritdoc cref="IMessageBuilder.Headers"/>
        public HeaderList Headers => Message.Headers;

        /// <inheritdoc cref="IMessageBuilder.Attachments"/>
        public AttachmentCollection Attachments => Builder.Attachments;

        /// <inheritdoc cref="IMessageBuilder.LinkedResources"/>
        public AttachmentCollection LinkedResources => Builder.LinkedResources;

        /// <inheritdoc cref="IMessageBuilder.TextBody"/>
        public string? TextBody
        {
            get => Builder.TextBody;
            set => Builder.TextBody = value;
        }

        /// <inheritdoc cref="IMessageBuilder.HtmlBody"/>
        public string? HtmlBody
        {
            get => Builder.HtmlBody;
            set => Builder.HtmlBody = value;
        }


        /// <inheritdoc cref="ICloneable.Clone"/>
        object ICloneable.Clone() => Clone();

        /// <inheritdoc cref="IMessageBuilder.Clone"/>
        public IMessageBuilder Clone() => new DefaultMessageBuilder(Message.Clone(), Builder.Clone());

        /// <inheritdoc cref="IMessageBuilder.ToMessage"/>
        public MimeMessage ToMessage()
        {
            string oldTextValue = Builder.TextBody;
            if (!string.IsNullOrEmpty(Builder.HtmlBody) && string.IsNullOrEmpty(Builder.TextBody))
                Builder.TextBody = WhitespaceExtractor.Replace(TagExtractor.Replace(Builder.HtmlBody, x => " "), x => " ").Trim();
            Message.Body = Builder.ToMessageBody();
            Builder.TextBody = oldTextValue;
            return Message;
        }
    }
}
