using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class NewProductStucture : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropColumn(
             name: "OptionGroupId",
             table: "Options");

         migrationBuilder.DropColumn(
             name: "Prio",
             table: "OptionGroups");

         migrationBuilder.RenameColumn(
             name: "Price",
             table: "Products",
             newName: "price");

         migrationBuilder.RenameColumn(
             name: "Name",
             table: "Products",
             newName: "name");

         migrationBuilder.RenameColumn(
             name: "Id",
             table: "Products",
             newName: "id");

         migrationBuilder.RenameColumn(
             name: "Name",
             table: "OptionGroups",
             newName: "Type");

         migrationBuilder.AddColumn<string>(
             name: "category",
             table: "Products",
             type: "text",
             nullable: false,
             defaultValue: "");

         migrationBuilder.AddColumn<string>(
             name: "description",
             table: "Products",
             type: "text",
             nullable: false,
             defaultValue: "");

         migrationBuilder.AddColumn<string>(
             name: "dimensions",
             table: "Products",
             type: "text",
             nullable: false,
             defaultValue: "");

         migrationBuilder.AddColumn<int>(
             name: "discount",
             table: "Products",
             type: "integer",
             nullable: false,
             defaultValue: 0);

         migrationBuilder.AddColumn<int>(
             name: "weight",
             table: "Products",
             type: "integer",
             nullable: false,
             defaultValue: 0);

         migrationBuilder.AddColumn<string>(
             name: "type",
             table: "Options",
             type: "text",
             nullable: false,
             defaultValue: "");

         migrationBuilder.AddColumn<int[]>(
             name: "optionId",
             table: "OptionGroups",
             type: "integer[]",
             nullable: false,
             defaultValue: new int[0]);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropColumn(
             name: "category",
             table: "Products");

         migrationBuilder.DropColumn(
             name: "description",
             table: "Products");

         migrationBuilder.DropColumn(
             name: "dimensions",
             table: "Products");

         migrationBuilder.DropColumn(
             name: "discount",
             table: "Products");

         migrationBuilder.DropColumn(
             name: "weight",
             table: "Products");

         migrationBuilder.DropColumn(
             name: "type",
             table: "Options");

         migrationBuilder.DropColumn(
             name: "optionId",
             table: "OptionGroups");

         migrationBuilder.RenameColumn(
             name: "price",
             table: "Products",
             newName: "Price");

         migrationBuilder.RenameColumn(
             name: "name",
             table: "Products",
             newName: "Name");

         migrationBuilder.RenameColumn(
             name: "id",
             table: "Products",
             newName: "Id");

         migrationBuilder.RenameColumn(
             name: "Type",
             table: "OptionGroups",
             newName: "Name");

         migrationBuilder.AddColumn<int>(
             name: "OptionGroupId",
             table: "Options",
             type: "integer",
             nullable: false,
             defaultValue: 0);

         migrationBuilder.AddColumn<int>(
             name: "Prio",
             table: "OptionGroups",
             type: "integer",
             nullable: false,
             defaultValue: 0);
      }
   }
}