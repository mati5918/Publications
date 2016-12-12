using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Publications.Data.Migrations
{
    public partial class AddFieldValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FieldValues",
                columns: table => new
                {
                    FieldValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldValues", x => x.FieldValueId);
                });

            migrationBuilder.AddColumn<int>(
                name: "FieldValueId",
                table: "PublicationFields",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FieldValueId",
                table: "Publications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PublicationFields_FieldValueId",
                table: "PublicationFields",
                column: "FieldValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_FieldValueId",
                table: "Publications",
                column: "FieldValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_FieldValues_FieldValueId",
                table: "Publications",
                column: "FieldValueId",
                principalTable: "FieldValues",
                principalColumn: "FieldValueId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationFields_FieldValues_FieldValueId",
                table: "PublicationFields",
                column: "FieldValueId",
                principalTable: "FieldValues",
                principalColumn: "FieldValueId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_FieldValues_FieldValueId",
                table: "Publications");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationFields_FieldValues_FieldValueId",
                table: "PublicationFields");

            migrationBuilder.DropIndex(
                name: "IX_PublicationFields_FieldValueId",
                table: "PublicationFields");

            migrationBuilder.DropIndex(
                name: "IX_Publications_FieldValueId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "FieldValueId",
                table: "PublicationFields");

            migrationBuilder.DropColumn(
                name: "FieldValueId",
                table: "Publications");

            migrationBuilder.DropTable(
                name: "FieldValues");
        }
    }
}
