using System;
using System.Windows.Forms;

namespace MonkeyTyper.WinForms.Forms
{
    /// <summary>
    /// Proxy form providing information
    /// about the mailing progress.
    /// </summary>
    public partial class MailingForm : Form
    {
        #region Var
        /// <summary>
        /// Occurs when a process is requested to pause.
        /// </summary>
        public event EventHandler? PauseRequest;

        /// <summary>
        /// Occurs when a process is requested to be canceled.
        /// </summary>
        public event EventHandler? CancellationRequest;
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="MailingForm"/> class.
        /// </summary>
        public MailingForm() => InitializeComponent();
        #endregion

        #region Functions
        /// <summary>
        /// Removes all handlers from control events.
        /// </summary>
        public virtual void ResetControlEvents()
        {
            PauseRequest = null;
            CancellationRequest = null;
        }

        /// <summary>
        /// Informs about the progress of message sending.
        /// </summary>
        /// <param name="bytesTransferred">The number of bytes transferred.</param>
        /// <param name="totalSize">The total size, in bytes, of the message being transferred.</param>
        /// <param name="tip">Description of the current process.</param>
        public virtual void SetMessageProgress(long bytesTransferred, long totalSize, string? tip = null) => 
            SetProgress(messageProgressLabel, messageSendingProgress, bytesTransferred, totalSize, tip);

        /// <summary>
        /// Informs about the overall progress of the process.
        /// </summary>
        /// <param name="emailIndex">One-base ordinal number of the sent message.</param>
        /// <param name="totalEmails">The total number of messages to send.</param>
        /// <param name="tip">Description of the current process.</param>
        public virtual void SetTotalProgress(int emailIndex, int totalEmails, string? tip = null) => 
            SetProgress(totalProgressLabel, totalProgress, emailIndex, totalEmails, tip);

        private void SetProgress(Label label, ProgressBar progress, long valueLong, long totalLong, string? tip)
        {
            int value = (int)(valueLong > int.MaxValue ? int.MaxValue : valueLong);
            int total = (int)(totalLong > int.MaxValue ? int.MaxValue : totalLong);
            ProgressBarStyle style = ProgressBarStyle.Blocks;
            if (total < 0)
            {
                total = int.MaxValue;
                style = ProgressBarStyle.Marquee;
            }

            label.Text = $"{label.Text.Substring(0, label.Text.IndexOf(':') + 1)} {tip ?? string.Empty}";
                
            if ((progress.Maximum, progress.Style) != (total, style))
                (progress.Maximum, progress.Style) = (total, style);
            progress.Value = Math.Min(progress.Maximum, value);
        }
        #endregion

        #region Handlers
        private void Pause_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to pause the process?", "Confirmation required.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                PauseRequest?.Invoke(this, EventArgs.Empty);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to stop the process? This operation cannot be undone.", "Confirmation required.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                CancellationRequest?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
