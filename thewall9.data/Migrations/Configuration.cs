namespace thewall9.data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using thewall9.data.Models;


    internal sealed class Configuration : DbMigrationsConfiguration<thewall9.data.Models.ApplicationDbContext>
    {
        //CHANGE ME
        public const string EmailRoot = "oz@thewall9.com";
        //CHANGE ME
        public const string NameRoot = "Oswaldo Alvarez";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "thewall9.data.Models.ApplicationDbContext";
        }

        protected override void Seed(thewall9.data.Models.ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            ApplicationUser userRoot = null;
            if (userManager.FindByName(EmailRoot) == null)
            {
                userRoot = new ApplicationUser
                {
                    UserName = EmailRoot,
                    Name = NameRoot
                };
                userManager.Create(userRoot, "123456");

                roleManager.Create(new IdentityRole { Name = "admin" });

                userManager.AddToRole(userRoot.Id, "admin");
            }
        }
    }
}
