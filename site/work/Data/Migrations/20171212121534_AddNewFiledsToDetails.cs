using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class AddNewFiledsToDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EstimateDeliveryDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceFile",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimateDeliveryDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "InvoiceFile",
                table: "RequestDetails");
        }
    }
}
