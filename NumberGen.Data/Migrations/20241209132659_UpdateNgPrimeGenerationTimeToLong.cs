using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NumberGen.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNgPrimeGenerationTimeToLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "GenerationTime",
                schema: "NumberGen",
                table: "NgPrime",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GenerationTime",
                schema: "NumberGen",
                table: "NgPrime",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
