using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UpisAkademske",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datumUpisa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    datumOvjere = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cijena = table.Column<int>(type: "int", nullable: false),
                    godinaStudija = table.Column<int>(type: "int", nullable: false),
                    napomena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    obnova = table.Column<bool>(type: "bit", nullable: true),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    korisnickiNalogId = table.Column<int>(type: "int", nullable: false),
                    akademskaGodinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpisAkademske", x => x.id);
                    table.ForeignKey(
                        name: "FK_UpisAkademske_AkademskaGodina_akademskaGodinaId",
                        column: x => x.akademskaGodinaId,
                        principalTable: "AkademskaGodina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpisAkademske_KorisnickiNalog_korisnickiNalogId",
                        column: x => x.korisnickiNalogId,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpisAkademske_Student_studentId",
                        column: x => x.studentId,
                        principalTable: "Student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpisAkademske_akademskaGodinaId",
                table: "UpisAkademske",
                column: "akademskaGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisAkademske_korisnickiNalogId",
                table: "UpisAkademske",
                column: "korisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisAkademske_studentId",
                table: "UpisAkademske",
                column: "studentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpisAkademske");
        }
    }
}
