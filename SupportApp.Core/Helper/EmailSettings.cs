using System;
using System.Collections.Generic;
using System.Text;

namespace SupportApp.Core.Helper
{
    public class EmailSettings
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
