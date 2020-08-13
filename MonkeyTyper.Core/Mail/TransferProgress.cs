using MailKit;
using System;

namespace MonkeyTyper.Core.Mail
{
    internal class TransferProgress : ITransferProgress
    {
        private readonly Action<long, long> ReportImpl;

        public TransferProgress(Action<long, long> report) => ReportImpl = report ?? throw new ArgumentNullException(nameof(report));

        public void Report(long bytesTransferred, long totalSize) => ReportImpl(bytesTransferred, totalSize);

        public void Report(long bytesTransferred) => ReportImpl(bytesTransferred, -1);
    }
}
