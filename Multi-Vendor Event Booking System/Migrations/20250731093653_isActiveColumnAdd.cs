using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Multi_Vendor_Event_Booking_System.Migrations
{
    /// <inheritdoc />
    public partial class isActiveColumnAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Services_Catalogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Services_Catalogs");
        }
    }
}
