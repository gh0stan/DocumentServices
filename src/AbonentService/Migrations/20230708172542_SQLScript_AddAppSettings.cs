using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbonentService.Migrations
{
    /// <inheritdoc />
    public partial class SQLScriptAddAppSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine("Scripts/AddAppSettings.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
