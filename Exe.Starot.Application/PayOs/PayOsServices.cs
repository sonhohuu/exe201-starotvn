using Exe.Starot.Application.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PayOs
{
    //public class PayOsServices : IPayOsServices
    //{
    //    public async Task<string> CreatePaymentLink(PaymentRequest model)
    //    {
    //        string txnRef = GenerateTransactionId();
    //        //long orderCode = long.Parse(txnRef.Substring(5));
    //        var transaction = new Repositories.Entities.Transaction
    //        {
    //            TransactionId = txnRef,
    //            UserId = model.UserId.ToUpper(),
    //            Type = 2,                 // recharge 
    //            Amount = (int)model.Amount,
    //            Status = 2,               // Pending
    //            CreatedDate = DateTime.Now,
    //            CreatTime = DateTime.Now.TimeOfDay
    //        };
    //        _transactionRepository.Add(transaction);
    //        _transactionRepository.SaveChanges();

    //        long expiredAt = (long)(DateTime.UtcNow.AddMinutes(10) - new DateTime(1970, 1, 1)).TotalSeconds;
    //        PaymentData paymentData = new PaymentData(
    //            orderCode: long.Parse(txnRef.Substring(5)),
    //            amount: (int)model.Amount,
    //            description: $"Deposit {model.Amount} into wallet",
    //            items: new List<ItemData>(),
    //            cancelUrl: "https://dev.fancy.io.vn/paymen-failed/",
    //            returnUrl: "https://dev.fancy.io.vn/payment-page/",
    //            expiredAt: expiredAt
    //        );

    //        CreatePaymentResult createPaymentResult = await _payOs.createPaymentLink(paymentData);
    //        return createPaymentResult.checkoutUrl;
    //    }

    //    public async Task<ResultModel> ProcessPaymentResponse(WebhookType webhookBody)
    //    {

    //        WebhookData verifiedData = _payOs.verifyPaymentWebhookData(webhookBody); //xác thực data from webhook
    //        string responseCode = verifiedData.code;

    //        string orderCode = verifiedData.orderCode.ToString();

    //        string transactionId = "TRANS" + orderCode;

    //        var transaction = _transactionRepository.GetByTransactionId(transactionId);

    //        if (transaction != null && responseCode == "00")
    //        {
    //            transaction.Status = 1; // Success
    //            _transactionRepository.Update(transaction);
    //            await _transactionRepository.SaveChangesAsync();

    //            var user = _userService.GetUserById(transaction.UserId);
    //            if (user != null)
    //            {
    //                var result = await _userService.UpdateUserBalance(transaction.UserId, transaction.Amount / 1000);
    //                result.Code = 0;
    //                return result;
    //            }
    //        }
    //        else
    //        {
    //            if (transaction != null)
    //            {
    //                transaction.Status = 3; // Faild
    //                _transactionRepository.Update(transaction);
    //                await _transactionRepository.SaveChangesAsync();
    //            }
    //        }
    //        return new ResultModel { IsSuccess = false, Code = int.Parse(responseCode), Message = "Payment failed" };
    //    }


    //}
}
