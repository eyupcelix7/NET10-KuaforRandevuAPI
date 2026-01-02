using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuaforRandevuAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BarberServices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "BarberServices");
        }
    }
}
