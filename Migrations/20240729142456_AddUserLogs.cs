using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldMRIServices.Migrations
{
    /// <inheritdoc />
    public partial class AddUserLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Inventories");

            migrationBuilder.RenameColumn(
                name: "Serial",
                table: "Inventories",
                newName: "Tag");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Inventories",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "GeneralImage",
                table: "Computers",
                newName: "SASRaid");

            migrationBuilder.AddColumn<string>(
                name: "BarCode",
                table: "Inventories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MRITS",
                table: "Inventories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fiber",
                table: "Computers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IEEE",
                table: "Computers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Computers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modisk",
                table: "Computers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pin",
                table: "Computers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    PageVisited = table.Column<string>(type: "TEXT", nullable: true),
                    DataAccessed = table.Column<string>(type: "TEXT", nullable: true),
                    CrudOperation = table.Column<string>(type: "TEXT", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LoginTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LogoutTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.DropColumn(
                name: "BarCode",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "MRITS",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Fiber",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "IEEE",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "Modisk",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "Pin",
                table: "Computers");

            migrationBuilder.RenameColumn(
                name: "Tag",
                table: "Inventories",
                newName: "Serial");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Inventories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "SASRaid",
                table: "Computers",
                newName: "GeneralImage");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Inventories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
