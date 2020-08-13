using MimeKit;
using System;

namespace MonkeyTyper.Core.Extensions
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) helpful methods for
    /// MimeKit objects.
    /// </summary>
    public static class MimeKitExtensions
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <param name="builder"> <see cref="BodyBuilder"/> instance.</param>
        /// <returns>A new object that is a copy of this instance.</returns>
        public static BodyBuilder Clone(this BodyBuilder builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            BodyBuilder clone = new BodyBuilder
            {
                HtmlBody = builder.HtmlBody,
                TextBody = builder.TextBody
            };

            foreach (MimeEntity entity in builder.Attachments)
                clone.Attachments.Add(entity);

            foreach (MimeEntity entity in builder.LinkedResources)
                clone.LinkedResources.Add(entity);

            return clone;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <param name="message"> <see cref="MimeMessage"/> instance.</param>
        /// <returns>A new object that is a copy of this instance.</returns>
        public static MimeMessage Clone(this MimeMessage message)
        {
            _ = message ?? throw new ArgumentNullException(nameof(message));

            MimeMessage clone = new MimeMessage
            {
                 Date = message.Date,
                 Importance = message.Importance,
                 Priority = message.Priority,
                 ResentDate = message.ResentDate,
                 XPriority = message.XPriority
            };
            if (message.Sender is { })
                clone.Sender = message.Sender;
            if (message.ResentSender is { })
                clone.ResentSender = message.ResentSender;
            if (message.Subject is { })
                clone.Subject = message.Subject;
            if (message.InReplyTo is { })
                clone.InReplyTo = message.InReplyTo;
            if (message.MessageId is { })
                clone.MessageId = message.MessageId;
            if (message.ResentMessageId is { })
                clone.ResentMessageId = message.ResentMessageId;
            if (message.MimeVersion is { })
                clone.MimeVersion = message.MimeVersion;
            if (message.Body is { })
                clone.Body = message.Body;
            clone.From.AddRange(message.From);
            clone.ResentFrom.AddRange(message.ResentFrom);
            clone.ReplyTo.AddRange(message.ReplyTo);
            clone.ResentReplyTo.AddRange(message.ResentReplyTo);
            clone.To.AddRange(message.To);
            clone.ResentTo.AddRange(message.ResentTo);
            clone.Cc.AddRange(message.Cc);
            clone.ResentCc.AddRange(message.ResentCc);
            clone.Bcc.AddRange(message.Bcc);
            clone.ResentBcc.AddRange(message.ResentBcc);
            foreach (Header header in message.Headers)
                if (!clone.Headers.Contains(header.Id))
                    clone.Headers.Add(header);

            return clone;
        }
    }
}
