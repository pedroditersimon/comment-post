using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommentPost.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class FixLastLoginAtType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLoginAt",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastLoginAt",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
