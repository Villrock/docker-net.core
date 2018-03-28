using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class AddedSubjectToMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            string insert = @"update Messages set Subject = '{1}' where Id = {0}";
            migrationBuilder.Sql(string.Format(insert, "10", "Created New request"));
            migrationBuilder.Sql(string.Format(insert, "20", "Changed status of request to QUOTED"));
            migrationBuilder.Sql(string.Format(insert, "30", "Changed status of request to PLACE ORDER"));
            migrationBuilder.Sql(string.Format(insert, "40", "Changed status of request to PLACE INVOICED"));
            migrationBuilder.Sql(string.Format(insert, "50", "Changed status of request to PLACE PAYED"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Messages");
        }
    }
}
