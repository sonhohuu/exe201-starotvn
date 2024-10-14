using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exe.Starot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Introduction",
                table: "Readers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Experience",
                table: "Readers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: "customer1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 13, 19, 44, 55, 396, DateTimeKind.Utc).AddTicks(6553), new DateTime(2024, 10, 13, 19, 44, 55, 396, DateTimeKind.Utc).AddTicks(6553) });

            migrationBuilder.UpdateData(
                table: "Readers",
                keyColumn: "ID",
                keyValue: "reader1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 13, 19, 44, 55, 396, DateTimeKind.Utc).AddTicks(6596), new DateTime(2024, 10, 13, 19, 44, 55, 396, DateTimeKind.Utc).AddTicks(6596) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "admin",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 19, 44, 55, 264, DateTimeKind.Utc).AddTicks(4906), new DateTime(2024, 10, 13, 19, 44, 55, 264, DateTimeKind.Utc).AddTicks(4906), "$2a$11$GAkZ8J0Sx6xjJXDyrfFEw.wYXVbQAtdkbnniLiyEJXa.AkKfwaPGi", new DateTime(2024, 11, 12, 19, 44, 55, 396, DateTimeKind.Utc).AddTicks(5818), new DateTime(2024, 10, 13, 19, 44, 55, 396, DateTimeKind.Utc).AddTicks(5828) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user1",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 19, 44, 54, 994, DateTimeKind.Utc).AddTicks(6577), new DateTime(2024, 10, 13, 19, 44, 54, 994, DateTimeKind.Utc).AddTicks(6577), "$2a$11$DkRpqcbUWyY3XPBWrR6/6eP8D7hEowTGdVEw5hPx2wUqmo5h4nXoS", new DateTime(2024, 11, 12, 19, 44, 55, 130, DateTimeKind.Utc).AddTicks(4869), new DateTime(2024, 10, 13, 19, 44, 55, 130, DateTimeKind.Utc).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 19, 44, 55, 130, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 10, 13, 19, 44, 55, 130, DateTimeKind.Utc).AddTicks(5000), "$2a$11$P/Y1ySvl8KUBsrjRjygtb.RRQFAkyaHQ7IxHpYpdDVUUILTLQ7Gpy", new DateTime(2024, 11, 12, 19, 44, 55, 264, DateTimeKind.Utc).AddTicks(4779), new DateTime(2024, 10, 13, 19, 44, 55, 264, DateTimeKind.Utc).AddTicks(4793) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Introduction",
                table: "Readers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Experience",
                table: "Readers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: "customer1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 13, 19, 40, 46, 603, DateTimeKind.Utc).AddTicks(4137), new DateTime(2024, 10, 13, 19, 40, 46, 603, DateTimeKind.Utc).AddTicks(4137) });

            migrationBuilder.UpdateData(
                table: "Readers",
                keyColumn: "ID",
                keyValue: "reader1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 13, 19, 40, 46, 603, DateTimeKind.Utc).AddTicks(4206), new DateTime(2024, 10, 13, 19, 40, 46, 603, DateTimeKind.Utc).AddTicks(4206) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "admin",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 19, 40, 46, 471, DateTimeKind.Utc).AddTicks(2855), new DateTime(2024, 10, 13, 19, 40, 46, 471, DateTimeKind.Utc).AddTicks(2855), "$2a$11$i6TwlrZSVYsYuSyU45J6A.lh6XR7wNySo2fII1xk2kTNHwpdH5WPG", new DateTime(2024, 11, 12, 19, 40, 46, 603, DateTimeKind.Utc).AddTicks(3245), new DateTime(2024, 10, 13, 19, 40, 46, 603, DateTimeKind.Utc).AddTicks(3248) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user1",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 19, 40, 46, 201, DateTimeKind.Utc).AddTicks(9990), new DateTime(2024, 10, 13, 19, 40, 46, 201, DateTimeKind.Utc).AddTicks(9990), "$2a$11$rwa.BTiNYusov.A1xPE0Eumao96UVs/5ET4pTtyUnazr9/B8rhy76", new DateTime(2024, 11, 12, 19, 40, 46, 335, DateTimeKind.Utc).AddTicks(5025), new DateTime(2024, 10, 13, 19, 40, 46, 335, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 19, 40, 46, 335, DateTimeKind.Utc).AddTicks(5169), new DateTime(2024, 10, 13, 19, 40, 46, 335, DateTimeKind.Utc).AddTicks(5169), "$2a$11$OPH32MwTFIsrPthsnXljue9jZk8jOnIWCp9syTTIG/UZDqF6qjG3S", new DateTime(2024, 11, 12, 19, 40, 46, 471, DateTimeKind.Utc).AddTicks(2726), new DateTime(2024, 10, 13, 19, 40, 46, 471, DateTimeKind.Utc).AddTicks(2737) });
        }
    }
}
