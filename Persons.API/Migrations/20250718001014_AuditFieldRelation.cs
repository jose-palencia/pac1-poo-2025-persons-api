using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persons.API.Migrations
{
    /// <inheritdoc />
    public partial class AuditFieldRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                table: "countries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "countries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_countries_created_by",
                table: "countries",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_countries_updated_by",
                table: "countries",
                column: "updated_by");

            migrationBuilder.AddForeignKey(
                name: "FK_countries_sec_users_created_by",
                table: "countries",
                column: "created_by",
                principalTable: "sec_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_countries_sec_users_updated_by",
                table: "countries",
                column: "updated_by",
                principalTable: "sec_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_countries_sec_users_created_by",
                table: "countries");

            migrationBuilder.DropForeignKey(
                name: "FK_countries_sec_users_updated_by",
                table: "countries");

            migrationBuilder.DropIndex(
                name: "IX_countries_created_by",
                table: "countries");

            migrationBuilder.DropIndex(
                name: "IX_countries_updated_by",
                table: "countries");

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                table: "countries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "countries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
