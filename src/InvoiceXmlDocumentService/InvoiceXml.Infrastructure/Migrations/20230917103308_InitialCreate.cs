using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceXml.Infrastructure.Migrations
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
                    ExternalId = table.Column<int>(type: "int", nullable: false, comment: "Company Id in our system."),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Company name."),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Description"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Document name."),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentXml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAbonentId = table.Column<int>(type: "int", nullable: false, comment: "Sender company Id."),
                    ReceiverAbonentId = table.Column<int>(type: "int", nullable: false, comment: "Receiver company Id."),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date from the document itself."),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Document guid."),
                    InvoiceTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Total of the invoice."),
                    CreatedBy = table.Column<int>(type: "int", nullable: false, comment: "Created by user id."),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date added into the system."),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date of the last status change.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<int>(type: "int", nullable: false, comment: "User Id in the system."),
                    AbonentId = table.Column<int>(type: "int", nullable: false, comment: "Company Id user belongs to."),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date added into the system."),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date of the last status change.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Abonent_AbonentId",
                        column: x => x.AbonentId,
                        principalTable: "Abonent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_AbonentId",
                table: "User",
                column: "AbonentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Abonent");
        }
    }
}
