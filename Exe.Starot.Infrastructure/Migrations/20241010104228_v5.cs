using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exe.Starot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DateOfBirth",
                table: "Users",
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
                keyValue: "user1",
                columns: new[] { "CreatedDate", "DateOfBirth", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 10, 42, 26, 926, DateTimeKind.Utc).AddTicks(8849), "20/11/2020", new DateTime(2024, 10, 10, 10, 42, 26, 926, DateTimeKind.Utc).AddTicks(8849), "$2a$11$w7uDIPRRjJCFBTOuu29mAOq5SnC8Gis9DWGzphUJ8WRnVCIc175oy", new DateTime(2024, 11, 9, 10, 42, 27, 77, DateTimeKind.Utc).AddTicks(6933), new DateTime(2024, 10, 10, 10, 42, 27, 77, DateTimeKind.Utc).AddTicks(6944) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "CreatedDate", "DateOfBirth", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 10, 42, 27, 77, DateTimeKind.Utc).AddTicks(7061), "20/11/2020", new DateTime(2024, 10, 10, 10, 42, 27, 77, DateTimeKind.Utc).AddTicks(7061), "$2a$11$uad2ka6RWYbMtK4absKP0OKp4TQf8GfoqPZHW5Xp/FZMK4D.3Yc8u", new DateTime(2024, 11, 9, 10, 42, 27, 228, DateTimeKind.Utc).AddTicks(5157), new DateTime(2024, 10, 10, 10, 42, 27, 228, DateTimeKind.Utc).AddTicks(5176) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Balance", "CreatedBy", "CreatedDate", "DateOfBirth", "DeletedBy", "DeletedDay", "Email", "FirstName", "Gender", "Image", "LastName", "LastUpdated", "PasswordHash", "Phone", "RefreshToken", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt", "Role", "UpdatedBy" },
                values: new object[] { "admin", 0m, null, new DateTime(2024, 10, 10, 10, 42, 27, 228, DateTimeKind.Utc).AddTicks(5274), "20/11/2020", null, null, "admin@gmail.com", "Test", "Female", "", "Admin", new DateTime(2024, 10, 10, 10, 42, 27, 228, DateTimeKind.Utc).AddTicks(5274), "$2a$11$BtAK4YCTxHMYN9LsQtW9FO.yeXKOv5wmmNSkd5vgQz/pXADP3qM6u", "123456789", null, new DateTime(2024, 11, 9, 10, 42, 27, 382, DateTimeKind.Utc).AddTicks(3900), new DateTime(2024, 10, 10, 10, 42, 27, 382, DateTimeKind.Utc).AddTicks(3914), "Admin", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
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
                columns: new[] { "CreatedDate", "DateOfBirth", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 48, 39, 227, DateTimeKind.Utc).AddTicks(3055), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 10, 9, 48, 39, 227, DateTimeKind.Utc).AddTicks(3055), "$2a$11$tpFKpb.V7QbUxrBzCf4OxOBTwuXB4v6nQTLMwM2ihW5vrQI.77D8e", new DateTime(2024, 11, 9, 9, 48, 39, 395, DateTimeKind.Utc).AddTicks(2792), new DateTime(2024, 10, 10, 9, 48, 39, 395, DateTimeKind.Utc).AddTicks(2805) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "user2",
                columns: new[] { "CreatedDate", "DateOfBirth", "LastUpdated", "PasswordHash", "RefreshTokenExpiryTime", "RefreshTokenIssuedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 48, 39, 395, DateTimeKind.Utc).AddTicks(2926), new DateTime(1992, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 10, 9, 48, 39, 395, DateTimeKind.Utc).AddTicks(2926), "$2a$11$iqAxb9sns9Ns6UH9n7q7K.40VpJlng0tgb7nfrfTUqz//uhC7vHoa", new DateTime(2024, 11, 9, 9, 48, 39, 552, DateTimeKind.Utc).AddTicks(363), new DateTime(2024, 10, 10, 9, 48, 39, 552, DateTimeKind.Utc).AddTicks(381) });
        }
    }
}
