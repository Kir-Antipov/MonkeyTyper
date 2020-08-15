using System;
using System.Drawing;
using System.Windows.Forms;

namespace MonkeyTyper.WinForms.Helpers
{
    internal static class RichTextBoxExtensions
    {
        public static RichTextBox Append(this RichTextBox box, string text)
        {
            box.AppendText(text);
            return box;
        }

        public static RichTextBox AppendLine(this RichTextBox box, string text)
        {
            box.AppendText(text);
            box.AppendText(Environment.NewLine);
            return box;
        }

        public static RichTextBox AppendLine(this RichTextBox box) => box.AppendLine(string.Empty);

        public static RichTextBox Append(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            return box;
        }

        public static RichTextBox AppendLine(this RichTextBox box, string text, Color color)
        {
            box.Append(text, color);
            box.AppendText(Environment.NewLine);
            box.SelectionColor = box.ForeColor;
            return box;
        }
    }
}
