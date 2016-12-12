using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Publications.Models;
using Publications.Models.Entities;

namespace Publications.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<PublicationTemplate> PublicationTemplates { get; set; }
        public DbSet<PublicationField> PublicationFields { get; set; }
        public DbSet<FieldTemplate> FieldsTemplates { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<BranchOfKnowledge> BranchOfKnowledges { get; set; }
        public DbSet<BranchOfKnowledgePublication> BranchOfKnowledgePublications { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorPublication> AuthorPublications { get; set; }
        public DbSet<FieldValue> FieldValues { get; set; }
    }
}
