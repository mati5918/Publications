using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Publications.Models;

namespace Publications.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161212135603_TryToAddRelationshipBetweenTemplateAndPublication")]
    partial class TryToAddRelationshipBetweenTemplateAndPublication
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Publications.Models.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Publications.Models.Entities.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AcademicDegree");

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("FirstName");

                    b.Property<string>("SecondName");

                    b.HasKey("AuthorId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Publications.Models.Entities.AuthorPublication", b =>
                {
                    b.Property<int>("AuthorPublicationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<int>("PublicationId");

                    b.HasKey("AuthorPublicationId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PublicationId");

                    b.ToTable("AuthorPublications");
                });

            modelBuilder.Entity("Publications.Models.Entities.BranchOfKnowledge", b =>
                {
                    b.Property<int>("BranchOfKnowledgeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("BranchOfKnowledgeId");

                    b.ToTable("BranchOfKnowledges");
                });

            modelBuilder.Entity("Publications.Models.Entities.BranchOfKnowledgePublication", b =>
                {
                    b.Property<int>("BranchOfKnowledgePublicationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BranchOfKnowledgeId");

                    b.Property<int>("PublicationId");

                    b.HasKey("BranchOfKnowledgePublicationId");

                    b.HasIndex("BranchOfKnowledgeId");

                    b.HasIndex("PublicationId");

                    b.ToTable("BranchOfKnowledgePublications");
                });

            modelBuilder.Entity("Publications.Models.Entities.FieldTemplate", b =>
                {
                    b.Property<int>("FieldTemplateId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FieldId");

                    b.Property<int>("TemplateId");

                    b.HasKey("FieldTemplateId");

                    b.HasIndex("FieldId");

                    b.HasIndex("TemplateId");

                    b.ToTable("FieldsTemplates");
                });

            modelBuilder.Entity("Publications.Models.Entities.FieldValue", b =>
                {
                    b.Property<int>("FieldValueId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("FieldValueId");

                    b.ToTable("FieldValues");
                });

            modelBuilder.Entity("Publications.Models.Entities.Publication", b =>
                {
                    b.Property<int>("PublicationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<int?>("FieldValueId");

                    b.Property<int?>("PublicationTemplateId");

                    b.HasKey("PublicationId");

                    b.HasIndex("FieldValueId");

                    b.HasIndex("PublicationTemplateId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("Publications.Models.Entities.PublicationField", b =>
                {
                    b.Property<int>("PublicationFieldId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FieldValueId");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.HasKey("PublicationFieldId");

                    b.HasIndex("FieldValueId");

                    b.ToTable("PublicationFields");
                });

            modelBuilder.Entity("Publications.Models.Entities.PublicationTemplate", b =>
                {
                    b.Property<int>("PublicationTemplateId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("PublicationTemplateId");

                    b.ToTable("PublicationTemplates");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Publications.Models.Entities.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Publications.Models.Entities.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Publications.Models.Entities.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Publications.Models.Entities.Author", b =>
                {
                    b.HasOne("Publications.Models.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("Publications.Models.Entities.AuthorPublication", b =>
                {
                    b.HasOne("Publications.Models.Entities.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Publications.Models.Entities.Publication", "Publication")
                        .WithMany("AuthorPublication")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Publications.Models.Entities.BranchOfKnowledgePublication", b =>
                {
                    b.HasOne("Publications.Models.Entities.BranchOfKnowledge", "BranchOfKnowledge")
                        .WithMany()
                        .HasForeignKey("BranchOfKnowledgeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Publications.Models.Entities.Publication", "Publication")
                        .WithMany("BranchOfKnowledgePublication")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Publications.Models.Entities.FieldTemplate", b =>
                {
                    b.HasOne("Publications.Models.Entities.PublicationField", "Field")
                        .WithMany("FieldsTemplates")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Publications.Models.Entities.PublicationTemplate", "Template")
                        .WithMany("FieldsTemplates")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Publications.Models.Entities.Publication", b =>
                {
                    b.HasOne("Publications.Models.Entities.FieldValue")
                        .WithMany("Publications")
                        .HasForeignKey("FieldValueId");

                    b.HasOne("Publications.Models.Entities.PublicationTemplate")
                        .WithMany("Publications")
                        .HasForeignKey("PublicationTemplateId");
                });

            modelBuilder.Entity("Publications.Models.Entities.PublicationField", b =>
                {
                    b.HasOne("Publications.Models.Entities.FieldValue")
                        .WithMany("PublicationFields")
                        .HasForeignKey("FieldValueId");
                });
        }
    }
}
