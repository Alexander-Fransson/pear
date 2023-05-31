using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class CreatedPearContext : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.CreateTable(
             name: "OptionGroups",
             columns: table => new
             {
                Id = table.Column<long>(type: "bigint", nullable: false)
                     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                Prio = table.Column<int>(type: "integer", nullable: false),
                ProductId = table.Column<int>(type: "integer", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_OptionGroups", x => x.Id);
             });

         migrationBuilder.CreateTable(
             name: "Options",
             columns: table => new
             {
                Id = table.Column<long>(type: "bigint", nullable: false)
                     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                Price = table.Column<int>(type: "integer", nullable: false),
                OptionGroupId = table.Column<int>(type: "integer", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Options", x => x.Id);
             });

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

         migrationBuilder.CreateTable(
             name: "Products",
             columns: table => new
             {
                Id = table.Column<long>(type: "bigint", nullable: false)
                     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                Price = table.Column<int>(type: "integer", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Products", x => x.Id);
             });

         migrationBuilder.CreateTable(
             name: "Users",
             columns: table => new
             {
                Id = table.Column<long>(type: "bigint", nullable: false)
                     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                Email = table.Column<string>(type: "text", nullable: false),
                Phone = table.Column<int>(type: "integer", nullable: false),
                PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                Role = table.Column<string>(type: "text", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Users", x => x.Id);
             });
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "OptionGroups");

         migrationBuilder.DropTable(
             name: "Options");

         migrationBuilder.DropTable(
             name: "OrderItems");

         migrationBuilder.DropTable(
             name: "Products");

         migrationBuilder.DropTable(
             name: "Users");
      }
   }
}