using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjustesNasEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DurantionDays",
                table: "Plans",
                newName: "DurationDays");

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentSatatus",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnrollmentSatatus",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "DurationDays",
                table: "Plans",
                newName: "DurantionDays");
        }
    }
}
