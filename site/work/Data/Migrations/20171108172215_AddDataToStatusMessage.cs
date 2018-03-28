using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class AddDataToStatusMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var insert = @"insert RequestStatusMessages(MessageId,RequestStatusId) values({0},{1})";
            migrationBuilder.Sql(string.Format(insert, 10, 10));
            migrationBuilder.Sql(string.Format(insert, 20, 20));
            migrationBuilder.Sql(string.Format(insert, 30, 30));
            migrationBuilder.Sql(string.Format(insert, 40, 40));
            migrationBuilder.Sql(string.Format(insert, 50, 50));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
