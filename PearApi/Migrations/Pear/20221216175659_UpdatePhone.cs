using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class UpdatePhone : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.RenameColumn(
             name: "PhoneNumber",
             table: "Users",
             newName: "Phone");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.RenameColumn(
             name: "Phone",
             table: "Users",
             newName: "PhoneNumber");
      }
   }
}