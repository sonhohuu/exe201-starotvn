using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exe.Starot.Domain.Entities.Base
{
    public class TransactionEntity
    {
        [Key]
        public string TransactionId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public int Type {  get; set; }
        public int Status { get; set; } 
        public string UserId { get; set; }
        public DateTime TransactionDate { get; set; }
        public TimeSpan CreatTime { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
