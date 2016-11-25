using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Publications.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicationFields",
                columns: table => new
                {
                    PublicationFieldId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationFields", x => x.PublicationFieldId);
                });

            migrationBuilder.CreateTable(
                name: "PublicationTemplates",
                columns: table => new
                {
                    PublicationTemplateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationTemplates", x => x.PublicationTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "FieldsTemplates",
                columns: table => new
                {
                    FieldTemplateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FieldId = table.Column<int>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldsTemplates", x => x.FieldTemplateId);
                    table.ForeignKey(
                        name: "FK_FieldsTemplates_PublicationFields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "PublicationFields",
                        principalColumn: "PublicationFieldId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldsTemplates_PublicationTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "PublicationTemplates",
                        principalColumn: "PublicationTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FieldsTemplates_FieldId",
                table: "FieldsTemplates",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldsTemplates_TemplateId",
                table: "FieldsTemplates",
                column: "TemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldsTemplates");

            migrationBuilder.DropTable(
                name: "PublicationFields");

            migrationBuilder.DropTable(
                name: "PublicationTemplates");
        }
    }
}
