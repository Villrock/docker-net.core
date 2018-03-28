using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class UpdatedRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestDetails_DetailId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_DetailId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "DetailId",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "RequestDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestDetails_RequestId",
                table: "RequestDetails",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetails_Requests_RequestId",
                table: "RequestDetails",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetails_Requests_RequestId",
                table: "RequestDetails");

            migrationBuilder.DropIndex(
                name: "IX_RequestDetails_RequestId",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "RequestDetails");

            migrationBuilder.AddColumn<int>(
                name: "DetailId",
                table: "Requests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_DetailId",
                table: "Requests",
                column: "DetailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestDetails_DetailId",
                table: "Requests",
                column: "DetailId",
                principalTable: "RequestDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
