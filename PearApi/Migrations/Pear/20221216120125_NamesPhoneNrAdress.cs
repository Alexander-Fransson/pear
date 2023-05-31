using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class NamesPhoneNrAdress : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AddColumn<string>(
             name: "Adress",
             table: "Users",
             type: "text",
             nullable: false,
             defaultValue: "");

         migrationBuilder.AddColumn<string>(
             name: "Name",
             table: "Users",
             type: "text",
             nullable: false,
             defaultValue: "");

         migrationBuilder.AddColumn<string>(
             name: "PhoneNumber",
             table: "Users",
             type: "text",
             nullable: false,
             defaultValue: "");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropColumn(
             name: "Adress",
             table: "Users");

         migrationBuilder.DropColumn(
             name: "Name",
             table: "Users");

         migrationBuilder.DropColumn(
             name: "PhoneNumber",
             table: "Users");
      }
   }
}