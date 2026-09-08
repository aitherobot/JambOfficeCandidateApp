using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JambOfficeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddInstitutionApplied : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstitutionApplied",
                table: "Candidates",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstitutionApplied",
                table: "Candidates");
        }
    }
}
