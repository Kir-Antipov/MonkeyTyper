namespace MonkeyTyper.WinForms.Forms
{
    partial class MailingForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailingForm));
            this.messageProgressLabel = new System.Windows.Forms.Label();
            this.messageSendingProgress = new System.Windows.Forms.ProgressBar();
            this.totalProgressLabel = new System.Windows.Forms.Label();
            this.totalProgress = new System.Windows.Forms.ProgressBar();
            this.pauseButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // messageProgressLabel
            // 
            this.messageProgressLabel.AutoSize = true;
            this.messageProgressLabel.Location = new System.Drawing.Point(12, 9);
            this.messageProgressLabel.Name = "messageProgressLabel";
            this.messageProgressLabel.Size = new System.Drawing.Size(187, 20);
            this.messageProgressLabel.TabIndex = 0;
            this.messageProgressLabel.Text = "Message sending progress:";
            // 
            // messageSendingProgress
            // 
            this.messageSendingProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageSendingProgress.Location = new System.Drawing.Point(16, 32);
            this.messageSendingProgress.Name = "messageSendingProgress";
            this.messageSendingProgress.Size = new System.Drawing.Size(604, 23);
            this.messageSendingProgress.Step = 1;
            this.messageSendingProgress.TabIndex = 1;
            // 
            // totalProgressLabel
            // 
            this.totalProgressLabel.AutoSize = true;
            this.totalProgressLabel.Location = new System.Drawing.Point(16, 73);
            this.totalProgressLabel.Name = "totalProgressLabel";
            this.totalProgressLabel.Size = new System.Drawing.Size(106, 20);
            this.totalProgressLabel.TabIndex = 2;
            this.totalProgressLabel.Text = "Total progress:";
            // 
            // totalProgress
            // 
            this.totalProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalProgress.Location = new System.Drawing.Point(16, 96);
            this.totalProgress.Name = "totalProgress";
            this.totalProgress.Size = new System.Drawing.Size(604, 23);
            this.totalProgress.Step = 1;
            this.totalProgress.TabIndex = 3;
            // 
            // pauseButton
            // 
            this.pauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pauseButton.AutoSize = true;
            this.pauseButton.Location = new System.Drawing.Point(364, 125);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(125, 30);
            this.pauseButton.TabIndex = 4;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.Pause_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.AutoSize = true;
            this.cancelButton.Location = new System.Drawing.Point(495, 125);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(125, 30);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // MailingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(632, 165);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.totalProgress);
            this.Controls.Add(this.totalProgressLabel);
            this.Controls.Add(this.messageSendingProgress);
            this.Controls.Add(this.messageProgressLabel);
            this.Controls.Add(this.pauseButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MailingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sending messages...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label messageProgressLabel;
        private System.Windows.Forms.ProgressBar messageSendingProgress;
        private System.Windows.Forms.Label totalProgressLabel;
        private System.Windows.Forms.ProgressBar totalProgress;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button cancelButton;
    }
}