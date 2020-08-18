using Microsoft.Extensions.DependencyInjection;
using MonkeyTyper.Core.Plugins;
using MonkeyTyper.WinForms.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonkeyTyper.Core.Extensions;
using IDataReader = MonkeyTyper.Core.Data.IDataReader;
using System.IO;
using MonkeyTyper.WinForms.Controls;
using MonkeyTyper.Core.Mail;

namespace MonkeyTyper.WinForms.Forms
{
    /// <summary>
    /// Entry form.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Var
        private AppSettings AppSettings { get; }

        private AboutForm About { get; }

        private SettingsForm Settings { get; }

        private MailingForm Mailing { get; }
        
        private IServiceProvider ServiceProvider { get; }

        private IProjectProperties Properties { get; }

        private ISettingsProvider SettingsProvider { get; }

        private Type[] DataReaders { get; }
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// <param name="serviceProvider">
        /// Service provider.
        /// </param>
        /// <param name="properties">
        /// An object that provides access to project properties.
        /// </param>
        /// <param name="settings">
        /// Settings provider.
        /// </param>
        public MainForm(IServiceProvider serviceProvider, IProjectProperties properties, ISettingsProvider settings)
        {
            InitializeComponent();
            InitializeNetCoreComponent();
            settings.Restore();
            ServiceProvider = serviceProvider;
            Properties = properties;
            SettingsProvider = settings;
            About = serviceProvider.GetRequiredService<AboutForm>();
            Settings = serviceProvider.GetRequiredService<SettingsForm>();
            Mailing = serviceProvider.GetRequiredService<MailingForm>();
            AppSettings = serviceProvider.GetRequiredService<AppSettings>();
            DataReaders = InitializeDataReaders(serviceProvider, dataReader);
            CopyValuesFromAppSettingsToFields();
        }

        private static Type[] InitializeDataReaders(IServiceProvider provider, ComboBox comboBox)
        {
            List<Type> types = new List<Type>();
            comboBox.Items.Clear();
            using IServiceScope scope = provider.CreateScope();
            foreach (IDataReader reader in scope.ServiceProvider.GetServices<IDataReader>())
            {
                Type type = reader.GetType();
                types.Add(type);
                comboBox.Items.Add(type.Name);
                reader.Dispose();
            }
            return types.ToArray();
        }
        #endregion

        #region Functions
        private void CopyValuesFromAppSettingsToFields()
        {
            senderAddress.Text = AppSettings.Address;
            senderPassword.Text = AppSettings.Password;
            senderName.Text = AppSettings.SenderName;
            smtpHost.Text = AppSettings.Host;
            smtpPort.Text = AppSettings.Port.ToString();
            smtpSSL.Checked = AppSettings.UseSSL;

            Guid.TryParse(AppSettings.DataReader, out Guid guid);
            for (int i = 0; i < DataReaders.Length; ++i)
            {
                Type type = DataReaders[i];
                if (type.GUID == guid
                    || type.Name.Equals(AppSettings.DataReader, StringComparison.InvariantCultureIgnoreCase)
                    || type.FullName!.Equals(AppSettings.DataReader, StringComparison.InvariantCultureIgnoreCase)
                    || type.AssemblyQualifiedName!.Equals(AppSettings.DataReader, StringComparison.InvariantCultureIgnoreCase))
                {
                    dataReader.SelectedIndex = i;
                }
            }
        }

        private void CopyValuesFromFieldsToAppSettings()
        {
            AppSettings.Port = int.Parse(smtpPort.Text);
            AppSettings.Address = senderAddress.Text;
            AppSettings.Password = senderPassword.Text;
            AppSettings.SenderName = senderName.Text;
            AppSettings.Host = smtpHost.Text;
            AppSettings.UseSSL = smtpSSL.Checked;
            AppSettings.DataReader = dataReader.SelectedIndex < 0 || dataReader.SelectedIndex >= DataReaders.Length ?
                string.Empty :
                DataReaders[dataReader.SelectedIndex].AssemblyQualifiedName!;
        }

        private void AddAttachment(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return;

            int lastRight = attachmentsPanel.Controls.OfType<FileBox>().OrderByDescending(x => x.Left).FirstOrDefault()?.Right ?? 2;
            attachmentsLabel.Visible = false;
            attachmentsPanel.AutoScrollPosition = new Point(0, 0);
            FileBox fileBox = new FileBox
            {
                Path = path,
                Font = attachmentsLabel.Font,
                AutoSize = true,
                IconSize = new Size(32, 32),
                Location = new Point(lastRight + 3, 5),
                ContextMenuStrip = attachmentContextMenu
            };
            fileBox.MouseClick += Attachments_MouseClick;
            attachmentsPanel.Controls.Add(fileBox);
        }

        private void DeleteAttachment(FileBox fileBox)
        {
            attachmentsPanel.Controls.Remove(fileBox);
            FileBox? prev = null;
            attachmentsPanel.AutoScrollPosition = new Point(0, 0);
            for (int i = 0; i < attachmentsPanel.Controls.Count; ++i)
            {
                if (!(attachmentsPanel.Controls[i] is FileBox current))
                    continue;

                current.Left = (prev?.Right ?? 2) + 3;
                prev = current;
            }
            if (attachmentsPanel.Controls.Count == 1)
                attachmentsLabel.Visible = true;
        }
        #endregion

        #region Handlers
        private void Settings_Click(object sender, EventArgs e)
        {
            if (Settings.ShowDialog() == DialogResult.OK)
                SettingsProvider.Store();
            else
                SettingsProvider.Restore();
        }

        private void VisitWebsite_Click(object sender, EventArgs e) => Process.Start(Properties.ProjectUrl);

        private void SendReview_Click(object sender, EventArgs e) => Process.Start(Properties.HelpUrl);

        private void ReportProblem_Click(object sender, EventArgs e) => Process.Start(Properties.HelpUrl);

        private void About_Click(object sender, EventArgs e) => About.ShowDialog();

        private void SaveAsDefaults_Click(object sender, EventArgs e)
        {
            CopyValuesFromFieldsToAppSettings();
            SettingsProvider.Store();
        }

        private void RestoreDefaults_Click(object sender, EventArgs e) => CopyValuesFromAppSettingsToFields();

        private void SmtpPort_Validating(object sender, CancelEventArgs e)
        {
            if (!int.TryParse(smtpPort.Text, out _))
            {
                MessageBox.Show(this, "SMTP port can only be a number.", "Invalid data provided", MessageBoxButtons.OK, MessageBoxIcon.Error);
                smtpPort.Text = AppSettings.Port.ToString();
            }
        }

        private void TextBox_DragEnter(object sender, DragEventArgs e) => e.Effect = 
            e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.UnicodeText) || e.Data.GetDataPresent(DataFormats.Text)
                ? DragDropEffects.Copy
                : DragDropEffects.None;

        private void TextBox_DragDrop(object sender, DragEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string? text = null;
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    text = string.Join(Environment.NewLine, ((string[])e.Data.GetData(DataFormats.FileDrop)).Select(File.ReadAllText));
                else if (e.Data.GetDataPresent(DataFormats.UnicodeText))
                    text = e.Data.GetData(DataFormats.UnicodeText).ToString();
                else if (e.Data.GetDataPresent(DataFormats.Text))
                    text = e.Data.GetData(DataFormats.Text).ToString();
                e.Effect = DragDropEffects.None;

                if (!string.IsNullOrEmpty(text))
                    textBox.Text = text;
            }
        }

        private void Attachments_DragEnter(object sender, DragEventArgs e) => e.Effect = 
            e.Data.GetDataPresent(DataFormats.FileDrop) ?
                DragDropEffects.Copy :
                DragDropEffects.None;

        private void Attachments_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                foreach (string file in (string[])e.Data.GetData(DataFormats.FileDrop))
                    AddAttachment(file);

            e.Effect = DragDropEffects.None;
        }

        private void Attachments_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (attachmentDialog.ShowDialog() == DialogResult.OK)
                AddAttachment(attachmentDialog.FileName);
        }

        private void DeleteAttachment_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem && menuItem.GetCurrentParent() is ContextMenuStrip { SourceControl: FileBox fileBox })
                DeleteAttachment(fileBox);
        }

        private void DuplicateAttachment_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem && menuItem.GetCurrentParent() is ContextMenuStrip { SourceControl: FileBox fileBox })
                AddAttachment(fileBox.Path);
        }

        /// <TODO>
        /// Split this method.
        /// </TODO>
        private async void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Controls.OfType<TextBox>().Any(x => string.IsNullOrWhiteSpace(x.Text)))
                    throw new InvalidOperationException("Please, fill in all text fields.");

                if (dataReader.SelectedIndex < 0 || dataReader.SelectedIndex >= DataReaders.Length)
                    throw new InvalidOperationException("Please, select a data reader.");

                using IServiceScope scope = ServiceProvider.CreateScope();
                IServiceProvider provider = scope.ServiceProvider;
                await using IMailClient mailClient = provider.GetRequiredService<IMailClient>();
                await using IDataReader reader = provider.GetServices<IDataReader>().FirstOrDefault(DataReaders[dataReader.SelectedIndex].IsInstanceOfType) ?? throw new MissingMemberException("Selected DataReader is missing.");
                IMessageMutator[] mutators = provider.GetServices<IMessageMutator>().ToArray();
                IMessageBuilder messageBuilder = provider.GetRequiredService<IMessageBuilder>();

                await mailClient.AuthenticateAsync(smtpHost.Text, int.Parse(smtpPort.Text), smtpSSL.Checked, senderAddress.Text, senderPassword.Text);

                messageBuilder.From.Add(senderName.Text, senderAddress.Text);
                messageBuilder.To.Add(recipientAddress.Text);
                messageBuilder.Subject = messageSubject.Text;
                if (enableHTML.Checked)
                    messageBuilder.HtmlBody = messageText.Text;
                else
                    messageBuilder.TextBody = messageText.Text;
                foreach (string path in attachmentsPanel.Controls.OfType<FileBox>().Select(x => x.Path))
                    messageBuilder.Attachments.Add(path);

                Mailing.ResetControlEvents();
                Mailing.SetMessageProgress(0, 100);
                Mailing.SetTotalProgress(0, 100);

                void pauseEvent(object? sender, BeforeMessageSentEventArgs e)
                {
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show(Mailing, "The process of sending messages has been paused.\n\nTo continue, click on the OK button.", "The process is paused.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mailClient.BeforeMessageSent -= pauseEvent;
                    }));
                }
                Mailing.CancellationRequest += (s, e) => mailClient.BeforeMessageSent += (ms, me) => me.Stop();
                Mailing.PauseRequest += (s, e) => mailClient.BeforeMessageSent += pauseEvent;

                mailClient.MessageTransferProgress += (s, e) =>
                {
                    string to = e.Message.To.Count > 0 ? e.Message.To[0].ToString() : string.Empty;
                    string bytes = e.TotalSize > 0 ? 
                        $"{Math.Round((double)e.BytesTransferred / e.TotalSize * 100d, 2)}% ({e.BytesTransferred} bytes/{e.TotalSize} bytes)" :
                        $"{e.BytesTransferred} bytes";

                    Invoke(new Action(() => Mailing.SetMessageProgress(e.BytesTransferred, e.TotalSize, string.IsNullOrEmpty(to) ? bytes : $"{to} - {bytes}")));
                };
                mailClient.MessageProcessed += (s, e) =>
                {
                    int ordinal = e.Ordinal + 1;
                    string total = e.Count > 0 ?
                        $"{Math.Round((double)ordinal / e.Count * 100d, 2)}% ({ordinal}/{e.Count})" :
                        $"{e.Ordinal}";

                    Invoke(new Action(() => Mailing.SetTotalProgress(ordinal, e.Count, total)));
                };
                mailClient.MessageTransferError += (s, e) => Invoke(new Action(() =>
                {
                    switch (MessageBox.Show(Mailing, $"Do you want to try sending this message again?\nTo skip just this message, press No. To cancel the whole mailing, click Cancel.\n\nException text:\n{e.Exception}", "An error occurred while trying to send the message.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error))
                    {
                        case DialogResult.Yes:
                            e.Retry();
                            break;
                        case DialogResult.No:
                            e.Skip();
                            break;
                        default:
                            e.Stop();
                            break;
                    }
                }));
                mailClient.MessageMutationError += (s, e) => Invoke(new Action(() =>
                {
                    switch (MessageBox.Show(Mailing, $"Do you want to skip this mutation?\nTo skip just this message, press No. To cancel the whole mailing, click Cancel.\n\nException text:\n{e.Exception}", "An error occurred while trying to mutate the message.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error))
                    {
                        case DialogResult.Yes:
                            e.SkipMutation();
                            break;
                        case DialogResult.No:
                            e.Skip();
                            break;
                        default:
                            e.Stop();
                            break;
                    }
                }));

                _ = Task.Run(() => Invoke(new Action(() => Mailing.ShowDialog(this))));
                await mailClient.BulkSendAllAsync(messageBuilder, reader, mutators);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "An error occurred while trying to send messages.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Mailing.Close();
        }
        #endregion
    }
}
