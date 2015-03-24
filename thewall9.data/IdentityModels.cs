using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Collections.Generic;

namespace thewall9.data.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public virtual List<SiteUser> SiteUsers { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        //public virtual List<Site> Sites { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Site> Sites { get; set; }
        public DbSet<SiteUser> SiteUsers { get; set; }
        public DbSet<SiteUserRol> SiteUserRoles { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<PageCulture> PageCultures { get; set; }
        public DbSet<ContentProperty> ContentProperties { get; set; }
        public DbSet<ContentPropertyCulture> ContentPropertyCultures { get; set; }
        public DbSet<SiteUrl> SiteUrls { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryCulture> CategoryCultures { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        
    }
}