using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ResourceTypeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "DonationRequests");

            migrationBuilder.AddColumn<int>(
                name: "ResourceTypeId",
                table: "DonationRequests",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptedAt",
                table: "DonationRequestClaims",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "DonationRequestClaims",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ResourceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Scale = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationRequests_ResourceTypeId",
                table: "DonationRequests",
                column: "ResourceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonationRequests_ResourceTypes_ResourceTypeId",
                table: "DonationRequests",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonationRequests_ResourceTypes_ResourceTypeId",
                table: "DonationRequests");

            migrationBuilder.DropTable(
                name: "ResourceTypes");

            migrationBuilder.DropIndex(
                name: "IX_DonationRequests_ResourceTypeId",
                table: "DonationRequests");

            migrationBuilder.DropColumn(
                name: "ResourceTypeId",
                table: "DonationRequests");

            migrationBuilder.DropColumn(
                name: "AcceptedAt",
                table: "DonationRequestClaims");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DonationRequestClaims");

            migrationBuilder.AddColumn<string>(
                name: "ResourceType",
                table: "DonationRequests",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
