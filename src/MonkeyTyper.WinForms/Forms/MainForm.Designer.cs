namespace MonkeyTyper.WinForms.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method fixes some of the differences
        /// between .NET Framework and .NET Core.
        /// </summary>
        [System.Diagnostics.Conditional("NETCOREAPP")]
        private void InitializeNetCoreComponent()
        {
            attachmentsPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            attachmentsLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Resize += Attachments_Resize;
        }

        private void Attachments_Resize(object sender, System.EventArgs e)
        {
            attachmentsPanel.Size = System.Drawing.Size.Subtract(attachmentsBox.Size, new System.Drawing.Size(2, 19));
            attachmentsPanel.Location = new System.Drawing.Point(1, 17);
            attachmentsLabel.Left = (attachmentsBox.Width - attachmentsLabel.Width) / 2;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMessageFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visitWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendReviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportProblemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smtpSSL = new System.Windows.Forms.CheckBox();
            this.saveAsDefaults = new System.Windows.Forms.Button();
            this.restoreDefaults = new System.Windows.Forms.Button();
            this.dataReader = new System.Windows.Forms.ComboBox();
            this.dataReaderLabel = new System.Windows.Forms.Label();
            this.attachmentsBox = new System.Windows.Forms.GroupBox();
            this.attachmentsPanel = new System.Windows.Forms.Panel();
            this.attachmentsLabel = new System.Windows.Forms.Label();
            this.attachmentDialog = new System.Windows.Forms.OpenFileDialog();
            this.attachmentContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smtpPort = new MonkeyTyper.WinForms.Controls.PlaceholderTextBox();
            this.smtpHost = new MonkeyTyper.WinForms.Controls.PlaceholderTextBox();
            this.senderName = new MonkeyTyper.WinForms.Controls.PlaceholderTextBox();
            this.senderPassword = new MonkeyTyper.WinForms.Controls.PlaceholderTextBox();
            this.senderAddress = new MonkeyTyper.WinForms.Controls.PlaceholderTextBox();
            this.messageText = new MonkeyTyper.WinForms.Controls.PlaceholderTextBox();
            this.messageSubject = new MonkeyTyper.WinForms.Controls.PlaceholderTextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.enableHTML = new System.Windows.Forms.CheckBox();
            this.recipientAddress = new MonkeyTyper.WinForms.Controls.PlaceholderTextBox();
            this.openMessageFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip.SuspendLayout();
            this.attachmentsBox.SuspendLayout();
            this.attachmentsPanel.SuspendLayout();
            this.attachmentContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(782, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMessageFileToolStripMenuItem,
            this.exitSeparator,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openMessageFileToolStripMenuItem
            // 
            this.openMessageFileToolStripMenuItem.Name = "openMessageFileToolStripMenuItem";
            this.openMessageFileToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openMessageFileToolStripMenuItem.Text = "Open file...";
            this.openMessageFileToolStripMenuItem.Click += new System.EventHandler(this.OpenMessageFile_Click);
            // 
            // exitSeparator
            // 
            this.exitSeparator.Name = "exitSeparator";
            this.exitSeparator.Size = new System.Drawing.Size(221, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.Settings_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visitWebsiteToolStripMenuItem,
            this.sendReviewToolStripMenuItem,
            this.reportProblemToolStripMenuItem,
            this.aboutSeparator,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // visitWebsiteToolStripMenuItem
            // 
            this.visitWebsiteToolStripMenuItem.Name = "visitWebsiteToolStripMenuItem";
            this.visitWebsiteToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.visitWebsiteToolStripMenuItem.Text = "Visit the website";
            this.visitWebsiteToolStripMenuItem.Click += new System.EventHandler(this.VisitWebsite_Click);
            // 
            // sendReviewToolStripMenuItem
            // 
            this.sendReviewToolStripMenuItem.Name = "sendReviewToolStripMenuItem";
            this.sendReviewToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.sendReviewToolStripMenuItem.Text = "Send a review";
            this.sendReviewToolStripMenuItem.Click += new System.EventHandler(this.SendReview_Click);
            // 
            // reportProblemToolStripMenuItem
            // 
            this.reportProblemToolStripMenuItem.Name = "reportProblemToolStripMenuItem";
            this.reportProblemToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.reportProblemToolStripMenuItem.Text = "Report a problem";
            this.reportProblemToolStripMenuItem.Click += new System.EventHandler(this.ReportProblem_Click);
            // 
            // aboutSeparator
            // 
            this.aboutSeparator.Name = "aboutSeparator";
            this.aboutSeparator.Size = new System.Drawing.Size(207, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.About_Click);
            // 
            // smtpSSL
            // 
            this.smtpSSL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.smtpSSL.AutoSize = true;
            this.smtpSSL.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.smtpSSL.Location = new System.Drawing.Point(468, 163);
            this.smtpSSL.Name = "smtpSSL";
            this.smtpSSL.Size = new System.Drawing.Size(156, 24);
            this.smtpSSL.TabIndex = 8;
            this.smtpSSL.Text = "Use SMTP over SSL";
            this.smtpSSL.UseVisualStyleBackColor = true;
            // 
            // saveAsDefaults
            // 
            this.saveAsDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveAsDefaults.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveAsDefaults.Location = new System.Drawing.Point(468, 227);
            this.saveAsDefaults.Name = "saveAsDefaults";
            this.saveAsDefaults.Size = new System.Drawing.Size(148, 30);
            this.saveAsDefaults.TabIndex = 9;
            this.saveAsDefaults.Text = "Save as defaults";
            this.saveAsDefaults.UseVisualStyleBackColor = true;
            this.saveAsDefaults.Click += new System.EventHandler(this.SaveAsDefaults_Click);
            // 
            // restoreDefaults
            // 
            this.restoreDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.restoreDefaults.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.restoreDefaults.Location = new System.Drawing.Point(622, 227);
            this.restoreDefaults.Name = "restoreDefaults";
            this.restoreDefaults.Size = new System.Drawing.Size(148, 30);
            this.restoreDefaults.TabIndex = 10;
            this.restoreDefaults.Text = "Restore defaults";
            this.restoreDefaults.UseVisualStyleBackColor = true;
            this.restoreDefaults.Click += new System.EventHandler(this.RestoreDefaults_Click);
            // 
            // dataReader
            // 
            this.dataReader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataReader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataReader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataReader.FormattingEnabled = true;
            this.dataReader.Location = new System.Drawing.Point(468, 193);
            this.dataReader.Name = "dataReader";
            this.dataReader.Size = new System.Drawing.Size(200, 28);
            this.dataReader.TabIndex = 11;
            // 
            // dataReaderLabel
            // 
            this.dataReaderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataReaderLabel.AutoSize = true;
            this.dataReaderLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataReaderLabel.Location = new System.Drawing.Point(670, 196);
            this.dataReaderLabel.Name = "dataReaderLabel";
            this.dataReaderLabel.Size = new System.Drawing.Size(88, 20);
            this.dataReaderLabel.TabIndex = 12;
            this.dataReaderLabel.Text = "Data reader";
            // 
            // attachmentsBox
            // 
            this.attachmentsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attachmentsBox.Controls.Add(this.attachmentsPanel);
            this.attachmentsBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.attachmentsBox.Location = new System.Drawing.Point(12, 260);
            this.attachmentsBox.Name = "attachmentsBox";
            this.attachmentsBox.Size = new System.Drawing.Size(450, 85);
            this.attachmentsBox.TabIndex = 14;
            this.attachmentsBox.TabStop = false;
            this.attachmentsBox.Text = "Attachments";
            // 
            // attachmentsPanel
            // 
            this.attachmentsPanel.AllowDrop = true;
            this.attachmentsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attachmentsPanel.AutoScroll = true;
            this.attachmentsPanel.Controls.Add(this.attachmentsLabel);
            this.attachmentsPanel.Location = new System.Drawing.Point(1, 17);
            this.attachmentsPanel.Name = "attachmentsPanel";
            this.attachmentsPanel.Size = new System.Drawing.Size(448, 66);
            this.attachmentsPanel.TabIndex = 0;
            this.attachmentsPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.Attachments_DragDrop);
            this.attachmentsPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.Attachments_DragEnter);
            this.attachmentsPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Attachments_MouseClick);
            // 
            // attachmentsLabel
            // 
            this.attachmentsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attachmentsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.attachmentsLabel.Location = new System.Drawing.Point(22, 16);
            this.attachmentsLabel.Name = "attachmentsLabel";
            this.attachmentsLabel.Size = new System.Drawing.Size(404, 20);
            this.attachmentsLabel.TabIndex = 0;
            this.attachmentsLabel.Text = "🡓 Attach binaries by dropping them here or selecting them. ";
            this.attachmentsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.attachmentsLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Attachments_MouseClick);
            // 
            // attachmentDialog
            // 
            this.attachmentDialog.AddExtension = false;
            this.attachmentDialog.Title = "Select an attachment file";
            // 
            // attachmentContextMenu
            // 
            this.attachmentContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.attachmentContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.duplicateToolStripMenuItem});
            this.attachmentContextMenu.Name = "attachmentContextMenu";
            this.attachmentContextMenu.Size = new System.Drawing.Size(143, 52);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(142, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteAttachment_Click);
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(142, 24);
            this.duplicateToolStripMenuItem.Text = "Duplicate";
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.DuplicateAttachment_Click);
            // 
            // smtpPort
            // 
            this.smtpPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.smtpPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.smtpPort.Location = new System.Drawing.Point(674, 130);
            this.smtpPort.Name = "smtpPort";
            this.smtpPort.PlaceholderText = "SMTP port";
            this.smtpPort.Size = new System.Drawing.Size(96, 27);
            this.smtpPort.TabIndex = 7;
            this.smtpPort.Validating += new System.ComponentModel.CancelEventHandler(this.SmtpPort_Validating);
            // 
            // smtpHost
            // 
            this.smtpHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.smtpHost.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.smtpHost.Location = new System.Drawing.Point(468, 130);
            this.smtpHost.Name = "smtpHost";
            this.smtpHost.PlaceholderText = "SMTP host";
            this.smtpHost.Size = new System.Drawing.Size(200, 27);
            this.smtpHost.TabIndex = 6;
            // 
            // senderName
            // 
            this.senderName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.senderName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.senderName.Location = new System.Drawing.Point(468, 97);
            this.senderName.Name = "senderName";
            this.senderName.PlaceholderText = "Sender display name";
            this.senderName.Size = new System.Drawing.Size(302, 27);
            this.senderName.TabIndex = 5;
            // 
            // senderPassword
            // 
            this.senderPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.senderPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.senderPassword.Location = new System.Drawing.Point(468, 64);
            this.senderPassword.Name = "senderPassword";
            this.senderPassword.PasswordChar = '•';
            this.senderPassword.PlaceholderText = "Sender email password";
            this.senderPassword.Size = new System.Drawing.Size(302, 27);
            this.senderPassword.TabIndex = 4;
            // 
            // senderAddress
            // 
            this.senderAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.senderAddress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.senderAddress.Location = new System.Drawing.Point(468, 31);
            this.senderAddress.Name = "senderAddress";
            this.senderAddress.PlaceholderText = "Sender email address";
            this.senderAddress.Size = new System.Drawing.Size(302, 27);
            this.senderAddress.TabIndex = 3;
            // 
            // messageText
            // 
            this.messageText.AcceptsReturn = true;
            this.messageText.AllowDrop = true;
            this.messageText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messageText.Location = new System.Drawing.Point(12, 97);
            this.messageText.Multiline = true;
            this.messageText.Name = "messageText";
            this.messageText.PlaceholderText = "Message text";
            this.messageText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageText.Size = new System.Drawing.Size(450, 160);
            this.messageText.TabIndex = 2;
            this.messageText.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDrop);
            this.messageText.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBox_DragEnter);
            // 
            // messageSubject
            // 
            this.messageSubject.AllowDrop = true;
            this.messageSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageSubject.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messageSubject.Location = new System.Drawing.Point(12, 64);
            this.messageSubject.Name = "messageSubject";
            this.messageSubject.PlaceholderText = "Message subject";
            this.messageSubject.Size = new System.Drawing.Size(450, 27);
            this.messageSubject.TabIndex = 1;
            this.messageSubject.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDrop);
            this.messageSubject.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBox_DragEnter);
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sendButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.sendButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.sendButton.FlatAppearance.BorderSize = 2;
            this.sendButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(228)))), ((int)(((byte)(247)))));
            this.sendButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
            this.sendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sendButton.Location = new System.Drawing.Point(12, 351);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(150, 33);
            this.sendButton.TabIndex = 15;
            this.sendButton.Text = "Start mailing";
            this.sendButton.UseVisualStyleBackColor = false;
            this.sendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // enableHTML
            // 
            this.enableHTML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.enableHTML.AutoSize = true;
            this.enableHTML.Location = new System.Drawing.Point(288, 351);
            this.enableHTML.Name = "enableHTML";
            this.enableHTML.Size = new System.Drawing.Size(173, 24);
            this.enableHTML.TabIndex = 16;
            this.enableHTML.Text = "Enable HTML markup";
            this.enableHTML.UseVisualStyleBackColor = true;
            // 
            // recipientAddress
            // 
            this.recipientAddress.AllowDrop = true;
            this.recipientAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recipientAddress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.recipientAddress.Location = new System.Drawing.Point(12, 31);
            this.recipientAddress.Name = "recipientAddress";
            this.recipientAddress.PlaceholderText = "Recipient\'s address";
            this.recipientAddress.Size = new System.Drawing.Size(450, 27);
            this.recipientAddress.TabIndex = 17;
            // 
            // openMessageFileDialog
            // 
            this.openMessageFileDialog.AddExtension = false;
            this.openMessageFileDialog.Title = "Select message file...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(782, 396);
            this.Controls.Add(this.recipientAddress);
            this.Controls.Add(this.enableHTML);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.attachmentsBox);
            this.Controls.Add(this.dataReaderLabel);
            this.Controls.Add(this.dataReader);
            this.Controls.Add(this.restoreDefaults);
            this.Controls.Add(this.saveAsDefaults);
            this.Controls.Add(this.smtpSSL);
            this.Controls.Add(this.smtpPort);
            this.Controls.Add(this.smtpHost);
            this.Controls.Add(this.senderName);
            this.Controls.Add(this.senderPassword);
            this.Controls.Add(this.senderAddress);
            this.Controls.Add(this.messageText);
            this.Controls.Add(this.messageSubject);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MonkeyTyper";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.attachmentsBox.ResumeLayout(false);
            this.attachmentsPanel.ResumeLayout(false);
            this.attachmentContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visitWebsiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator aboutSeparator;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendReviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportProblemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private Controls.PlaceholderTextBox messageSubject;
        private Controls.PlaceholderTextBox messageText;
        private Controls.PlaceholderTextBox senderAddress;
        private Controls.PlaceholderTextBox senderPassword;
        private Controls.PlaceholderTextBox senderName;
        private Controls.PlaceholderTextBox smtpHost;
        private Controls.PlaceholderTextBox smtpPort;
        private System.Windows.Forms.CheckBox smtpSSL;
        private System.Windows.Forms.Button saveAsDefaults;
        private System.Windows.Forms.Button restoreDefaults;
        private System.Windows.Forms.ComboBox dataReader;
        private System.Windows.Forms.Label dataReaderLabel;
        private System.Windows.Forms.GroupBox attachmentsBox;
        private System.Windows.Forms.Panel attachmentsPanel;
        private System.Windows.Forms.Label attachmentsLabel;
        private System.Windows.Forms.OpenFileDialog attachmentDialog;
        private System.Windows.Forms.ContextMenuStrip attachmentContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.CheckBox enableHTML;
        private Controls.PlaceholderTextBox recipientAddress;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMessageFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator exitSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openMessageFileDialog;
    }
}