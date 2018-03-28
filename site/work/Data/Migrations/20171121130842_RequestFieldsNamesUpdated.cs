using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class RequestFieldsNamesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteAactivated",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "SIVRequiredDate",
                table: "Requests",
                newName: "SivRequiredDate");

            migrationBuilder.AddColumn<bool>(
                name: "SiteActivated",
                table: "Requests",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteActivated",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "SivRequiredDate",
                table: "Requests",
                newName: "SIVRequiredDate");

            migrationBuilder.AddColumn<bool>(
                name: "SiteAactivated",
                table: "Requests",
                nullable: true);
        }
    }
}
