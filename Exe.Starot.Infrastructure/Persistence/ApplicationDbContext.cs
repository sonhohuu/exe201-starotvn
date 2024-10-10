﻿using Exe.Starot.Domain.Common.Interfaces;
using Exe.Starot.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Exe.Starot.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            var migrator = this.Database.GetService<IMigrator>();
            migrator.Migrate();
        }

        // Define DbSet properties for each entity/table
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderDetailEntity> OrderDetails { get; set; }
        public DbSet<FavoriteProductEntity> FavoriteProducts { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ReaderEntity> Readers { get; set; }
        public DbSet<FeedbackEntity> Feedbacks { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<UserAchievementEntity> UserAchievements { get; set; }
        public DbSet<AchievementEntity> Achievements { get; set; }
        public DbSet<PackageQuestionEntity> PackageQuestions { get; set; }
        public DbSet<TarotCardEntity> TarotCards { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<PaymentGatewayEntity> PaymentGateways { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            // Apply the Fluent API configurations
            ConfigureModel(modelBuilder);
            SeedData(modelBuilder);

        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Users
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    ID = "user1",  // Replace with actual ID structure
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "customer@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("string"),
                    Role = "Customer",
                    Phone = "123456789",
                    Gender = "Female",
                    DateOfBirth = "20/11/2020",
                    RefreshToken = null,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30),
                    RefreshTokenIssuedAt = DateTime.UtcNow
                },
                new UserEntity
                {
                    ID = "user2",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "reader@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("string"),
                    Role = "Reader",
                    Phone = "987654321",
                    Gender = "Male",
                    DateOfBirth = "20/11/2020",
                    RefreshToken = null,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30),
                    RefreshTokenIssuedAt = DateTime.UtcNow
                },
                new UserEntity
                {
                    ID = "admin",  // Replace with actual ID structure
                    FirstName = "Test",
                    LastName = "Admin",
                    Email = "admin@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("string"),
                    Role = "Admin",
                    Phone = "123456789",
                    Gender = "Female",
                    DateOfBirth = "20/11/2020",
                    RefreshToken = null,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30),
                    RefreshTokenIssuedAt = DateTime.UtcNow
                }
            );

            // Seed Customers
            modelBuilder.Entity<CustomerEntity>().HasData(
                new CustomerEntity
                {
                    ID = "customer1",  // Use appropriate ID structure
                    UserId = "user1",  // Linked to Jane Smith
                    Membership = 0 // Example membership level
                }
            );

            // Seed Readers
            modelBuilder.Entity<ReaderEntity>().HasData(
                new ReaderEntity
                {
                    ID = "reader1",  // Use appropriate ID structure
                    UserId = "user2",  // Linked to John Doe
                    ExperienceYears = 5,
                    Quote = "I foresee great things!",
                    Rating = 4.8M,
                    Image = "default_image.png",
                    LinkUrl = "http://johndoe.com",
                    Introduction = "I am a seasoned reader with years of experience.",
                    Experience = "Experienced in tarot reading, astrology, and palmistry."
                }
            );

        }

        private void ConfigureModel(ModelBuilder modelBuilder)
        {
            // Define string length constraints for each table
            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.Code).HasMaxLength(50);
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Content).HasMaxLength(1000);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Image).HasMaxLength(255);
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.Code).HasMaxLength(50);
                entity.Property(e => e.Status).HasMaxLength(50);
                entity.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderDetailEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.ProductId).HasMaxLength(36);
                entity.Property(e => e.OrderId).HasMaxLength(36);
            });

            modelBuilder.Entity<FavoriteProductEntity>(entity =>
            {
                entity.Property(e => e.ProductId).HasMaxLength(36);
                entity.Property(e => e.UserId).HasMaxLength(36);
            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.FirstName).HasMaxLength(255);
                entity.Property(e => e.LastName).HasMaxLength(255);
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.UserId).HasMaxLength(36);
            });

            modelBuilder.Entity<ReaderEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.UserId).HasMaxLength(36);
                entity.Property(e => e.Quote).HasMaxLength(1000);
                entity.Property(e => e.Image).HasMaxLength(255);
            });

            modelBuilder.Entity<FeedbackEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.Comment).HasMaxLength(1000);
            });

            modelBuilder.Entity<BookingEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
                entity.Property(e => e.LinkUrl).HasMaxLength(1000);
                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<UserAchievementEntity>(entity =>
            {
                entity.Property(e => e.ID).HasMaxLength(36);
            });

            modelBuilder.Entity<AchievementEntity>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<PackageQuestionEntity>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Time).HasMaxLength(50);
            });

            modelBuilder.Entity<TarotCardEntity>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Content).HasMaxLength(1000);
                entity.Property(e => e.Image).HasMaxLength(255);
                entity.Property(e => e.Type).HasMaxLength(50);
            });

            // Product and OrderDetail relationship (One-to-Many)
            modelBuilder.Entity<OrderDetailEntity>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);

            // Order and OrderDetail relationship (One-to-Many)
            modelBuilder.Entity<OrderDetailEntity>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            // User and FavoriteProduct relationship (One-to-Many)
            modelBuilder.Entity<FavoriteProductEntity>()
                .HasOne(fp => fp.User)
                .WithMany(u => u.FavoriteProducts)
                .HasForeignKey(fp => fp.UserId);

            // Product and FavoriteProduct relationship (One-to-Many)
            modelBuilder.Entity<FavoriteProductEntity>()
                .HasOne(fp => fp.Product)
                .WithMany(p => p.FavoriteProducts)
                .HasForeignKey(fp => fp.ProductId);

            // User and Order relationship (One-to-Many)
            modelBuilder.Entity<OrderEntity>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            // User and Customer relationship (One-to-One)
            modelBuilder.Entity<CustomerEntity>()
                .HasOne(c => c.User)
                .WithMany(u => u.Customers)
                .HasForeignKey(c => c.UserId);

            // User and Reader relationship (One-to-One)
            modelBuilder.Entity<ReaderEntity>()
                .HasOne(r => r.User)
                .WithMany(u => u.Readers)
                .HasForeignKey(r => r.UserId);

            // Customer and Feedback relationship (One-to-Many)
            modelBuilder.Entity<FeedbackEntity>()
                .HasOne(f => f.Customer)
                .WithMany(c => c.Feedbacks)
                .HasForeignKey(f => f.CustomerId);

            // Reader and Feedback relationship (One-to-Many)
            modelBuilder.Entity<FeedbackEntity>()
                .HasOne(f => f.Reader)
                .WithMany(r => r.Feedbacks)
                .HasForeignKey(f => f.ReaderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer and Booking relationship (One-to-Many)
            modelBuilder.Entity<BookingEntity>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId);

            // Reader and Booking relationship (One-to-Many)
            modelBuilder.Entity<BookingEntity>()
                .HasOne(b => b.Reader)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.ReaderId)
                .OnDelete(DeleteBehavior.Restrict);

            // PackageQuestion and Booking relationship (One-to-Many)
            modelBuilder.Entity<BookingEntity>()
                .HasOne(b => b.Package)
                .WithMany(pq => pq.Bookings)
                .HasForeignKey(b => b.PackageId);

            // User and UserAchievement relationship (One-to-Many)
            modelBuilder.Entity<UserAchievementEntity>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAchievements)
                .HasForeignKey(ua => ua.UserId);

            // Achievement and UserAchievement relationship (One-to-Many)
            modelBuilder.Entity<UserAchievementEntity>()
                .HasOne(ua => ua.Achievement)
                .WithMany(a => a.UserAchievements)
                .HasForeignKey(ua => ua.AchievementId);

            // Additional Fluent API configurations can be added here, such as indexes or constraints
        }
    }
}