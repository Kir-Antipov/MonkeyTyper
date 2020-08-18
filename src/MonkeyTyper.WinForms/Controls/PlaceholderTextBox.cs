using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MonkeyTyper.WinForms.Controls
{
    /// <summary>
    /// <see cref="TextBox"/> extended with <see cref="PlaceholderText"/> property.
    /// </summary>
    public class PlaceholderTextBox : TextBox
    {
#if NETFRAMEWORK
        /// <summary>
        /// Gets or sets the text that is displayed when the
        /// control has no text and does not have the focus.
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets the text that is displayed when the control has no text and does not have the focus.")]
        public string PlaceholderText
        {
            get => _placeholderText;
            set
            {
                _placeholderText = value ?? throw new ArgumentNullException(nameof(value));
                Invalidate();
            }
        }
        private string _placeholderText = string.Empty;

        /// <inheritdoc/>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0xf && !Focused && string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(_placeholderText))
            {
                using Graphics graphics = CreateGraphics();
                TextRenderer.DrawText(graphics, _placeholderText, Font, ClientRectangle, SystemColors.GrayText, BackColor, TextFormatFlags.Top | TextFormatFlags.Left);
            }
        }
#endif
    }
}
