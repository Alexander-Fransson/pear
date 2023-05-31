using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class NewAttributesForOrder : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AddColumn<DateTime>(
             name: "OrderDate",
             table: "OrderItems",
             type: "timestamp with time zone",
             nullable: false,
             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

         migrationBuilder.AddColumn<long>(
             name: "TotalPrice",
             table: "OrderItems",
             type: "bigint",
             nullable: false,
             defaultValue: 0L);

         migrationBuilder.AddColumn<string>(
             name: "userAddress",
             table: "OrderItems",
             type: "text",
             nullable: false,
             defaultValue: "");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropColumn(
             name: "OrderDate",
             table: "OrderItems");

         migrationBuilder.DropColumn(
             name: "TotalPrice",
             table: "OrderItems");

         migrationBuilder.DropColumn(
             name: "userAddress",
             table: "OrderItems");
      }
   }
}