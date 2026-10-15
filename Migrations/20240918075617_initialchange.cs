using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskBackend.Migrations
{
    /// <inheritdoc />
    public partial class initialchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrganizationName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c0cd8ad0-04fe-4180-8b9c-c1466d14c2ec", null, "Admin", "ADMIN" },
                    { "d1371363-4642-4cf9-a99f-237b85eadd21", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ApplicationUserId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "OrganizationName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a6b83fc3-370a-4d9b-8a02-7f7ed9bb6ae5", 0, null, "e8e503b0-010d-4efc-a506-25934b70785d", "sy335740@gmail.com", true, false, null, "Shubham", "sy335740@gmail.com", "sy335740@GMAIL.COM", "Revocept", "AQAAAAIAAYagAAAAEMFEizAXjlKs/u2SO8MjD2SaSKmekjJWcnwkTsdDdfIHfEu+f5oVouWRJB8Sa23RKQ==", "8950334790", false, "95ba1339-7eff-46ca-9359-d9cd233a5373", false, "sy335740@gmail.com" },
                    { "f3b0ae36-7c3b-4d48-9e7f-0b77e89a6f89", 0, null, "6c9ff1f8-2288-4a33-af64-708746a4e26c", "shubhamyy.aspirefox@gmail.com", true, false, null, "Shubham Yadav", "shubhamyy.aspirefox@gmail.com", "shubhamyy.aspirefox@gmail.com", null, "AQAAAAIAAYagAAAAENOLRD1EqsGBx2mbsMKDp01NBFAKW0K4CnnU/B1BqI/hyiYq6kG3PSaDle2zy8teOw==", "8950334790", false, "f24d6f5a-4d6e-430b-a6d3-4c0e45bc38db", false, "shubhamyy.aspirefox@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "c0cd8ad0-04fe-4180-8b9c-c1466d14c2ec", "a6b83fc3-370a-4d9b-8a02-7f7ed9bb6ae5" },
                    { "d1371363-4642-4cf9-a99f-237b85eadd21", "f3b0ae36-7c3b-4d48-9e7f-0b77e89a6f89" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c0cd8ad0-04fe-4180-8b9c-c1466d14c2ec", "a6b83fc3-370a-4d9b-8a02-7f7ed9bb6ae5" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d1371363-4642-4cf9-a99f-237b85eadd21", "f3b0ae36-7c3b-4d48-9e7f-0b77e89a6f89" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0cd8ad0-04fe-4180-8b9c-c1466d14c2ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1371363-4642-4cf9-a99f-237b85eadd21");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a6b83fc3-370a-4d9b-8a02-7f7ed9bb6ae5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f3b0ae36-7c3b-4d48-9e7f-0b77e89a6f89");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
