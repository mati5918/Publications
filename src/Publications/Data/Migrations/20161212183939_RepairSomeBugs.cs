using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Publications.Data.Migrations
{
    public partial class RepairSomeBugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_PublicationTemplates_PublicationTemplateId",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Publications_PublicationTemplateId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "PublicationTemplateId",
                table: "Publications");

            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "Publications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Publications_TemplateId",
                table: "Publications",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_PublicationTemplates_TemplateId",
                table: "Publications",
                column: "TemplateId",
                principalTable: "PublicationTemplates",
                principalColumn: "PublicationTemplateId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_PublicationTemplates_TemplateId",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Publications_TemplateId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Publications");

            migrationBuilder.AddColumn<int>(
                name: "PublicationTemplateId",
                table: "Publications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publications_PublicationTemplateId",
                table: "Publications",
                column: "PublicationTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_PublicationTemplates_PublicationTemplateId",
                table: "Publications",
                column: "PublicationTemplateId",
                principalTable: "PublicationTemplates",
                principalColumn: "PublicationTemplateId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
