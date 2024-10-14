using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Exe.Starot.Domain.Entities.Base
{
    public class UserEntity : Entity
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime RefreshTokenIssuedAt { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }
        public decimal Balance { get; set; } = 0;
        public string Phone { get; set; } = string.Empty;
        public string? DateOfBirth { get; set; }
        public string? Image { get; set; } = string.Empty;
        public string? Gender {  get; set; } = string.Empty;

        public virtual ICollection<OrderEntity> Orders { get; set; }
        public virtual ICollection<FavoriteProductEntity> FavoriteProducts { get; set; }
        public virtual ICollection<CustomerEntity> Customers { get; set; }
        public virtual ICollection<ReaderEntity> Readers { get; set; }
        public virtual ICollection<UserAchievementEntity> UserAchievements { get; set; }
        public virtual ICollection<TransactionEntity> Transactions { get; set; }
        public void SetRefreshToken(string token, DateTime expiry)
        {
            RefreshToken = token;
            RefreshTokenExpiryTime = expiry;
            RefreshTokenIssuedAt = DateTime.UtcNow;
        }

        public bool IsRefreshTokenValid(string token)
        {
            return RefreshToken == token && RefreshTokenExpiryTime > DateTime.UtcNow;
        }
    }
}
