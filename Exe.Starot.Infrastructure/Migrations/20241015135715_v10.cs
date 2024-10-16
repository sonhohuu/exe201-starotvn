using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exe.Starot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Orders",
                newName: "PaymentMethod");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "OrderDate",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderTime",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: "customer1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 15, 13, 57, 13, 457, DateTimeKind.Utc).AddTicks(6541), new DateTime(2024, 10, 15, 13, 57, 13, 457, DateTimeKind.Utc).AddTicks(6541) });

            migrationBuilder.UpdateData(
                table: "Readers",
                keyColumn: "ID",
                keyValue: "reader1",
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2024, 10, 15, 13, 57, 13, 457, DateTimeKind.Utc).AddTicks(6588), new DateTime(2024, 10, 15, 13, 57, 13, 457, DateTimeKind.Utc).AddTicks(6588) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "admin",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 15, 13, 57, 13, 325, DateTimeKind.Utc).AddTicks(1302), new DateTime(2024, 10, 15, 13, 57, 13, 325, DateTimeKind.Utc).AddTicks(1302), "$2a$11$xY5NjaMlv0JQi6A57aITOeMsojxXd.bcrrQUZooWTo8Mn.A.2r4UK", new DateTime(2024, 11, 14, 13, 57, 13, 457, DateTimeKind.Utc).AddTicks(5614), new DateTime(2024, 10, 15, 13, 57, 13, 457, DateTimeKind.Utc).AddTicks(5622) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user1",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 15, 13, 57, 13, 51, DateTimeKind.Utc).AddTicks(425), new DateTime(2024, 10, 15, 13, 57, 13, 51, DateTimeKind.Utc).AddTicks(425), "$2a$11$JPRY9GAWJg7MZEPS2BIzsu1ER9dwQqkMj6rP6yCsbDoH3aC5bVe8.", new DateTime(2024, 11, 14, 13, 57, 13, 185, DateTimeKind.Utc).AddTicks(8143), new DateTime(2024, 10, 15, 13, 57, 13, 185, DateTimeKind.Utc).AddTicks(8156) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "CreatedDate", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 15, 13, 57, 13, 185, DateTimeKind.Utc).AddTicks(8271), new DateTime(2024, 10, 15, 13, 57, 13, 185, DateTimeKind.Utc).AddTicks(8271), "$2a$11$vyWT2lTexkmG1orvDvBSL.8R9z/Zaj6Ur8FWR3rtuwXO1zN0gsPGm", new DateTime(2024, 11, 14, 13, 57, 13, 325, DateTimeKind.Utc).AddTicks(1176), new DateTime(2024, 10, 15, 13, 57, 13, 325, DateTimeKind.Utc).AddTicks(1185) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderTime",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Orders",
                newName: "PaymentStatus");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
