using MonkeyTyper.Core.Data;

namespace MonkeyTyper.Core.Mail
{
    /// <summary>
    /// Describes a transformation of a message object.
    /// </summary>
    public interface IMessageMutator
    {
        /// <summary>
        /// Transforms the <see cref="IMessageBuilder"/>
        /// into a new form.
        /// </summary>
        /// <param name="message">
        /// The <see cref="IMessageBuilder"/> instance.
        /// </param>
        /// <param name="record">Data source record.</param>
        /// <returns>
        /// Transformed <see cref="IMessageBuilder"/> instance.
        /// </returns>
        IMessageBuilder Mutate(IMessageBuilder message, IDataRecord record);
    }
}
