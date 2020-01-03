using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication15.Data.Migrations
{
    public partial class addCategorypost2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_AspNetUsers_UserId1",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_UserId1",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "posts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "posts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_posts_UserId",
                table: "posts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_AspNetUsers_UserId",
                table: "posts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_AspNetUsers_UserId",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_UserId",
                table: "posts");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "posts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_posts_UserId1",
                table: "posts",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_AspNetUsers_UserId1",
                table: "posts",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
