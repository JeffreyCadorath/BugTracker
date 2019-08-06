namespace August6thExercise.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<August6thExercise.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(August6thExercise.Models.ApplicationDbContext context)
        {

            if(!context.Roles.Any(x => x.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);

                manager.Create(new IdentityRole("Admin"));
                manager.Create(new IdentityRole("Project Manager"));
                manager.Create(new IdentityRole("Developer"));
                manager.Create(new IdentityRole("Submitter"));
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
