using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BethanysPieShop.Migrations
{
    public partial class PieReview_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "PieReviews",
                newName: "UserName");

            migrationBuilder.CreateTable(
                name: "LikesReview",
                columns: table => new
                {
                    LikeReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    PieReviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikesReview", x => x.LikeReviewId);
                    table.ForeignKey(
                        name: "FK_LikesReview_PieReviews_PieReviewId",
                        column: x => x.PieReviewId,
                        principalTable: "PieReviews",
                        principalColumn: "PieReviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikesReview_PieReviewId",
                table: "LikesReview",
                column: "PieReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikesReview");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "PieReviews",
                newName: "Username");
        }
    }
}
