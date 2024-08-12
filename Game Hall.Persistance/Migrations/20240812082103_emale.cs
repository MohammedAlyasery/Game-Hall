using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Game_Hall.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class emale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "emale",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "emale",
                table: "Players");
        }
    }
}
