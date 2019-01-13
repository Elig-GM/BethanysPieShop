using Microsoft.EntityFrameworkCore.Migrations;

namespace BethanysPieShop.Migrations
{
    public partial class PieReview_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PieReviews_AspNetUsers_UserReviewId",
                table: "PieReviews");

            migrationBuilder.DropIndex(
                name: "IX_PieReviews_UserReviewId",
                table: "PieReviews");

            migrationBuilder.RenameColumn(
                name: "UserReviewId",
                table: "PieReviews",
                newName: "UserName");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "PieReviews",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PieReviews",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PieReviews");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "PieReviews",
                newName: "UserReviewId");

            migrationBuilder.AlterColumn<string>(
                name: "UserReviewId",
                table: "PieReviews",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PieReviews_UserReviewId",
                table: "PieReviews",
                column: "UserReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_PieReviews_AspNetUsers_UserReviewId",
                table: "PieReviews",
                column: "UserReviewId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
