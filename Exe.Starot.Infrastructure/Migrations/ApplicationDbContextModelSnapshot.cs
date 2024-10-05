﻿// <auto-generated />
using System;
using Exe.Starot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Exe.Starot.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.AchievementEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDay")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.BookingEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("LinkUrl")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<string>("ReaderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PackageId");

                    b.HasIndex("ReaderId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.CustomerEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Membership")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            ID = "customer1",
                            CreatedDate = new DateTime(2024, 10, 4, 22, 15, 23, 288, DateTimeKind.Utc).AddTicks(8883),
                            LastUpdated = new DateTime(2024, 10, 4, 22, 15, 23, 288, DateTimeKind.Utc).AddTicks(8883),
                            Membership = 0,
                            UserId = "user1"
                        });
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.FavoriteProductEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFavorite")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("ID");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteProducts");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.FeedbackEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ReaderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ReaderId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.OrderDetailEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.PackageQuestionEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Time")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDay")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("PackageQuestions");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.PaymentEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("OrderID")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentGatewayID")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.HasIndex("PaymentGatewayID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.PaymentGatewayEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDay")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("PaymentGateways");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.ProductEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.ReaderEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Experience")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExperienceYears")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Introduction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("LinkUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quote")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(4,1)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("Readers");

                    b.HasData(
                        new
                        {
                            ID = "reader1",
                            CreatedDate = new DateTime(2024, 10, 4, 22, 15, 23, 288, DateTimeKind.Utc).AddTicks(9013),
                            Experience = "Experienced in tarot reading, astrology, and palmistry.",
                            ExperienceYears = 5,
                            Image = "default_image.png",
                            Introduction = "I am a seasoned reader with years of experience.",
                            LastUpdated = new DateTime(2024, 10, 4, 22, 15, 23, 288, DateTimeKind.Utc).AddTicks(9013),
                            LinkUrl = "http://johndoe.com",
                            Quote = "I foresee great things!",
                            Rating = 4.8m,
                            UserId = "user2"
                        });
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.TarotCardEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDay")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("TarotCards");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.UserAchievementEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("AchievedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("AchievementId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("ID");

                    b.HasIndex("AchievementId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAchievements");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.UserEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RefreshTokenIssuedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = "user1",
                            CreatedDate = new DateTime(2024, 10, 4, 22, 15, 23, 1, DateTimeKind.Utc).AddTicks(8416),
                            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "customer@gmail.com",
                            FirstName = "John",
                            Image = "",
                            LastName = "Doe",
                            LastUpdated = new DateTime(2024, 10, 4, 22, 15, 23, 1, DateTimeKind.Utc).AddTicks(8416),
                            PasswordHash = "$2a$11$8eJ7nAWAKLwGKBGSUN.X2e1PnfT8OFLkC5y7tWzWE35gvDFXyNmBa",
                            Phone = "123456789",
                            RefreshTokenExpiryTime = new DateTime(2024, 11, 3, 22, 15, 23, 142, DateTimeKind.Utc).AddTicks(8140),
                            RefreshTokenIssuedAt = new DateTime(2024, 10, 4, 22, 15, 23, 142, DateTimeKind.Utc).AddTicks(8149),
                            Role = "Customer"
                        },
                        new
                        {
                            ID = "user2",
                            CreatedDate = new DateTime(2024, 10, 4, 22, 15, 23, 142, DateTimeKind.Utc).AddTicks(8260),
                            DateOfBirth = new DateTime(1992, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "reader@gmail.com",
                            FirstName = "Jane",
                            Image = "",
                            LastName = "Smith",
                            LastUpdated = new DateTime(2024, 10, 4, 22, 15, 23, 142, DateTimeKind.Utc).AddTicks(8260),
                            PasswordHash = "$2a$11$HeoRwsQ9pSDzTJnYP0C5KOuVeX7LItbggeS0fwVhNlxiCV8M8oP.y",
                            Phone = "987654321",
                            RefreshTokenExpiryTime = new DateTime(2024, 11, 3, 22, 15, 23, 288, DateTimeKind.Utc).AddTicks(8140),
                            RefreshTokenIssuedAt = new DateTime(2024, 10, 4, 22, 15, 23, 288, DateTimeKind.Utc).AddTicks(8147),
                            Role = "Reader"
                        });
                });

            modelBuilder.Entity("OrderEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.BookingEntity", b =>
                {
                    b.HasOne("Exe.Starot.Domain.Entities.Base.CustomerEntity", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exe.Starot.Domain.Entities.Base.PackageQuestionEntity", "Package")
                        .WithMany("Bookings")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exe.Starot.Domain.Entities.Base.ReaderEntity", "Reader")
                        .WithMany("Bookings")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Package");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.CustomerEntity", b =>
                {
                    b.HasOne("Exe.Starot.Domain.Entities.Base.UserEntity", "User")
                        .WithMany("Customers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.FavoriteProductEntity", b =>
                {
                    b.HasOne("Exe.Starot.Domain.Entities.Base.ProductEntity", "Product")
                        .WithMany("FavoriteProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exe.Starot.Domain.Entities.Base.UserEntity", "User")
                        .WithMany("FavoriteProducts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.FeedbackEntity", b =>
                {
                    b.HasOne("Exe.Starot.Domain.Entities.Base.CustomerEntity", "Customer")
                        .WithMany("Feedbacks")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exe.Starot.Domain.Entities.Base.ReaderEntity", "Reader")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.OrderDetailEntity", b =>
                {
                    b.HasOne("OrderEntity", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exe.Starot.Domain.Entities.Base.ProductEntity", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.PaymentEntity", b =>
                {
                    b.HasOne("OrderEntity", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exe.Starot.Domain.Entities.Base.PaymentGatewayEntity", "PaymentGateway")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentGatewayID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("PaymentGateway");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.ReaderEntity", b =>
                {
                    b.HasOne("Exe.Starot.Domain.Entities.Base.UserEntity", "User")
                        .WithMany("Readers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.UserAchievementEntity", b =>
                {
                    b.HasOne("Exe.Starot.Domain.Entities.Base.AchievementEntity", "Achievement")
                        .WithMany("UserAchievements")
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exe.Starot.Domain.Entities.Base.UserEntity", "User")
                        .WithMany("UserAchievements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Achievement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderEntity", b =>
                {
                    b.HasOne("Exe.Starot.Domain.Entities.Base.UserEntity", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.AchievementEntity", b =>
                {
                    b.Navigation("UserAchievements");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.CustomerEntity", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.PackageQuestionEntity", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.PaymentGatewayEntity", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.ProductEntity", b =>
                {
                    b.Navigation("FavoriteProducts");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.ReaderEntity", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("Exe.Starot.Domain.Entities.Base.UserEntity", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("FavoriteProducts");

                    b.Navigation("Orders");

                    b.Navigation("Readers");

                    b.Navigation("UserAchievements");
                });

            modelBuilder.Entity("OrderEntity", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
