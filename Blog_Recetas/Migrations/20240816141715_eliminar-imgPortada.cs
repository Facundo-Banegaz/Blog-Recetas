using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_Recetas.Data.Migrations
{
    /// <inheritdoc />
    public partial class eliminarimgPortada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenUrlPortada",
                table: "Publicaciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenUrlPortada",
                table: "Publicaciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
