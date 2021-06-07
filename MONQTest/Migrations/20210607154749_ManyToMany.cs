using Microsoft.EntityFrameworkCore.Migrations;

namespace MONQTest.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailRecipients");

            migrationBuilder.CreateTable(
                name: "MailRecipient",
                columns: table => new
                {
                    MailsID = table.Column<int>(type: "int", nullable: false),
                    RecipientsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailRecipient", x => new { x.MailsID, x.RecipientsID });
                    table.ForeignKey(
                        name: "FK_MailRecipient_Mails_MailsID",
                        column: x => x.MailsID,
                        principalTable: "Mails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MailRecipient_Recipients_RecipientsID",
                        column: x => x.RecipientsID,
                        principalTable: "Recipients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MailRecipient_RecipientsID",
                table: "MailRecipient",
                column: "RecipientsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailRecipient");

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
    }
}
