using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DonationRequestClaimCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonationRequestClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Message = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deadline = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationRequestClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonationRequestClaims_DonationRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "DonationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonationRequestClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationRequestClaims_RequestId",
                table: "DonationRequestClaims",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationRequestClaims_UserId",
                table: "DonationRequestClaims",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonationRequestClaims");
        }
    }
}
