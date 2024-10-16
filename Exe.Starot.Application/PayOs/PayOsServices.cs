using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Entities.Repositories;
using Net.payOS;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PayOs
{
    public class PayOsServices
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly PayOS _payOs;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public PayOsServices(ITransactionRepository transactionRepository, PayOS payOs, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _payOs = payOs;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }
        public async Task<string> CreatePaymentLink(PaymentRequest model)
        {
            if (_currentUserService.UserId == null)
            {
                throw new UnauthorizedAccessException("User not login yet");
            }

            string txnRef = GenerateTransactionId();
            long orderCode = long.Parse(txnRef.Substring(5));
            var transaction = new Domain.Entities.Base.TransactionEntity
            {
                TransactionId = txnRef,
                UserId = _currentUserService.UserId,
                Type = 2,                 // recharge 
                Amount = (int)model.Amount,
                Status = 2,               // Pending
                TransactionDate = DateTime.UtcNow,
                CreatTime = DateTime.UtcNow.TimeOfDay
            };
            _transactionRepository.Add(transaction);
            await _transactionRepository.UnitOfWork.SaveChangesAsync();

            long expiredAt = (long)(DateTime.UtcNow.AddMinutes(10) - new DateTime(1970, 1, 1)).TotalSeconds;

            ItemData item = new ItemData("Mì tôm hảo hảo ly", 1, 1000);
            List<ItemData> items = new List<ItemData>();

            PaymentData paymentData = new PaymentData(orderCode, (int)model.Amount, $"Desposit {model.Amount} in Wallet", items, "cancelUrl", "returnUrl");
            CreatePaymentResult createPaymentResult = await _payOs.createPaymentLink(paymentData);
            return createPaymentResult.checkoutUrl;
        }

        public async Task<ResultModel> ProcessPaymentResponse(WebhookType webhookBody)
        {

            WebhookData verifiedData = _payOs.verifyPaymentWebhookData(webhookBody); //xác thực data from webhook
            string responseCode = verifiedData.code;

            string orderCode = verifiedData.orderCode.ToString();

            string transactionId = "TRANS" + orderCode;

            var transaction = await _transactionRepository.FindAsync(x => x.TransactionId == transactionId);

            if (transaction != null && responseCode == "00")
            {
                transaction.Status = 1; // Success
                _transactionRepository.Update(transaction);
                await _transactionRepository.UnitOfWork.SaveChangesAsync();

                var existUser = await _userRepository.FindAsync(x => x.ID == transaction.UserId && !x.DeletedDay.HasValue);
                if (existUser != null)
                {
                    existUser.Balance += transaction.Amount;
                    _userRepository.Update(existUser);
                    await _userRepository.UnitOfWork.SaveChangesAsync();
                    return new ResultModel { IsSuccess = true, Code = 0, Message = "Payment success" };
                }
            }
            else
            {
                if (transaction != null)
                {
                    transaction.Status = 3; // Fail
                    _transactionRepository.Update(transaction);
                    await _transactionRepository.UnitOfWork.SaveChangesAsync();
                }
            }
            return new ResultModel { IsSuccess = false, Code = int.Parse(responseCode), Message = "Payment failed" };
        }

        private string GenerateTransactionId()
        {
            return "TRANS" + DateTimeOffset.Now.ToString("fff"); // You can improve this logic
        }
    }
}
