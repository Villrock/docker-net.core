using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class AlertsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.CreateTable(
                name: "AlertNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertNotifications_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.CreateTable(
            //    name: "AlertTypes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        IsSendingEmail = table.Column<bool>(type: "bit", nullable: false),
            //        Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AlertTypes", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "EmailNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailsTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProcessing = table.Column<bool>(type: "bit", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNotifications", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "PeriodTimes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PeriodTimes", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "AlertTypePeriodTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlertTypeId = table.Column<int>(type: "int", nullable: false),
                    PeriodTimeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertTypePeriodTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertTypePeriodTime_AlertTypes_AlertTypeId",
                        column: x => x.AlertTypeId,
                        principalTable: "AlertTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlertTypePeriodTime_PeriodTimes_PeriodTimeId",
                        column: x => x.PeriodTimeId,
                        principalTable: "PeriodTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertNotifications_RequestId",
                table: "AlertNotifications",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertTypePeriodTime_AlertTypeId",
                table: "AlertTypePeriodTime",
                column: "AlertTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertTypePeriodTime_PeriodTimeId",
                table: "AlertTypePeriodTime",
                column: "PeriodTimeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertNotifications");

            migrationBuilder.DropTable(
                name: "AlertTypePeriodTime");

            migrationBuilder.DropTable(
                name: "EmailNotifications");

            //migrationBuilder.DropTable(
            //    name: "AlertTypes");

            //migrationBuilder.DropTable(
            //    name: "PeriodTimes");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(nullable: true),
                    Completed = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    EmailsTo = table.Column<string>(nullable: true),
                    IsProcessing = table.Column<bool>(nullable: false),
                    IsSent = table.Column<bool>(nullable: false),
                    Subject = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });
        }
    }
}
