using System;

namespace MonkeyTyper.Core.Mail
{
    /// <summary>
    /// Describes an <see cref="EventArgs"/> that can cancel
    /// the sending of a message or terminate the sending method.
    /// </summary>
    public interface IControllableMessageEvent
    {
        /// <summary>
        /// If this property is set to <see langword="true"/>,
        /// the message won't be sent.
        /// </summary>
        bool Skip { get; set; }

        /// <summary>
        /// If this property is set to <see langword="true"/>,
        /// the message won't be sent,
        /// and the send method will be terminated.
        /// </summary>
        bool Stop { get; set; }
    }
}
