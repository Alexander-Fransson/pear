﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class LineItemAddition : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.CreateTable(
             name: "LineItem",
             columns: table => new
             {
                Id = table.Column<long>(type: "bigint", nullable: false)
                     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                OrderId = table.Column<long>(type: "bigint", nullable: false),
                FullProduct = table.Column<string>(type: "text", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_LineItem", x => x.Id);
             });
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "LineItem");
      }
   }
}