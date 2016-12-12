using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Publications.Data.Migrations
{
    public partial class FieldValueFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "PublicationFeildId",
                table: "FieldValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PublicationFieldId",
                table: "FieldValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicationId",
                table: "FieldValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FieldValues_PublicationFieldId",
                table: "FieldValues",
                column: "PublicationFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldValues_PublicationId",
                table: "FieldValues",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldValues_PublicationFields_PublicationFieldId",
                table: "FieldValues",
                column: "PublicationFieldId",
                principalTable: "PublicationFields",
                principalColumn: "PublicationFieldId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FieldValues_Publications_PublicationId",
                table: "FieldValues",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "PublicationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldValues_PublicationFields_PublicationFieldId",
                table: "FieldValues");

            migrationBuilder.DropForeignKey(
                name: "FK_FieldValues_Publications_PublicationId",
                table: "FieldValues");

            migrationBuilder.DropIndex(
                name: "IX_FieldValues_PublicationFieldId",
                table: "FieldValues");

            migrationBuilder.DropIndex(
                name: "IX_FieldValues_PublicationId",
                table: "FieldValues");

            migrationBuilder.DropColumn(
                name: "PublicationFeildId",
                table: "FieldValues");

            migrationBuilder.DropColumn(
                name: "PublicationFieldId",
                table: "FieldValues");

            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "FieldValues");

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
    }
}
