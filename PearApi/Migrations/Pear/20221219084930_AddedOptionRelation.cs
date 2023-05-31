using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class AddedOptionRelation : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropColumn(
             name: "OptionId",
             table: "OptionGroups");

         migrationBuilder.AddColumn<int>(
             name: "AmountInStorage",
             table: "Products",
             type: "integer",
             nullable: false,
             defaultValue: 0);

         migrationBuilder.CreateTable(
             name: "OptionRelations",
             columns: table => new
             {
                OptionGroupId = table.Column<long>(type: "bigint", nullable: false),
                OptionId = table.Column<long>(type: "bigint", nullable: false),
                Id = table.Column<long>(type: "bigint", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_OptionRelations", x => new { x.OptionGroupId, x.OptionId });
                table.ForeignKey(
                       name: "FK_OptionRelations_OptionGroups_OptionGroupId",
                       column: x => x.OptionGroupId,
                       principalTable: "OptionGroups",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                       name: "FK_OptionRelations_Options_OptionId",
                       column: x => x.OptionId,
                       principalTable: "Options",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateIndex(
             name: "IX_OptionGroups_ProductId",
             table: "OptionGroups",
             column: "ProductId");

         migrationBuilder.CreateIndex(
             name: "IX_OptionRelations_OptionId",
             table: "OptionRelations",
             column: "OptionId");

         migrationBuilder.AddForeignKey(
             name: "FK_OptionGroups_Products_ProductId",
             table: "OptionGroups",
             column: "ProductId",
             principalTable: "Products",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropForeignKey(
             name: "FK_OptionGroups_Products_ProductId",
             table: "OptionGroups");

         migrationBuilder.DropTable(
             name: "OptionRelations");

         migrationBuilder.DropIndex(
             name: "IX_OptionGroups_ProductId",
             table: "OptionGroups");

         migrationBuilder.DropColumn(
             name: "AmountInStorage",
             table: "Products");

         migrationBuilder.AddColumn<long[]>(
             name: "OptionId",
             table: "OptionGroups",
             type: "bigint[]",
             nullable: false,
             defaultValue: new long[0]);
      }
   }
}