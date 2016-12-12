using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Publications.Data.Migrations
{
    public partial class TryToAddRelationshipBetweenTemplateAndPublication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
