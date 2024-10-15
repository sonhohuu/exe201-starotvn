using Exe.Starot.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderEntity : Entity
{
    public string Code { get; set; }
    public string UserId { get; set; }
    public string? Status { get; set; } // Pending, Success, Failed
    public string? Address { get; set; } 
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }
    public string? OrderDate { get; set; }
    public string? OrderTime { get; set; }
    public string? PaymentMethod { get; set; } // Wallet, Cash
    public virtual UserEntity User { get; set; }
    public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }
}
