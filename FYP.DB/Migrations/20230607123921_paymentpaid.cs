using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYP.DB.Migrations
{
    /// <inheritdoc />
    public partial class paymentpaid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "paid",
                table: "Account_Move",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "paid",
                table: "Account_Move");
        }
    }
}
