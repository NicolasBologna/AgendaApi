using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaApi.Migrations
{
    /// <inheritdoc />
    public partial class addGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contacts",
                table: "Groups");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Contacts",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
