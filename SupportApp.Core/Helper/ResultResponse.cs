using System;
using System.Collections.Generic;
using System.Text;

namespace SupportApp.Core.Helper
{
    public class ResultResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public Exception Error { get; set; }
    }
}
