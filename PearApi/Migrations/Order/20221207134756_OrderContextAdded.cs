using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PearApi.Migrations.Order
{
   /// <inheritdoc />
   public partial class OrderContextAdded : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.CreateTable(
             name: "OrderItems",
             columns: table => new
             {
                Id = table.Column<long>(type: "bigint", nullable: false)
                     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CustomerId = table.Column<long>(type: "bigint", nullable: false),
                LineItemId = table.Column<long>(type: "bigint", nullable: false),
                Status = table.Column<string>(type: "text", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_OrderItems", x => x.Id);
             });
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "OrderItems");
      }
   }
}