using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosApi.Migrations
{
    public partial class Criandoroleregular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9999,
                column: "ConcurrencyStamp",
                value: "504c1a64-594f-4f68-84af-99a8ddc77956");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 9998, "bdff8ffd-014d-4697-b84f-4ddaced2fd78", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82d91c4d-042e-42d1-8d35-5a018f639be2", "AQAAAAEAACcQAAAAEOBSZ1H/RNHtZZkb/ucrJKnUL5h3CFd2X+kPWtGmegSDu+tcdOBWX0YYncCZ8ljOaQ==", "a03438ed-a5c7-49d3-a8ca-34166d14e872" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9998);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9999,
                column: "ConcurrencyStamp",
                value: "5e4a7558-e823-4566-a46c-08b5e67bb187");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24efac19-d6a2-432e-bea9-0bc3fa1b332a", "AQAAAAEAACcQAAAAEIHEfFfELqyubh9UfdNk5FYQDJTsrS4vR5ZLs3Es7BYS9gzKsYdZjPZdkBCqoOsg7w==", "55497c6d-9663-4d06-b894-dc78d512c6fe" });
        }
    }
}
