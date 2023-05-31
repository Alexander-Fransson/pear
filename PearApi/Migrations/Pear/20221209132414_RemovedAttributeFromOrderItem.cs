using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class RemovedAttributeFromOrderItem : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropColumn(
             name: "LineItemId",
             table: "OrderItems");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AddColumn<long>(
             name: "LineItemId",
             table: "OrderItems",
             type: "bigint",
             nullable: false,
             defaultValue: 0L);
      }
   }
}