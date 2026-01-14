using Entities.TableModels.Content;
using Entities.TableModels.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database
{
    public class ApplicationDbContext: IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {            
        }

        public DbSet<HomeSlider> HomeSliders { get; set; }        
        public DbSet<About> Abouts { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<TeamCategory> TeamCategories { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<ContactSubmission> ContactSubmissions { get; set; }
        public DbSet<SiteSettings> SiteSettings { get; set; }
        public DbSet<PageBanner> PageBanners { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            
            modelBuilder.Entity<AppUser>().ToTable("Users");
            modelBuilder.Entity<AppRole>().ToTable("Roles");
            modelBuilder.Entity<AppUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<AppUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<AppUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<AppRoleClaim>().ToTable("RoleClaims");
            modelBuilder.Entity<AppUserToken>().ToTable("UserTokens");

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
                
        //        optionsBuilder.UseSqlServer("Server=localhost;Database=BuilderDb;Trusted_Connection=True;TrustServerCertificate=true");
        //    }
        //}
    }
}
