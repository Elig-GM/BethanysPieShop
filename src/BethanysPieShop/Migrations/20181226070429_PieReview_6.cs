using Microsoft.EntityFrameworkCore.Migrations;

namespace BethanysPieShop.Migrations
{
    public partial class PieReview_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "PieReviews");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PieReviews",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PieReviews_UserId",
                table: "PieReviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PieReviews_AspNetUsers_UserId",
                table: "PieReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PieReviews_AspNetUsers_UserId",
                table: "PieReviews");

            migrationBuilder.DropIndex(
                name: "IX_PieReviews_UserId",
                table: "PieReviews");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PieReviews",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "PieReviews",
                nullable: true);
        }
    }
}
