using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace JWTMysql.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EMAIL = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValueSql: "'0'"),
                    SALT = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValueSql: "'0'"),
                    PASSWORD = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, defaultValueSql: "'0'"),
                    ROLE = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMAIL",
                table: "user",
                column: "EMAIL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
