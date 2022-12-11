using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Posts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Forum_ForumId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "ForumId",
                table: "Comment",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ForumId",
                table: "Comment",
                newName: "IX_Comment_PostId");

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForumId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Forum_ForumId",
                        column: x => x.ForumId,
                        principalTable: "Forum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_ForumId",
                table: "Post",
                column: "ForumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Comment",
                newName: "ForumId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                newName: "IX_Comment_ForumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Forum_ForumId",
                table: "Comment",
                column: "ForumId",
                principalTable: "Forum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
