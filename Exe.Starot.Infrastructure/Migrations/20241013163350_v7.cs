using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exe.Starot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: "customer1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 13, 16, 33, 49, 176, DateTimeKind.Utc).AddTicks(5043), new DateTime(2024, 10, 13, 16, 33, 49, 176, DateTimeKind.Utc).AddTicks(5043) });

            migrationBuilder.UpdateData(
                table: "Readers",
                keyColumn: "ID",
                keyValue: "reader1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 13, 16, 33, 49, 176, DateTimeKind.Utc).AddTicks(5086), new DateTime(2024, 10, 13, 16, 33, 49, 176, DateTimeKind.Utc).AddTicks(5086) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "admin",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 16, 33, 49, 42, DateTimeKind.Utc).AddTicks(817), new DateTime(2024, 10, 13, 16, 33, 49, 42, DateTimeKind.Utc).AddTicks(817), "$2a$11$VuPyhyN7bQNZuFpRqKtFK.I4AJ.IzvB5zvkjBnafWZE8NkKGHUciW", new DateTime(2024, 11, 12, 16, 33, 49, 176, DateTimeKind.Utc).AddTicks(4236), new DateTime(2024, 10, 13, 16, 33, 49, 176, DateTimeKind.Utc).AddTicks(4244) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user1",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 16, 33, 48, 770, DateTimeKind.Utc).AddTicks(6987), new DateTime(2024, 10, 13, 16, 33, 48, 770, DateTimeKind.Utc).AddTicks(6987), "$2a$11$3x3xI8fKcGfo5AEnQJgIp.Ads6ls7iT5ZZMfQXhw05bVtSRK7uRVC", new DateTime(2024, 11, 12, 16, 33, 48, 905, DateTimeKind.Utc).AddTicks(4502), new DateTime(2024, 10, 13, 16, 33, 48, 905, DateTimeKind.Utc).AddTicks(4513) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 16, 33, 48, 905, DateTimeKind.Utc).AddTicks(4635), new DateTime(2024, 10, 13, 16, 33, 48, 905, DateTimeKind.Utc).AddTicks(4635), "$2a$11$Gx64bL52Xkj8vSvjBOpqG.jBiJsMvTrfACnMShaxbTd3XIZX1MWhi", new DateTime(2024, 11, 12, 16, 33, 49, 42, DateTimeKind.Utc).AddTicks(695), new DateTime(2024, 10, 13, 16, 33, 49, 42, DateTimeKind.Utc).AddTicks(704) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

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
        }
    }
}
