﻿using System.Security.Claims;
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //CONTENT
            modelBuilder.Entity<ContentRoot>()
                .HasKey(m => new { m.ContentID, m.ContentParentID });

            //BLOG
            modelBuilder.Entity<BlogPostCulture>()
                .HasKey(m => new { m.BlogPostID, m.CultureID })
                .HasRequired(a => a.Culture)
                .WithMany(m => m.BlogPostCultures)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<BlogPostTag>()
                .HasKey(m => new { m.BlogTagID, m.BlogPostID, m.CultureID });

            modelBuilder.Entity<BlogCategoryCulture>()
                .HasKey(m => new { m.BlogCategoryID, m.CultureID });

            modelBuilder.Entity<BlogPostCategory>()
                .HasKey(m => new { m.BlogPostID, m.BlogCategoryID });

            modelBuilder.Entity<BlogCategoryCulture>()
                .HasRequired(a => a.Culture)
                .WithMany(m => m.BlogCategoryCultures)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BlogPostFeatureImage>()
                .HasKey(m => new { m.BlogPostID, m.CultureID })
                .HasRequired(m => m.BlogPostCulture)
                .WithOptional(m => m.BlogPostFeatureImage)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<BlogPostImages>()
                .HasKey(m => new { m.BlogPostID, m.CultureID, m.MediaID });

            //PAGES
            modelBuilder.Entity<PageCultureOGraph>()
                 .HasKey(m => new { m.PageID, m.CultureID })
                 .HasRequired(m => m.PageCulture)
                 .WithOptional(m => m.PageCultureOGraph)
                .WillCascadeOnDelete(true);


            //ODATA
            modelBuilder.Entity<OGraphMedia>()
                 .HasKey(m => new { m.OGraphID })
                .HasRequired(m => m.OGraph)
                .WithOptional(m => m.OGraphMedia)
               .WillCascadeOnDelete(true);

            //PRODUCT
            modelBuilder.Entity<Product>()
                .HasOptional(m=>m.Brand)
                .WithMany(o => o.Products)
                .HasForeignKey(ol => ol.BrandID);

            modelBuilder.Entity<ProductCategory>()
                .HasKey(m => new { m.ProductID, m.CategoryID });
            //modelBuilder.Entity<BrandProduct>()
            //    .HasKey(m => new { m.ProductID, m.BrandID });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Site> Sites { get; set; }
        public DbSet<SiteUser> SiteUsers { get; set; }
        public DbSet<SiteUserRol> SiteUserRoles { get; set; }
        public DbSet<SiteUrl> SiteUrls { get; set; }

        public DbSet<Culture> Cultures { get; set; }

        public DbSet<Page> Pages { get; set; }
        public DbSet<PageCulture> PageCultures { get; set; }
        public DbSet<PageCultureOGraph> PageCulturesOGraphs { get; set; }

        public DbSet<OGraph> OGraphs { get; set; }
        public DbSet<OGraphMedia> OGraphMedias { get; set; }

        public DbSet<ContentProperty> ContentProperties { get; set; }
        public DbSet<ContentPropertyCulture> ContentPropertyCultures { get; set; }
        public DbSet<ContentRoot> ContentRoots { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryCulture> CategoryCultures { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCulture> ProductCultures { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        public DbSet<ProductCurrency> ProductCurrencies { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Brand> Brands { get; set; }
       // public DbSet<BrandProduct> BrandProducts { get; set; }

        //BLOG
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostCulture> BlogPostCultures { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<BlogPostTag> BlogPostTags { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogCategoryCulture> BlogCategoryCultures { get; set; }
        public DbSet<BlogPostCategory> BlogPostCategories { get; set; }
        public DbSet<BlogPostFeatureImage> BlogPostFeatureImages { get; set; }
        public DbSet<BlogPostImages> BlogPostImages { get; set; }

        //Media
        public DbSet<Media> Medias { get; set; }

    }
}