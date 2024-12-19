using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PediatriNobetYonetimSistemi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcilDurum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Durum = table.Column<string>(type: "nvarchar(max)", nullable: false)
                    // aciklama yok
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcilDurum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Asistan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bildirim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mesaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Okundu = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bildirim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departman",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmanAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BolumTanimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YatakSayisi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departman", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hasta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Yas = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BolumId = table.Column<int>(type: "int", nullable: false),
                    YatisDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hasta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hoca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BolumId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Randevu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tarih = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HastaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Randevu_Hasta_HastaId",
                        column: x => x.HastaId,
                        principalTable: "Hasta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nobet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tarih = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HocaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nobet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nobet_Hoca_HocaId",
                        column: x => x.HocaId,
                        principalTable: "Hoca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nobet_HocaId",
                table: "Nobet",
                column: "HocaId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_HastaId",
                table: "Randevu",
                column: "HastaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcilDurum");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Asistan");

            migrationBuilder.DropTable(
                name: "Bildirim");

            migrationBuilder.DropTable(
                name: "Departman");

            migrationBuilder.DropTable(
                name: "Nobet");

            migrationBuilder.DropTable(
                name: "Randevu");

            migrationBuilder.DropTable(
                name: "Hoca");

            migrationBuilder.DropTable(
                name: "Hasta");
        }
    }
}
