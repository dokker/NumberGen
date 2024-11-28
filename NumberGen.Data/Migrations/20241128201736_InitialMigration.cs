using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NumberGen.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "NumberGen");

            migrationBuilder.CreateTable(
                name: "NgPrime",
                schema: "NumberGen",
                columns: table => new
                {
                    Number = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    GenerationTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NgPrime", x => x.Number);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NgPrime",
                schema: "NumberGen");
        }
    }
}
