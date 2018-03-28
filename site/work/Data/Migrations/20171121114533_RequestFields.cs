using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class RequestFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AncillaryItems",
                table: "Requests");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualDespatchDate",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualInstallationDate",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternativesRecommendation",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalsCertificationRequired",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CalibrationServiceRequirements",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedDeliveryDate",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPersons",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedLeadTime",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InstallationDate",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Issueddate",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkToBrochure",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalTechnicalSupport",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOfPerson",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentReceivedDate",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentRunDate",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProofOfDelivery",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SIVRequiredDate",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippingDate",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SiteAactivated",
                table: "Requests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingNumberOrCourierDetails",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TrainingDate",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingDocuments",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AncillaryItems",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualDespatchDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ActualInstallationDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "AlternativesRecommendation",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ApprovalsCertificationRequired",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CalibrationServiceRequirements",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ConfirmedDeliveryDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ContactPersons",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "EstimatedLeadTime",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "InstallationDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Issueddate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "LinkToBrochure",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "LocalTechnicalSupport",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "NameOfPerson",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "PaymentReceivedDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "PaymentRunDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ProofOfDelivery",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SIVRequiredDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ShippingDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SiteAactivated",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "TrackingNumberOrCourierDetails",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "TrainingDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "TrainingDocuments",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "AncillaryItems",
                table: "RequestDetails");

            migrationBuilder.AddColumn<string>(
                name: "AncillaryItems",
                table: "Requests",
                nullable: true);
        }
    }
}
