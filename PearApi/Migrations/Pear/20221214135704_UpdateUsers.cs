using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class UpdateUsers : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropColumn(
             name: "Name",
             table: "Users");

         migrationBuilder.DropColumn(
             name: "Phone",
             table: "Users");

         migrationBuilder.RenameColumn(
             name: "Token",
             table: "RefreshTokens",
             newName: "TokenString");

         migrationBuilder.AlterColumn<DateTime>(
             name: "LastSignIn",
             table: "Users",
             type: "timestamp with time zone",
             nullable: false,
             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
             oldClrType: typeof(DateTime),
             oldType: "timestamp with time zone",
             oldNullable: true);

         migrationBuilder.AlterColumn<long>(
             name: "Id",
             table: "RefreshTokens",
             type: "bigint",
             nullable: false,
             oldClrType: typeof(int),
             oldType: "integer")
             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
             .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

         migrationBuilder.AddColumn<DateTime>(
             name: "ExpireAt",
             table: "RefreshTokens",
             type: "timestamp with time zone",
             nullable: false,
             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropColumn(
             name: "ExpireAt",
             table: "RefreshTokens");

         migrationBuilder.RenameColumn(
             name: "TokenString",
             table: "RefreshTokens",
             newName: "Token");

         migrationBuilder.AlterColumn<DateTime>(
             name: "LastSignIn",
             table: "Users",
             type: "timestamp with time zone",
             nullable: true,
             oldClrType: typeof(DateTime),
             oldType: "timestamp with time zone");

         migrationBuilder.AddColumn<string>(
             name: "Name",
             table: "Users",
             type: "text",
             nullable: false,
             defaultValue: "");

         migrationBuilder.AddColumn<string>(
             name: "Phone",
             table: "Users",
             type: "text",
             nullable: false,
             defaultValue: "");

         migrationBuilder.AlterColumn<int>(
             name: "Id",
             table: "RefreshTokens",
             type: "integer",
             nullable: false,
             oldClrType: typeof(long),
             oldType: "bigint")
             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
             .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
      }
   }
}