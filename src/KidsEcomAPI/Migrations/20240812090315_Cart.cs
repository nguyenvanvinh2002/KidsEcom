using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsEcomAPI.Migrations
{
    /// <inheritdoc />
    public partial class Cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "DanhGia",
                newName: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "IdSp",
                table: "DanhGia",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSp = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenSp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaSp = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropColumn(
                name: "IdSp",
                table: "DanhGia");

            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "DanhGia",
                newName: "Content");
        }
    }
}
