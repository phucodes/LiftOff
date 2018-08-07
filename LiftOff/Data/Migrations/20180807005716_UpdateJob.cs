using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiftOff.Data.Migrations
{
    public partial class UpdateJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Job",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PositionLevel",
                table: "Job",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostitionType",
                table: "Job",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "PositionLevel",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "PostitionType",
                table: "Job");
        }
    }
}
