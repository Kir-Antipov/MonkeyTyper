using MonkeyTyper.WinForms.Helpers;
using MonkeyTyper.WinForms.Data;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MonkeyTyper.WinForms.Forms
{
    /// <summary>
    /// Program's about window.
    /// </summary>
    public partial class AboutForm : Form
    {
        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="AboutForm"/> class.
        /// </summary>
        /// <param name="properties">
        /// An object that provides access to project properties.
        /// </param>
        public AboutForm(IProjectProperties properties)
        {
            InitializeComponent();

            infoText
                .Append(properties.Product, Color.FromArgb(232, 178, 103))
                .AppendLine($", {(properties.Version.StartsWith("v", StringComparison.InvariantCultureIgnoreCase) ? string.Empty : "v")}{properties.Version}")
                .AppendLine()
                .AppendLine(properties.Copyright)
                .AppendLine("All Rights Reserved.")
                .AppendLine()
                .AppendLine("Visit our website to learn more about the product:")
                .AppendLine(properties.ProjectUrl)
                .AppendLine()
                .AppendLine("If you need help, visit this link:")
                .AppendLine(properties.HelpUrl);
        }
        #endregion

        #region Handlers
        private void InfoText_Focus(object sender, EventArgs e) => logoPicture.Focus();

        private void InfoText_LinkClicked(object sender, LinkClickedEventArgs e) => Process.Start(e.LinkText);
        #endregion
    }
}
