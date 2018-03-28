using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class RelocatedFiledFromRequestToDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "IssuedDate",
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
                name: "SerialNumber",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ShippingDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SiteActivated",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SivRequiredDate",
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

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualDespatchDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualInstallationDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternativesRecommendation",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalsCertificationRequired",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CalibrationServiceRequirements",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedDeliveryDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPersons",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedLeadTime",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InstallationDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssuedDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkToBrochure",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalTechnicalSupport",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOfPerson",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentReceivedDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentRunDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProofOfDelivery",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "RequestDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippingDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SiteActivated",
                table: "RequestDetails",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SivRequiredDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingNumberOrCourierDetails",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TrainingDate",
                table: "RequestDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingDocuments",
                table: "RequestDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualDespatchDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "ActualInstallationDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "AlternativesRecommendation",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "ApprovalsCertificationRequired",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "CalibrationServiceRequirements",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "ConfirmedDeliveryDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "ContactPersons",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "EstimatedLeadTime",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "InstallationDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "IssuedDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "LinkToBrochure",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "LocalTechnicalSupport",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "NameOfPerson",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "PaymentReceivedDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "PaymentRunDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "ProofOfDelivery",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "ShippingDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "SiteActivated",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "SivRequiredDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "TrackingNumberOrCourierDetails",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "TrainingDate",
                table: "RequestDetails");

            migrationBuilder.DropColumn(
                name: "TrainingDocuments",
                table: "RequestDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualDespatchDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualInstallationDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternativesRecommendation",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalsCertificationRequired",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CalibrationServiceRequirements",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedDeliveryDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPersons",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedLeadTime",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InstallationDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssuedDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkToBrochure",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalTechnicalSupport",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOfPerson",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentReceivedDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentRunDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProofOfDelivery",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Requests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippingDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SiteActivated",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SivRequiredDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingNumberOrCourierDetails",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TrainingDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingDocuments",
                table: "Requests",
                nullable: true);
        }
    }
}
