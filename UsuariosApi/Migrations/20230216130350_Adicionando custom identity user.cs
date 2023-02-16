using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosApi.Migrations
{
    public partial class Adicionandocustomidentityuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9998,
                column: "ConcurrencyStamp",
                value: "bcd71229-743d-4ab0-9cd1-edcdb066d1bf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9999,
                column: "ConcurrencyStamp",
                value: "3e9f1144-384a-431d-ace4-97a51168896c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "39786605-5700-4e13-8600-71fba327a7d4", "AQAAAAEAACcQAAAAECFKyq380LbNmqAHmxSGIbQ8gu8Aes8rWNIu++U4Jgpi7wp7tQK1FJVbtRg10/HwAQ==", "2dbe446a-33f3-4c0c-978f-8e8f7beeb1b7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9998,
                column: "ConcurrencyStamp",
                value: "bdff8ffd-014d-4697-b84f-4ddaced2fd78");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9999,
                column: "ConcurrencyStamp",
                value: "504c1a64-594f-4f68-84af-99a8ddc77956");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82d91c4d-042e-42d1-8d35-5a018f639be2", "AQAAAAEAACcQAAAAEOBSZ1H/RNHtZZkb/ucrJKnUL5h3CFd2X+kPWtGmegSDu+tcdOBWX0YYncCZ8ljOaQ==", "a03438ed-a5c7-49d3-a8ca-34166d14e872" });
        }
    }
}
