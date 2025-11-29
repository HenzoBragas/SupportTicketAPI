using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportTickestAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConvertStatusTicketToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StatusTicket",
                table: "Tickets",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StatusTicket",
                table: "Tickets",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
