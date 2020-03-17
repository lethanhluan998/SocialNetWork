using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebVuiVN.Data.Entities;
using Microsoft.AspNetCore.Identity;
using WebVuiVN.Data.EF.Configurations;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebVuiVN.Data.Interface;
using WebVuiVN.Data.EF.Extensions;

namespace WebVuiVN.Data.EF
{
    public class WebVuiVNDbContext: IdentityDbContext<AppUser,AppRole,Guid>
    {
        public WebVuiVNDbContext() : base() 
        {
        }
        public WebVuiVNDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { set; get; }
        public DbSet<AppRole> AppRoles { set;get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<PostCategory> Postcategorys { set; get; }
        public DbSet<PostTag> PostTags { set; get; }
        public DbSet<Tag> Tags { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<CommentTag> CommentTags { set; get; }
        public DbSet<Relationship> Relationships { set; get; }
        public DbSet<RoomChat> RoomChats { set; get; }
        public DbSet<Message> Messages { set; get; }
        public DbSet<AppUserChat> AppUserChats { set; get; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Identity Config

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);

            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims")
                .HasKey(x => x.Id);

            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles")
                .HasKey(x => new { x.RoleId, x.UserId });

            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens")
               .HasKey(x => new { x.UserId });
            #endregion Identity Config
            builder.AddConfiguration(new TagConfiguration());
            
            //base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity as IDateTracking;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.DateCreated = DateTime.Now;
                    }
                    changedOrAddedItem.DateModified = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WebVuiVNDbContext>
    {
        public WebVuiVNDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<WebVuiVNDbContext>();
            var connectionString = configuration.GetConnectionString("WebVuiVNContext");
            builder.UseSqlServer(connectionString);
            return new WebVuiVNDbContext(builder.Options);
        }
    }
}
