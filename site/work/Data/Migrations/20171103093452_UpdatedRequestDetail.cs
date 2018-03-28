using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class UpdatedRequestDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EquipmentModel",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "Make",
                table: "RequestDetails");

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerId",
                table: "RequestDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerModelId",
                table: "RequestDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestDetails_ManufacturerId",
                table: "RequestDetails",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDetails_ManufacturerModelId",
                table: "RequestDetails",
                column: "ManufacturerModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetails_Manufacturers_ManufacturerId",
                table: "RequestDetails",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetails_ManufacturerModels_ManufacturerModelId",
                table: "RequestDetails",
                column: "ManufacturerModelId",
                principalTable: "ManufacturerModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetails_Manufacturers_ManufacturerId",
                table: "RequestDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetails_ManufacturerModels_ManufacturerModelId",
                table: "RequestDetails");

            migrationBuilder.DropIndex(
                name: "IX_RequestDetails_ManufacturerId",
                table: "RequestDetails");

            migrationBuilder.DropIndex(
                name: "IX_RequestDetails_ManufacturerModelId",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "ManufacturerModelId",
                table: "RequestDetails");

            migrationBuilder.AddColumn<string>(
                name: "EquipmentModel",
                table: "RequestDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "RequestDetails",
                nullable: true);
        }
    }
}
