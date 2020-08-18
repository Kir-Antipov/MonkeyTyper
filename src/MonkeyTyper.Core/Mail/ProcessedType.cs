namespace MonkeyTyper.Core.Mail
{
    /// <summary>
    /// Indicates whether the message was sent or skipped.
    /// </summary>
    public enum ProcessedType
    {
        /// <summary>
        /// Message was skipped.
        /// </summary>
        Skipped,

        /// <summary>
        /// Message was sent.
        /// </summary>
        Sent
    }
}
