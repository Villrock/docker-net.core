using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class UpdatedAdditionalRequestFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedQty",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "RequiredDate",
                table: "RequestDetails");

            migrationBuilder.AddColumn<string>(
                name: "AncillaryItems",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfStudy",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationOfStudy",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullAddress",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialInstructions",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudyInformation",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceWarrantyOptions",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AncillaryItems",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "DateOfStudy",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "DurationOfStudy",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "FullAddress",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SpecialInstructions",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "StudyInformation",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ServiceWarrantyOptions",
                table: "RequestDetails");

            migrationBuilder.AddColumn<string>(
                name: "ExpectedQty",
                table: "RequestDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "RequestDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequiredDate",
                table: "RequestDetails",
                nullable: true);
        }
    }
}
