using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoC.Migrations
{
    public partial class changedatacontextinterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessLoginUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin");

            migrationBuilder.DropIndex(
                name: "IX_UserLogin_IdAccessLogin",
                table: "UserLogin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin",
                columns: new[] { "IdAccessLogin", "IdUser" });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_IdUser",
                table: "UserLogin",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin");

            migrationBuilder.DropIndex(
                name: "IX_UserLogin_IdUser",
                table: "UserLogin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin",
                columns: new[] { "IdUser", "IdAccessLogin" });

            migrationBuilder.CreateTable(
                name: "AccessLoginUser",
                columns: table => new
                {
                    AccessLoginsIdAccessLogin = table.Column<int>(type: "int", nullable: false),
                    UsersIdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessLoginUser", x => new { x.AccessLoginsIdAccessLogin, x.UsersIdUser });
                    table.ForeignKey(
                        name: "FK_AccessLoginUser_AccessLogin_AccessLoginsIdAccessLogin",
                        column: x => x.AccessLoginsIdAccessLogin,
                        principalTable: "AccessLogin",
                        principalColumn: "IdAccessLogin",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessLoginUser_User_UsersIdUser",
                        column: x => x.UsersIdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_IdAccessLogin",
                table: "UserLogin",
                column: "IdAccessLogin");

            migrationBuilder.CreateIndex(
                name: "IX_AccessLoginUser_UsersIdUser",
                table: "AccessLoginUser",
                column: "UsersIdUser");
        }
    }
}
