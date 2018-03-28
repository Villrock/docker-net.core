using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class NewFieldsForNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlertTypeId",
                table: "EmailNotifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "EmailNotifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlertTypeId",
                table: "AlertNotifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotifications_AlertTypeId",
                table: "EmailNotifications",
                column: "AlertTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotifications_RequestId",
                table: "EmailNotifications",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertNotifications_AlertTypeId",
                table: "AlertNotifications",
                column: "AlertTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlertNotifications_AlertTypes_AlertTypeId",
                table: "AlertNotifications",
                column: "AlertTypeId",
                principalTable: "AlertTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailNotifications_AlertTypes_AlertTypeId",
                table: "EmailNotifications",
                column: "AlertTypeId",
                principalTable: "AlertTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailNotifications_Requests_RequestId",
                table: "EmailNotifications",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlertNotifications_AlertTypes_AlertTypeId",
                table: "AlertNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailNotifications_AlertTypes_AlertTypeId",
                table: "EmailNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailNotifications_Requests_RequestId",
                table: "EmailNotifications");

            migrationBuilder.DropIndex(
                name: "IX_EmailNotifications_AlertTypeId",
                table: "EmailNotifications");

            migrationBuilder.DropIndex(
                name: "IX_EmailNotifications_RequestId",
                table: "EmailNotifications");

            migrationBuilder.DropIndex(
                name: "IX_AlertNotifications_AlertTypeId",
                table: "AlertNotifications");

            migrationBuilder.DropColumn(
                name: "AlertTypeId",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "AlertTypeId",
                table: "AlertNotifications");
        }
    }
}
