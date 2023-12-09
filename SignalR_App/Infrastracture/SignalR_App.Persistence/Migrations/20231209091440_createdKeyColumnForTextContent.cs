using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalR_App.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class createdKeyColumnForTextContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "TextContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MetaId",
                table: "TextContents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TextContents_MetaId",
                table: "TextContents",
                column: "MetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TextContents_Metas_MetaId",
                table: "TextContents",
                column: "MetaId",
                principalTable: "Metas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextContents_Metas_MetaId",
                table: "TextContents");

            migrationBuilder.DropIndex(
                name: "IX_TextContents_MetaId",
                table: "TextContents");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "TextContents");

            migrationBuilder.DropColumn(
                name: "MetaId",
                table: "TextContents");
        }
    }
}
