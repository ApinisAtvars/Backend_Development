using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exercise2.Migrations
{
    /// <inheritdoc />
    public partial class added_backwards_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passports_Travellers_TravellerId",
                table: "Passports");

            migrationBuilder.DropIndex(
                name: "IX_Passports_TravellerId",
                table: "Passports");

            migrationBuilder.AddColumn<int>(
                name: "PassportId",
                table: "Travellers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Travellers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassportId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Travellers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassportId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Travellers_PassportId",
                table: "Travellers",
                column: "PassportId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Travellers_Passports_PassportId",
                table: "Travellers",
                column: "PassportId",
                principalTable: "Passports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travellers_Passports_PassportId",
                table: "Travellers");

            migrationBuilder.DropIndex(
                name: "IX_Travellers_PassportId",
                table: "Travellers");

            migrationBuilder.DropColumn(
                name: "PassportId",
                table: "Travellers");

            migrationBuilder.CreateIndex(
                name: "IX_Passports_TravellerId",
                table: "Passports",
                column: "TravellerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_Travellers_TravellerId",
                table: "Passports",
                column: "TravellerId",
                principalTable: "Travellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
