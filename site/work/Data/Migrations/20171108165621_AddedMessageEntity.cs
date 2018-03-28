using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class AddedMessageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatusMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    RequestStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatusMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStatusMessages_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestStatusMessages_RequestStatuses_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusMessages_MessageId",
                table: "RequestStatusMessages",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusMessages_RequestStatusId",
                table: "RequestStatusMessages",
                column: "RequestStatusId");

            string insert = @"insert Messages(id, text) values({0},'{1}')";
            migrationBuilder.Sql(string.Format(insert, 10, "Client([CLIENT]) created new request - [LINK]"));
            migrationBuilder.Sql(string.Format(insert, 20, "Manager([MANAGER]) have updated status of request ([LINK]) from NEW REQUEST to QUOTED."));
            migrationBuilder.Sql(string.Format(insert, 30, "Client([CLIENT]) have updated status of request ([LINK]) from QUOTED to PLACE ORDER."));
            migrationBuilder.Sql(string.Format(insert, 40, "Manager([MANAGER]) have updated status of request ([LINK]) from PLACE ORDER to INVOICED."));
            migrationBuilder.Sql(string.Format(insert, 50, "Client([CLIENT]) have updated status of request ([LINK]) from INVOICED to PAYED."));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestStatusMessages");

            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
