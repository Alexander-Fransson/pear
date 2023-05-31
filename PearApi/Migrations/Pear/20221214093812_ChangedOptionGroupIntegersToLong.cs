using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class ChangedOptionGroupIntegersToLong : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AlterColumn<long[]>(
             name: "optionId",
             table: "OptionGroups",
             type: "bigint[]",
             nullable: false,
             oldClrType: typeof(int[]),
             oldType: "integer[]");

         migrationBuilder.AlterColumn<long>(
             name: "ProductId",
             table: "OptionGroups",
             type: "bigint",
             nullable: false,
             oldClrType: typeof(int),
             oldType: "integer");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AlterColumn<int[]>(
             name: "optionId",
             table: "OptionGroups",
             type: "integer[]",
             nullable: false,
             oldClrType: typeof(long[]),
             oldType: "bigint[]");

         migrationBuilder.AlterColumn<int>(
             name: "ProductId",
             table: "OptionGroups",
             type: "integer",
             nullable: false,
             oldClrType: typeof(long),
             oldType: "bigint");
      }
   }
}