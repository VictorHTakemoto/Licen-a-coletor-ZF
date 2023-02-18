using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeviceLicense.Migrations
{
    /// <inheritdoc />
    public partial class CreateZFColetor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceMac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceSN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
