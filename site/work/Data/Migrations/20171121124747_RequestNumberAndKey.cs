using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class RequestNumberAndKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Issueddate",
                table: "Requests",
                newName: "IssuedDate");

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestNumber",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestNumber",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "IssuedDate",
                table: "Requests",
                newName: "Issueddate");
        }
    }
}
