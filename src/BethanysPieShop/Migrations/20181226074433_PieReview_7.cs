using Microsoft.EntityFrameworkCore.Migrations;

namespace BethanysPieShop.Migrations
{
    public partial class PieReview_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PieReviews_AspNetUsers_UserId",
                table: "PieReviews");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PieReviews",
                newName: "UserReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_PieReviews_UserId",
                table: "PieReviews",
                newName: "IX_PieReviews_UserReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_PieReviews_AspNetUsers_UserReviewId",
                table: "PieReviews",
                column: "UserReviewId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PieReviews_AspNetUsers_UserReviewId",
                table: "PieReviews");

            migrationBuilder.RenameColumn(
                name: "UserReviewId",
                table: "PieReviews",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PieReviews_UserReviewId",
                table: "PieReviews",
                newName: "IX_PieReviews_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PieReviews_AspNetUsers_UserId",
                table: "PieReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
