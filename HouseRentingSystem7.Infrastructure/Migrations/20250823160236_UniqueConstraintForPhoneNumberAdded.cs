using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem7.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueConstraintForPhoneNumberAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "57bbc871-9b29-434f-b1e6-6b7cc58fc4d1", "AQAAAAIAAYagAAAAEB3Os3zHdb445YGNmhY1kFUiNJTZlCAXJhZncr8Mo5g5uz+NZYRsqgncDVtt/s88tg==", "2997692f-cd2c-44f3-8511-428bbd6aff7e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "447aa975-bd2d-44e1-a8e2-772c03bd6bf2", "AQAAAAIAAYagAAAAEAP82R4RG5+dmZXOhxiRAxbQbKTbezAtIfPl2crDOah39s/UEM4tUO+TjVFfDs4i7g==", "9742430c-d851-429b-b56f-f6153f492250" });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1222756-6bb4-4361-b9b3-128a54814658", "AQAAAAIAAYagAAAAEEd2s5R/2IXwZbrfj0TsTn5zIeTYz6+eJMAJWm33Co9keqws/EN8NkgIJQeKnfdZ5Q==", "1e1824d9-66e8-4493-9f49-695b5eb2801e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0b4051e-9122-428b-b051-48bf6405da2f", "AQAAAAIAAYagAAAAEEzaASsGkJ8aDiZu6g09FgS5tAAGZT2WA2HXvvdAp69mz/3Lh1wlptCFJOmj7E9OaQ==", "3a1cd1c3-6947-40b4-a09e-274d71f3e720" });
        }
    }
}
