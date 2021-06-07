using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MONQTest.Migrations
{
    public partial class AddEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false),
                    FailedMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MailRecipients",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MailID = table.Column<int>(type: "int", nullable: false),
                    RecipientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailRecipients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MailRecipients_Mails_MailID",
                        column: x => x.MailID,
                        principalTable: "Mails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MailRecipients_Recipients_RecipientID",
                        column: x => x.RecipientID,
                        principalTable: "Recipients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MailRecipients_MailID",
                table: "MailRecipients",
                column: "MailID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MailRecipients_RecipientID",
                table: "MailRecipients",
                column: "RecipientID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailRecipients");

            migrationBuilder.DropTable(
                name: "Mails");

            migrationBuilder.DropTable(
                name: "Recipients");
        }
    }
}
