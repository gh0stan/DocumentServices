using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformalDocumentService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abonent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<int>(type: "int", nullable: false, comment: "Company Id for internal use."),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Company name.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Property key"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Property value"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Description")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Title"),
                    SenderId = table.Column<int>(type: "int", nullable: false, comment: "Sender abonent Id"),
                    ReceiverId = table.Column<int>(type: "int", nullable: false, comment: "Receiver abonent Id"),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Document created date"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date when document was added to system"),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Document guid")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abonent");

            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DropTable(
                name: "Document");
        }
    }
}
