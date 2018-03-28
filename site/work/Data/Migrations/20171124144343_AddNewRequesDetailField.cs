using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class AddNewRequesDetailField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "RequestDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeInstallDueDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDueDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ServiceDueDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeInstallDueDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "PaymentDueDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "ServiceDueDate",
                table: "RequestDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "RequestDetails",
                nullable: true);
        }
    }
}
