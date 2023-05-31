using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class ChangedDatabaseItemsToUpperCaseUserAdress : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.RenameColumn(
             name: "weight",
             table: "Products",
             newName: "Weight");

         migrationBuilder.RenameColumn(
             name: "price",
             table: "Products",
             newName: "Price");

         migrationBuilder.RenameColumn(
             name: "name",
             table: "Products",
             newName: "Name");

         migrationBuilder.RenameColumn(
             name: "discount",
             table: "Products",
             newName: "Discount");

         migrationBuilder.RenameColumn(
             name: "dimensions",
             table: "Products",
             newName: "Dimensions");

         migrationBuilder.RenameColumn(
             name: "description",
             table: "Products",
             newName: "Description");

         migrationBuilder.RenameColumn(
             name: "category",
             table: "Products",
             newName: "Category");

         migrationBuilder.RenameColumn(
             name: "id",
             table: "Products",
             newName: "Id");

         migrationBuilder.RenameColumn(
             name: "type",
             table: "Options",
             newName: "Type");

         migrationBuilder.RenameColumn(
             name: "optionId",
             table: "OptionGroups",
             newName: "OptionId");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.RenameColumn(
             name: "Weight",
             table: "Products",
             newName: "weight");

         migrationBuilder.RenameColumn(
             name: "Price",
             table: "Products",
             newName: "price");

         migrationBuilder.RenameColumn(
             name: "Name",
             table: "Products",
             newName: "name");

         migrationBuilder.RenameColumn(
             name: "Discount",
             table: "Products",
             newName: "discount");

         migrationBuilder.RenameColumn(
             name: "Dimensions",
             table: "Products",
             newName: "dimensions");

         migrationBuilder.RenameColumn(
             name: "Description",
             table: "Products",
             newName: "description");

         migrationBuilder.RenameColumn(
             name: "Category",
             table: "Products",
             newName: "category");

         migrationBuilder.RenameColumn(
             name: "Id",
             table: "Products",
             newName: "id");

         migrationBuilder.RenameColumn(
             name: "Type",
             table: "Options",
             newName: "type");

         migrationBuilder.RenameColumn(
             name: "OptionId",
             table: "OptionGroups",
             newName: "optionId");
      }
   }
}