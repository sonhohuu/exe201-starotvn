using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exe.Starot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExperienceYear",
                table: "Readers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                columns: new[] { "CreatedDate", "ExperienceYear", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 13, 19, 40, 46, 603, DateTimeKind.Utc).AddTicks(4206), "", new DateTime(2024, 10, 13, 19, 40, 46, 603, DateTimeKind.Utc).AddTicks(4206) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExperienceYear",
                table: "Readers");

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
    }
}
