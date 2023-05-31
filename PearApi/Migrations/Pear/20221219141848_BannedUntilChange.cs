﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PearApi.Migrations.Pear
{
   /// <inheritdoc />
   public partial class BannedUntilChange : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AlterColumn<string>(
             name: "BannedUntil",
             table: "Users",
             type: "text",
             nullable: false,
             defaultValue: "",
             oldClrType: typeof(DateTime),
             oldType: "timestamp with time zone",
             oldNullable: true);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AlterColumn<DateTime>(
             name: "BannedUntil",
             table: "Users",
             type: "timestamp with time zone",
             nullable: true,
             oldClrType: typeof(string),
             oldType: "text");
      }
   }
}