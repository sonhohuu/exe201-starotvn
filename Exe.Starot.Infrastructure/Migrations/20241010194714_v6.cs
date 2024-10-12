using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exe.Starot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: "customer1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 10, 19, 47, 13, 275, DateTimeKind.Utc).AddTicks(492), new DateTime(2024, 10, 10, 19, 47, 13, 275, DateTimeKind.Utc).AddTicks(492) });

            migrationBuilder.UpdateData(
                table: "Readers",
                keyColumn: "ID",
                keyValue: "reader1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 10, 19, 47, 13, 275, DateTimeKind.Utc).AddTicks(539), new DateTime(2024, 10, 10, 19, 47, 13, 275, DateTimeKind.Utc).AddTicks(539) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "admin",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 19, 47, 13, 132, DateTimeKind.Utc).AddTicks(7180), new DateTime(2024, 10, 10, 19, 47, 13, 132, DateTimeKind.Utc).AddTicks(7180), "$2a$11$MY.wv3cHytvKofNxEXdxCe6CKYz2zBiKxpW6exHWushgP9Ye9HRl6", new DateTime(2024, 11, 9, 19, 47, 13, 274, DateTimeKind.Utc).AddTicks(9604), new DateTime(2024, 10, 10, 19, 47, 13, 274, DateTimeKind.Utc).AddTicks(9615) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user1",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 19, 47, 12, 837, DateTimeKind.Utc).AddTicks(8227), new DateTime(2024, 10, 10, 19, 47, 12, 837, DateTimeKind.Utc).AddTicks(8227), "$2a$11$fAEVj.Vyi9TZp8tjwfdGJudyFava5p1TkquaXMFqB/.cfxYoceDMi", new DateTime(2024, 11, 9, 19, 47, 12, 982, DateTimeKind.Utc).AddTicks(3305), new DateTime(2024, 10, 10, 19, 47, 12, 982, DateTimeKind.Utc).AddTicks(3317) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 19, 47, 12, 982, DateTimeKind.Utc).AddTicks(3427), new DateTime(2024, 10, 10, 19, 47, 12, 982, DateTimeKind.Utc).AddTicks(3427), "$2a$11$i/TtWHqJwF1CEF2BzRX9c.C7JJTKH2avUjn6nRihwjur9v0YQHetC", new DateTime(2024, 11, 9, 19, 47, 13, 132, DateTimeKind.Utc).AddTicks(7073), new DateTime(2024, 10, 10, 19, 47, 13, 132, DateTimeKind.Utc).AddTicks(7083) });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: "customer1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 10, 10, 42, 27, 382, DateTimeKind.Utc).AddTicks(4846), new DateTime(2024, 10, 10, 10, 42, 27, 382, DateTimeKind.Utc).AddTicks(4846) });

            migrationBuilder.UpdateData(
                table: "Readers",
                keyColumn: "ID",
                keyValue: "reader1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 10, 10, 42, 27, 382, DateTimeKind.Utc).AddTicks(4890), new DateTime(2024, 10, 10, 10, 42, 27, 382, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "admin",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 10, 42, 27, 228, DateTimeKind.Utc).AddTicks(5274), new DateTime(2024, 10, 10, 10, 42, 27, 228, DateTimeKind.Utc).AddTicks(5274), "$2a$11$BtAK4YCTxHMYN9LsQtW9FO.yeXKOv5wmmNSkd5vgQz/pXADP3qM6u", new DateTime(2024, 11, 9, 10, 42, 27, 382, DateTimeKind.Utc).AddTicks(3900), new DateTime(2024, 10, 10, 10, 42, 27, 382, DateTimeKind.Utc).AddTicks(3914) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user1",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 10, 42, 26, 926, DateTimeKind.Utc).AddTicks(8849), new DateTime(2024, 10, 10, 10, 42, 26, 926, DateTimeKind.Utc).AddTicks(8849), "$2a$11$w7uDIPRRjJCFBTOuu29mAOq5SnC8Gis9DWGzphUJ8WRnVCIc175oy", new DateTime(2024, 11, 9, 10, 42, 27, 77, DateTimeKind.Utc).AddTicks(6933), new DateTime(2024, 10, 10, 10, 42, 27, 77, DateTimeKind.Utc).AddTicks(6944) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 10, 42, 27, 77, DateTimeKind.Utc).AddTicks(7061), new DateTime(2024, 10, 10, 10, 42, 27, 77, DateTimeKind.Utc).AddTicks(7061), "$2a$11$uad2ka6RWYbMtK4absKP0OKp4TQf8GfoqPZHW5Xp/FZMK4D.3Yc8u", new DateTime(2024, 11, 9, 10, 42, 27, 228, DateTimeKind.Utc).AddTicks(5157), new DateTime(2024, 10, 10, 10, 42, 27, 228, DateTimeKind.Utc).AddTicks(5176) });
        }
    }
}
