using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsiraRecruitmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class _20JuneUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BusinessUnit",
                table: "JobPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagerEmailAddress",
                table: "JobPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagerNames",
                table: "JobPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "OpeningDate",
                table: "JobPosts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RequiredDriversLicense",
                table: "JobPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BusinessUnit",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "ManagerEmailAddress",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "ManagerNames",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "OpeningDate",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "RequiredDriversLicense",
                table: "JobPosts");
        }
    }
}
