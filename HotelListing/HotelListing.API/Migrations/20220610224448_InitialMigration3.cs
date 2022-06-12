using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.API.Migrations
{
    public partial class InitialMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_countries_CountryId",
                table: "Hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_countries",
                table: "countries");

            migrationBuilder.RenameTable(
                name: "countries",
                newName: "Countries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Countries_CountryId",
                table: "Hotels",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Countries_CountryId",
                table: "Hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "countries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_countries",
                table: "countries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_countries_CountryId",
                table: "Hotels",
                column: "CountryId",
                principalTable: "countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
