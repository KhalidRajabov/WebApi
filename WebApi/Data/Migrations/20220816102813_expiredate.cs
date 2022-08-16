using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Data.Migrations
{
    public partial class expiredate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 16, 14, 28, 13, 416, DateTimeKind.Local).AddTicks(2755),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 11, 14, 42, 15, 530, DateTimeKind.Local).AddTicks(7383));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 11, 14, 42, 15, 530, DateTimeKind.Local).AddTicks(7383),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 8, 16, 14, 28, 13, 416, DateTimeKind.Local).AddTicks(2755));
        }
    }
}
