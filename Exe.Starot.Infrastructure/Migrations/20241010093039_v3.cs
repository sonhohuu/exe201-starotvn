using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exe.Starot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndHour",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartHour",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: "customer1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 30, 37, 711, DateTimeKind.Utc).AddTicks(3961), new DateTime(2024, 10, 10, 9, 30, 37, 711, DateTimeKind.Utc).AddTicks(3961) });

            migrationBuilder.UpdateData(
                table: "Readers",
                keyColumn: "ID",
                keyValue: "reader1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 30, 37, 711, DateTimeKind.Utc).AddTicks(4008), new DateTime(2024, 10, 10, 9, 30, 37, 711, DateTimeKind.Utc).AddTicks(4008) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user1",
                columns: new[] { "CreatedDate", "Gender", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 30, 37, 438, DateTimeKind.Utc).AddTicks(57), "Female", new DateTime(2024, 10, 10, 9, 30, 37, 438, DateTimeKind.Utc).AddTicks(57), "$2a$11$zsFfb9zXjswY6mat4Km0WOxD8yvwAbny1W6v9grKT.E95X.pnfv/W", new DateTime(2024, 11, 9, 9, 30, 37, 572, DateTimeKind.Utc).AddTicks(8322), new DateTime(2024, 10, 10, 9, 30, 37, 572, DateTimeKind.Utc).AddTicks(8331) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "CreatedDate", "Gender", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 30, 37, 572, DateTimeKind.Utc).AddTicks(8433), "Male", new DateTime(2024, 10, 10, 9, 30, 37, 572, DateTimeKind.Utc).AddTicks(8433), "$2a$11$4HNBC2xPuFvRvv.kObSHP.EDwEhR2p8dVOs66TslHts5m6kVG.kZW", new DateTime(2024, 11, 9, 9, 30, 37, 711, DateTimeKind.Utc).AddTicks(3100), new DateTime(2024, 10, 10, 9, 30, 37, 711, DateTimeKind.Utc).AddTicks(3109) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                columns: new[] { "CreatedDate", "Gender", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 9, 18, 33, 55, 108, DateTimeKind.Utc).AddTicks(9692), "", new DateTime(2024, 10, 9, 18, 33, 55, 108, DateTimeKind.Utc).AddTicks(9692), "$2a$11$Xspo7hXcmONtkN/3QlqL3eDrVEqeuvMI3X91cG4qCqwbBWv9TTpDq", new DateTime(2024, 11, 8, 18, 33, 55, 258, DateTimeKind.Utc).AddTicks(3105), new DateTime(2024, 10, 9, 18, 33, 55, 258, DateTimeKind.Utc).AddTicks(3115) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "CreatedDate", "Gender", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 9, 18, 33, 55, 258, DateTimeKind.Utc).AddTicks(3201), "", new DateTime(2024, 10, 9, 18, 33, 55, 258, DateTimeKind.Utc).AddTicks(3201), "$2a$11$yPF6Le3esl/5xmN5IAED4e/NVKZq7.BlQJijhdmrQRXL5rDaU4PIq", new DateTime(2024, 11, 8, 18, 33, 55, 414, DateTimeKind.Utc).AddTicks(5734), new DateTime(2024, 10, 9, 18, 33, 55, 414, DateTimeKind.Utc).AddTicks(5745) });
        }
    }
}
