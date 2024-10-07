using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exe.Starot.Domain.Entities.Base
{
    public class PaymentEntity
    {
        [Key]
        public required string ID { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public required decimal Amount { get; set; }
        //public required string TransactionId { get; set; }
        public required string Token { get; set; }
        public required DateTime PaymentDate { get; set; }
        public required string OrderID { get; set; }
        [ForeignKey(nameof(OrderID))]
        public virtual OrderEntity Order { get; set; }
        public required int PaymentGatewayID { get; set; }
        [ForeignKey(nameof(PaymentGatewayID))]
        public virtual PaymentGatewayEntity PaymentGateway { get; set; }

    }
}
