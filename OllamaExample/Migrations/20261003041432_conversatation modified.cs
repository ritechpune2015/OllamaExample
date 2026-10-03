using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OllamaExample.Migrations
{
    /// <inheritdoc />
    public partial class conversatationmodified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SummaryUpdatedAt",
                table: "Conversations",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "SummaryUpdatedAt",
                table: "Conversations");
        }
    }
}
