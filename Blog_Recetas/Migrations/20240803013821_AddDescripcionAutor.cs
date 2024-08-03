using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_Recetas.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDescripcionAutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Autores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Autores");
        }
    }
}
