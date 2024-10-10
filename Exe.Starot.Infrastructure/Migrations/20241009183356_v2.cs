using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exe.Starot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: "customer1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 9, 18, 33, 55, 414, DateTimeKind.Utc).AddTicks(6896), new DateTime(2024, 10, 9, 18, 33, 55, 414, DateTimeKind.Utc).AddTicks(6896) });

            migrationBuilder.UpdateData(
                table: "Readers",
                keyColumn: "ID",
                keyValue: "reader1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 9, 18, 33, 55, 414, DateTimeKind.Utc).AddTicks(6962), new DateTime(2024, 10, 9, 18, 33, 55, 414, DateTimeKind.Utc).AddTicks(6962) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user1",
                columns: new[] { "Balance", "CreatedDate", "Gender", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { 0m, new DateTime(2024, 10, 9, 18, 33, 55, 108, DateTimeKind.Utc).AddTicks(9692), "", new DateTime(2024, 10, 9, 18, 33, 55, 108, DateTimeKind.Utc).AddTicks(9692), "$2a$11$Xspo7hXcmONtkN/3QlqL3eDrVEqeuvMI3X91cG4qCqwbBWv9TTpDq", new DateTime(2024, 11, 8, 18, 33, 55, 258, DateTimeKind.Utc).AddTicks(3105), new DateTime(2024, 10, 9, 18, 33, 55, 258, DateTimeKind.Utc).AddTicks(3115) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "Balance", "CreatedDate", "Gender", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { 0m, new DateTime(2024, 10, 9, 18, 33, 55, 258, DateTimeKind.Utc).AddTicks(3201), "", new DateTime(2024, 10, 9, 18, 33, 55, 258, DateTimeKind.Utc).AddTicks(3201), "$2a$11$yPF6Le3esl/5xmN5IAED4e/NVKZq7.BlQJijhdmrQRXL5rDaU4PIq", new DateTime(2024, 11, 8, 18, 33, 55, 414, DateTimeKind.Utc).AddTicks(5734), new DateTime(2024, 10, 9, 18, 33, 55, 414, DateTimeKind.Utc).AddTicks(5745) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: "customer1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 7, 18, 33, 16, 986, DateTimeKind.Utc).AddTicks(941), new DateTime(2024, 10, 7, 18, 33, 16, 986, DateTimeKind.Utc).AddTicks(941) });

            migrationBuilder.UpdateData(
                table: "Readers",
                keyColumn: "ID",
                keyValue: "reader1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 7, 18, 33, 16, 986, DateTimeKind.Utc).AddTicks(974), new DateTime(2024, 10, 7, 18, 33, 16, 986, DateTimeKind.Utc).AddTicks(974) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user1",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 7, 18, 33, 16, 715, DateTimeKind.Utc).AddTicks(8251), new DateTime(2024, 10, 7, 18, 33, 16, 715, DateTimeKind.Utc).AddTicks(8251), "$2a$11$N/1gIJZJWlzE24wuHQeWG.VeDd8WX/TXPKBdNUKlKHfhZy79onWv.", new DateTime(2024, 11, 6, 18, 33, 16, 850, DateTimeKind.Utc).AddTicks(6275), new DateTime(2024, 10, 7, 18, 33, 16, 850, DateTimeKind.Utc).AddTicks(6283) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 7, 18, 33, 16, 850, DateTimeKind.Utc).AddTicks(6369), new DateTime(2024, 10, 7, 18, 33, 16, 850, DateTimeKind.Utc).AddTicks(6369), "$2a$11$a4LAuh2j7sCPCJMmhGj7qezDH0q54/BZ9YMSbuWZPTA7NGCMLjnJu", new DateTime(2024, 11, 6, 18, 33, 16, 986, DateTimeKind.Utc).AddTicks(345), new DateTime(2024, 10, 7, 18, 33, 16, 986, DateTimeKind.Utc).AddTicks(353) });
        }
    }
}
