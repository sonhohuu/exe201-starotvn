using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exe.Starot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StartHour",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EndHour",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: "customer1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 48, 39, 552, DateTimeKind.Utc).AddTicks(1532), new DateTime(2024, 10, 10, 9, 48, 39, 552, DateTimeKind.Utc).AddTicks(1532) });

            migrationBuilder.UpdateData(
                table: "Readers",
                keyColumn: "ID",
                keyValue: "reader1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 48, 39, 552, DateTimeKind.Utc).AddTicks(1599), new DateTime(2024, 10, 10, 9, 48, 39, 552, DateTimeKind.Utc).AddTicks(1599) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user1",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 48, 39, 227, DateTimeKind.Utc).AddTicks(3055), new DateTime(2024, 10, 10, 9, 48, 39, 227, DateTimeKind.Utc).AddTicks(3055), "$2a$11$tpFKpb.V7QbUxrBzCf4OxOBTwuXB4v6nQTLMwM2ihW5vrQI.77D8e", new DateTime(2024, 11, 9, 9, 48, 39, 395, DateTimeKind.Utc).AddTicks(2792), new DateTime(2024, 10, 10, 9, 48, 39, 395, DateTimeKind.Utc).AddTicks(2805) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 48, 39, 395, DateTimeKind.Utc).AddTicks(2926), new DateTime(2024, 10, 10, 9, 48, 39, 395, DateTimeKind.Utc).AddTicks(2926), "$2a$11$iqAxb9sns9Ns6UH9n7q7K.40VpJlng0tgb7nfrfTUqz//uhC7vHoa", new DateTime(2024, 11, 9, 9, 48, 39, 552, DateTimeKind.Utc).AddTicks(363), new DateTime(2024, 10, 10, 9, 48, 39, 552, DateTimeKind.Utc).AddTicks(381) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StartHour",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EndHour",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Bookings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 30, 37, 438, DateTimeKind.Utc).AddTicks(57), new DateTime(2024, 10, 10, 9, 30, 37, 438, DateTimeKind.Utc).AddTicks(57), "$2a$11$zsFfb9zXjswY6mat4Km0WOxD8yvwAbny1W6v9grKT.E95X.pnfv/W", new DateTime(2024, 11, 9, 9, 30, 37, 572, DateTimeKind.Utc).AddTicks(8322), new DateTime(2024, 10, 10, 9, 30, 37, 572, DateTimeKind.Utc).AddTicks(8331) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 30, 37, 572, DateTimeKind.Utc).AddTicks(8433), new DateTime(2024, 10, 10, 9, 30, 37, 572, DateTimeKind.Utc).AddTicks(8433), "$2a$11$4HNBC2xPuFvRvv.kObSHP.EDwEhR2p8dVOs66TslHts5m6kVG.kZW", new DateTime(2024, 11, 9, 9, 30, 37, 711, DateTimeKind.Utc).AddTicks(3100), new DateTime(2024, 10, 10, 9, 30, 37, 711, DateTimeKind.Utc).AddTicks(3109) });
        }
    }
}
