using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerryMVC.Migrations
{
    /// <inheritdoc />
    public partial class VendorAndArtifactNames : Migration
    {
        /// <inheritdoc />
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BerryModel",
                newName: "ArtifactName");

            migrationBuilder.AddColumn<string>(
                name: "VendorName",
                table: "BerryModel",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtifactName",
                table: "BerryModel");

            migrationBuilder.RenameColumn(
                name: "ArtifactName",
                table: "BerryModel",
                newName: "Name");
        }
    }
}
