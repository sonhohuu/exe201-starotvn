using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PayOs
{
    public class PaymentRequest
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
    }

    public class WebhookType
    {
        public string Code { get; set; }
        public string OrderCode { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; }
    }
    public interface IPayOsServices
    {
        public string CreatePaymentLink(PaymentRequest model);
        public string ProcessPaymentResponse(WebhookType webhookBody);
    }

    

}
