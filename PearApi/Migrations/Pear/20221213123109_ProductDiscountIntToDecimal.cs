using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class ProductDiscountIntToDecimal : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AlterColumn<decimal>(
             name: "discount",
             table: "Products",
             type: "numeric",
             nullable: false,
             oldClrType: typeof(int),
             oldType: "integer");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AlterColumn<int>(
             name: "discount",
             table: "Products",
             type: "integer",
             nullable: false,
             oldClrType: typeof(decimal),
             oldType: "numeric");
      }
   }
}