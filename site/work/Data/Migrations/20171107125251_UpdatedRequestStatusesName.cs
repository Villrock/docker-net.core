using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QFlow.Migrations
{
    public partial class UpdatedRequestStatusesName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from RequestStatuses where Id = 60 OR Id = 999");

            string insert = @"update RequestStatuses set Name = '{1}' where Id = {0}";
            migrationBuilder.Sql(string.Format(insert, "10", "New request"));
            migrationBuilder.Sql(string.Format(insert, "20", "Quoted"));
            migrationBuilder.Sql(string.Format(insert, "30", "Place order"));
            migrationBuilder.Sql(string.Format(insert, "40", "Invoiced"));
            migrationBuilder.Sql(string.Format(insert, "50", "Payed"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
