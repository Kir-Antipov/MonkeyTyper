using MonkeyTyper.Core.Plugins;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MonkeyTyper.WinForms.Data
{
    /// <summary>
    /// Application settings.
    /// </summary>
    [Guid("3275C6F8-CD41-4818-BE6D-08CDB677759F")]
    [DisplayName("Default app settings")]
    public class AppSettings : Settings
    {
        /// <summary>
        /// SMTP host.
        /// </summary>
        [DisplayName("SMTP host")]
        public string Host { get; set; } = "smtp.gmail.com";

        /// <summary>
        /// SMTP port.
        /// </summary>
        [DisplayName("SMTP port")]
        public int Port { get; set; } = 587;

        /// <summary>
        /// Specifies whether to use an SSL connection.
        /// </summary>
        [DisplayName("Use SMTP over SSL")]
        public bool UseSSL { get; set; } = false;

        /// <summary>
        /// Sender email address.
        /// </summary>
        [DisplayName("Sender email address")]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Sender email password.
        /// </summary>
        [DisplayName("Sender email password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Sender display name.
        /// </summary>
        [DisplayName("Sender display name")]
        public string SenderName { get; set; } = string.Empty;

        /// <summary>
        /// Default data reader's type or Guid.
        /// </summary>
        [DisplayName("Data reader")]
        public string DataReader { get; set; } = string.Empty;
    }
}
