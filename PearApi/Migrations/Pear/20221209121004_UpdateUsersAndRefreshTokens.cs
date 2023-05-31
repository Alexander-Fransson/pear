using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class UpdateUsersAndRefreshTokens : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AlterColumn<string>(
             name: "Phone",
             table: "Users",
             type: "text",
             nullable: false,
             oldClrType: typeof(int),
             oldType: "integer");

         migrationBuilder.AlterColumn<byte[]>(
             name: "PasswordSalt",
             table: "Users",
             type: "bytea",
             nullable: true,
             oldClrType: typeof(byte[]),
             oldType: "bytea");

         migrationBuilder.AlterColumn<byte[]>(
             name: "PasswordHash",
             table: "Users",
             type: "bytea",
             nullable: true,
             oldClrType: typeof(byte[]),
             oldType: "bytea");

         migrationBuilder.AddColumn<DateTime>(
             name: "BannedUntil",
             table: "Users",
             type: "timestamp with time zone",
             nullable: true);

         migrationBuilder.AddColumn<DateTime>(
             name: "CreatedAt",
             table: "Users",
             type: "timestamp with time zone",
             nullable: false,
             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

         migrationBuilder.AddColumn<DateTime>(
             name: "LastSignIn",
             table: "Users",
             type: "timestamp with time zone",
             nullable: true);

         migrationBuilder.AddColumn<DateTime>(
             name: "UpdatedAt",
             table: "Users",
             type: "timestamp with time zone",
             nullable: false,
             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

         migrationBuilder.CreateTable(
             name: "RefreshTokens",
             columns: table => new
             {
                Id = table.Column<int>(type: "integer", nullable: false)
                     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                UserId = table.Column<long>(type: "bigint", nullable: false),
                Token = table.Column<string>(type: "text", nullable: true)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_RefreshTokens", x => x.Id);
             });
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "RefreshTokens");

         migrationBuilder.DropColumn(
             name: "BannedUntil",
             table: "Users");

         migrationBuilder.DropColumn(
             name: "CreatedAt",
             table: "Users");

         migrationBuilder.DropColumn(
             name: "LastSignIn",
             table: "Users");

         migrationBuilder.DropColumn(
             name: "UpdatedAt",
             table: "Users");

         migrationBuilder.AlterColumn<int>(
             name: "Phone",
             table: "Users",
             type: "integer",
             nullable: false,
             oldClrType: typeof(string),
             oldType: "text");

         migrationBuilder.AlterColumn<byte[]>(
             name: "PasswordSalt",
             table: "Users",
             type: "bytea",
             nullable: false,
             defaultValue: new byte[0],
             oldClrType: typeof(byte[]),
             oldType: "bytea",
             oldNullable: true);

         migrationBuilder.AlterColumn<byte[]>(
             name: "PasswordHash",
             table: "Users",
             type: "bytea",
             nullable: false,
             defaultValue: new byte[0],
             oldClrType: typeof(byte[]),
             oldType: "bytea",
             oldNullable: true);
      }
   }
}