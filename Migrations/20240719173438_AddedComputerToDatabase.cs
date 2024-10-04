using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldMRIServices.Migrations
{
    /// <inheritdoc />
    public partial class AddedComputerToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Serial",
                table: "Inventories",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Qty",
                table: "Inventories",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pin",
                table: "Inventories",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Inventories",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);


            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Inventories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Permissions",
                table: "AspNetRoles",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "Images",
                table: "Inventories",
                newName: "Tag");

            migrationBuilder.AlterColumn<string>(
                name: "Serial",
                table: "Inventories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Qty",
                table: "Inventories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Pin",
                table: "Inventories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Inventories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "BarCodes",
                table: "Inventories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateRemoved",
                table: "Inventories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateReturned",
                table: "Inventories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeneralImage",
                table: "Inventories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryName",
                table: "Inventories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemovedBy",
                table: "Inventories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnedBy",
                table: "Inventories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Inventories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Computers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComputerPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComputerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerPhotos_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DVD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BarCodes = table.Column<string>(type: "TEXT", nullable: true),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    GeneralImage = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Qunity = table.Column<string>(type: "TEXT", nullable: true),
                    Serial = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DVDs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BarCodes = table.Column<string>(type: "TEXT", nullable: true),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    GeneralImage = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Qunity = table.Column<string>(type: "TEXT", nullable: true),
                    Serial = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVDs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fibers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BarCodes = table.Column<string>(type: "TEXT", nullable: true),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    GeneralImage = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Manufactor = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Qty = table.Column<string>(type: "TEXT", nullable: true),
                    Serial = table.Column<string>(type: "TEXT", nullable: true),
                    Size = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fibers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Harddrives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BarCodes = table.Column<string>(type: "TEXT", nullable: true),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    GeneralImage = table.Column<string>(type: "TEXT", nullable: true),
                    Harddrives = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Qty = table.Column<string>(type: "TEXT", nullable: true),
                    Serial = table.Column<string>(type: "TEXT", nullable: true),
                    Size = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Harddrives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IEEEs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BarCodes = table.Column<string>(type: "TEXT", nullable: true),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    GeneralImage = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Qunity = table.Column<string>(type: "TEXT", nullable: true),
                    Serial = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IEEEs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Memories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BarCodes = table.Column<string>(type: "TEXT", nullable: true),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    GeneralImage = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Qunity = table.Column<string>(type: "TEXT", nullable: true),
                    Size = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modisks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BarCodes = table.Column<string>(type: "TEXT", nullable: true),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    GeneralImage = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Qty = table.Column<string>(type: "TEXT", nullable: true),
                    Serial = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modisks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monitores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BarCodes = table.Column<string>(type: "TEXT", nullable: true),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    GeneralImage = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Manufactor = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Qty = table.Column<string>(type: "TEXT", nullable: true),
                    Resolution = table.Column<string>(type: "TEXT", nullable: true),
                    Serial = table.Column<string>(type: "TEXT", nullable: true),
                    Size = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Others",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BarCodes = table.Column<string>(type: "TEXT", nullable: true),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    GeneralImage = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Qty = table.Column<string>(type: "TEXT", nullable: true),
                    Serial = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Others", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    InventoryName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BarCodes = table.Column<string>(type: "TEXT", nullable: true),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    GeneralImage = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Qty = table.Column<string>(type: "TEXT", nullable: true),
                    Serial = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DVDPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DVDId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    DVDModelId = table.Column<int>(type: "INTEGER", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVDPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DVDPhotos_DVD_DVDId",
                        column: x => x.DVDId,
                        principalTable: "DVD",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DVDPhotos_DVDs_DVDModelId",
                        column: x => x.DVDModelId,
                        principalTable: "DVDs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FiberPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FiberId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiberPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiberPhotos_Fibers_FiberId",
                        column: x => x.FiberId,
                        principalTable: "Fibers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HarddrivePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    harddriveId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    InventoryName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HarddrivePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HarddrivePhotos_Harddrives_harddriveId",
                        column: x => x.harddriveId,
                        principalTable: "Harddrives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IEEEPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IEEEId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IEEEPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IEEEPhotos_IEEEs_IEEEId",
                        column: x => x.IEEEId,
                        principalTable: "IEEEs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemoryPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemoryPhotos_Memories_MemoryId",
                        column: x => x.MemoryId,
                        principalTable: "Memories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IEEEPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IEEEId = table.Column<int>(type: "INTEGER", nullable: false),
                    MemoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    ModiskId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IEEEPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IEEEPhoto_IEEEs_IEEEId",
                        column: x => x.IEEEId,
                        principalTable: "IEEEs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IEEEPhoto_Memories_MemoryId",
                        column: x => x.MemoryId,
                        principalTable: "Memories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IEEEPhoto_Modisks_ModiskId",
                        column: x => x.ModiskId,
                        principalTable: "Modisks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModiskPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    modiskId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    GeneralImage = table.Column<string>(type: "TEXT", nullable: true),
                    Serial = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModiskPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModiskPhotos_Modisks_modiskId",
                        column: x => x.modiskId,
                        principalTable: "Modisks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonitorPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MonitorsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitorPhotos_Monitores_MonitorsId",
                        column: x => x.MonitorsId,
                        principalTable: "Monitores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtherPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OtherId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherPhotos_Others_OtherId",
                        column: x => x.OtherId,
                        principalTable: "Others",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VideoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerName = table.Column<string>(type: "TEXT", nullable: true),
                    GeneralImage = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoPhotos_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComputerPhotos_ComputerId",
                table: "ComputerPhotos",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_DVDPhotos_DVDId",
                table: "DVDPhotos",
                column: "DVDId");

            migrationBuilder.CreateIndex(
                name: "IX_DVDPhotos_DVDModelId",
                table: "DVDPhotos",
                column: "DVDModelId");

            migrationBuilder.CreateIndex(
                name: "IX_FiberPhotos_FiberId",
                table: "FiberPhotos",
                column: "FiberId");

            migrationBuilder.CreateIndex(
                name: "IX_HarddrivePhotos_harddriveId",
                table: "HarddrivePhotos",
                column: "harddriveId");

            migrationBuilder.CreateIndex(
                name: "IX_IEEEPhoto_IEEEId",
                table: "IEEEPhoto",
                column: "IEEEId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IEEEPhoto_MemoryId",
                table: "IEEEPhoto",
                column: "MemoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IEEEPhoto_ModiskId",
                table: "IEEEPhoto",
                column: "ModiskId");

            migrationBuilder.CreateIndex(
                name: "IX_IEEEPhotos_IEEEId",
                table: "IEEEPhotos",
                column: "IEEEId");

            migrationBuilder.CreateIndex(
                name: "IX_MemoryPhotos_MemoryId",
                table: "MemoryPhotos",
                column: "MemoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ModiskPhotos_modiskId",
                table: "ModiskPhotos",
                column: "modiskId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitorPhotos_MonitorsId",
                table: "MonitorPhotos",
                column: "MonitorsId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherPhotos_OtherId",
                table: "OtherPhotos",
                column: "OtherId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_InventoryId",
                table: "Photos",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoPhotos_VideoId",
                table: "VideoPhotos",
                column: "VideoId");
        }
    }
}
