using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaApi.Migrations
{
    /// <inheritdoc />
    public partial class addGroupContactRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Groups_GroupId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_GroupId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Contacts");

            migrationBuilder.CreateTable(
                name: "ContactGroup",
                columns: table => new
                {
                    ContactsId = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactGroup", x => new { x.ContactsId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_ContactGroup_Contacts_ContactsId",
                        column: x => x.ContactsId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactGroup_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactGroup_GroupId",
                table: "ContactGroup",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactGroup");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Contacts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "GroupId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2,
                column: "GroupId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3,
                column: "GroupId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_GroupId",
                table: "Contacts",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Groups_GroupId",
                table: "Contacts",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
