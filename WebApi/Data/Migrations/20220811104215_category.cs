using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Data.Migrations
{
    public partial class category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 11, 14, 42, 15, 530, DateTimeKind.Local).AddTicks(7383),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 11, 2, 17, 5, 838, DateTimeKind.Local).AddTicks(8789));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 11, 2, 17, 5, 838, DateTimeKind.Local).AddTicks(8789),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 8, 11, 14, 42, 15, 530, DateTimeKind.Local).AddTicks(7383));
        }
    }
}
