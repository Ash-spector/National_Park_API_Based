using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace National_Park_API.Migrations
{
    /// <inheritdoc />
    public partial class AddDifficultyToTrail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "typeDifficult",
                table: "Trails",
                newName: "Difficulty");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Difficulty",
                table: "Trails",
                newName: "typeDifficult");
        }
    }
}
