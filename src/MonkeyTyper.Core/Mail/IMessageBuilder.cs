using MimeKit;
using System;

namespace MonkeyTyper.Core.Mail
{
    /// <summary>
    /// A message builder.
    /// </summary>
    public interface IMessageBuilder : ICloneable
    {
        /// <summary>
        /// Gets or sets the value of the Importance header.
        /// </summary>
        MessageImportance Importance { get; set; }

        /// <summary>
        /// Gets or sets the value of the Priority header.
        /// </summary>
        MessagePriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the value of the X-Priority header.
        /// </summary>
        XMessagePriority XPriority { get; set; }

        /// <summary>
        /// Gets or sets the address in the Sender header.
        /// </summary>
        /// <remarks>
        /// The sender may differ from the addresses in <see cref="From"/> if the message
        /// was sent by someone on behalf of someone else.
        /// </remarks>
        MailboxAddress? Sender { get; set; }

        /// <summary>
        /// Gets or sets the address in the Resent-Sender header.
        /// </summary>
        /// <remarks>
        /// The resent sender may differ from the addresses in <see cref="ResentFrom"/>
        /// if the message was sent by someone on behalf of someone else.
        /// </remarks>
        MailboxAddress? ResentSender { get; set; }

        /// <summary>
        /// Gets the list of addresses in the From header.
        /// </summary>
        /// <remarks>
        /// The "From" header specifies the author(s) of the message.
        /// If more than one <see cref="MailboxAddress"/> is added to the list of "From" addresses,
        /// the <see cref="Sender"/> should be set to the single <see cref="MailboxAddress"/>
        /// of the personal actually sending the message.
        /// </remarks>
        InternetAddressList From { get; }

        /// <summary>
        /// Gets the list of addresses in the Resent-From header.
        /// </summary>
        /// <remarks>
        /// The "Resent-From" header specifies the author(s) of the messagebeing resent.
        /// If more than one <see cref="MailboxAddress"/> is added to the list of "Resent-From"
        /// addresses, the <see cref="ResentSender"/> should be set to the single <see cref="MailboxAddress"/>
        /// of the personal actually sending the message.
        /// </remarks>
        InternetAddressList ResentFrom { get; }

        /// <summary>
        /// Gets the list of addresses in the Reply-To header.
        /// </summary>
        /// <remarks>
        /// When the list of addresses in the Reply-To header is not empty, it contains the
        /// address(es) where the author(s) of the message prefer that replies be sent.
        /// When the list of addresses in the Reply-To header is empty, replies should be
        /// sent to the mailbox(es) specified in the From header.
        /// </remarks>
        InternetAddressList ReplyTo { get; }

        /// <summary>
        /// Gets the list of addresses in the Resent-Reply-To header.
        /// </summary>
        /// <remarks>
        /// When the list of addresses in the Resent-Reply-To header is not empty, it contains
        /// the address(es) where the author(s) of the resent message prefer that replies
        /// be sent.
        /// When the list of addresses in the Resent-Reply-To header is empty, replies should
        /// be sent to the mailbox(es) specified in the Resent-From header.
        /// </remarks>
        InternetAddressList ResentReplyTo { get; }

        /// <summary>
        /// Gets the list of addresses in the To header.
        /// </summary>
        InternetAddressList To { get; }

        /// <summary>
        /// Gets the list of addresses in the Resent-To header.
        /// </summary>
        InternetAddressList ResentTo { get; }

        /// <summary>
        /// Gets the list of addresses in the Cc header.
        /// </summary>
        /// <remarks>
        /// The addresses in the Cc header are secondary recipients of the message and are
        /// usually not the individuals being directly addressed in the content of the message.
        /// </remarks>
        InternetAddressList Cc { get; }

        /// <summary>
        /// Gets the list of addresses in the Resent-Cc header.
        /// </summary>
        /// <remarks>
        /// The addresses in the Resent-Cc header are secondary recipients of the message and are
        /// usually not the individuals being directly addressed in the content of the message.
        /// </remarks>
        InternetAddressList ResentCc { get; }

        /// <summary>
        /// Gets the list of addresses in the Bcc header.
        /// </summary>
        /// <remarks>
        /// Recipients in the Blind-Carpbon-Copy list will not be visible to the other recipients
        /// of the message.
        /// </remarks>
        InternetAddressList Bcc { get; }

        /// <summary>
        /// Gets the list of addresses in the Resent-Bcc header.
        /// </summary>
        /// <remarks>
        /// Recipients in the Resent-Bcc list will not be visible to the other recipients
        /// of the message.
        /// </remarks>
        InternetAddressList ResentBcc { get; }

        /// <summary>
        /// Gets or sets the subject of the message.
        /// </summary>
        string? Subject { get; set; }

        /// <summary>
        /// Gets or sets the date of the message.
        /// </summary>
        /// <remarks>
        /// If the date is not explicitly set before the message is written to a stream,
        /// the date will default to the exact moment when it is written to said stream.
        /// </remarks>
        DateTimeOffset Date { get; set; }

        /// <summary>
        /// Gets or sets the Resent-Date of the message.
        /// </summary>
        DateTimeOffset ResentDate { get; set; }

        /// <summary>
        /// Gets the list of references to other messages.
        /// </summary>
        /// <remarks>
        /// The References header contains a chain of Message-Ids back to the original message
        /// that started the thread.
        /// </remarks>
        MessageIdList References { get; }

        /// <summary>
        /// Gets or sets the Message-Id that this message is replying to.
        /// </summary>
        /// <remarks>
        /// If the message is a reply to another message, it will typically use the In-Reply-To
        /// header to specify the Message-Id of the original message being replied to.
        /// </remarks>
        string? InReplyTo { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        string? MessageId { get; set; }

        /// <summary>
        /// Gets or sets the Resent-Message-Id header.
        /// </summary>
        string? ResentMessageId { get; set; }

        /// <summary>
        /// Gets or sets the MIME-Version.
        /// </summary>
        Version? MimeVersion { get; set; }

        /// <summary>
        /// Gets the list of headers.
        /// </summary>
        HeaderList Headers { get; }

        /// <summary>
        /// Represents a collection of file attachments
        /// that will be included in the message.
        /// </summary>
        AttachmentCollection Attachments { get; }

        /// <summary>
        /// Represents a special type of attachment
        /// which are linked to from the <see cref="HtmlBody"/>.
        /// </summary>
        AttachmentCollection LinkedResources { get; }

        /// <summary>
        /// Represents the plain-text formatted version of the message body.
        /// </summary>
        string? TextBody { get; set; }

        /// <summary>
        /// Represents the html formatted version of the message body
        /// and may link to any of the <see cref="LinkedResources"/>.
        /// </summary>
        string? HtmlBody { get; set; }

        /// <summary>
        /// Constructs the message based on the text-based bodies,
        /// the linked resources, the attachments.
        /// </summary>
        /// <returns>Constructed <see cref="MimeMessage"/>.</returns>
        MimeMessage ToMessage();

        /// <inheritdoc cref="ICloneable.Clone"/>
        new IMessageBuilder Clone();
    }
}
