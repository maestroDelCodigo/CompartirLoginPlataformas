using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoC.Migrations
{
    public partial class changeprimaryKeyinterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserLogin",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_IdAccessLogin",
                table: "UserLogin",
                column: "IdAccessLogin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin");

            migrationBuilder.DropIndex(
                name: "IX_UserLogin_IdAccessLogin",
                table: "UserLogin");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserLogin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin",
                columns: new[] { "IdAccessLogin", "IdUser" });
        }
    }
}
