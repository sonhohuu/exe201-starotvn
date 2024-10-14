using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PayOs
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
    }

    public class ResultModel
    {
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
