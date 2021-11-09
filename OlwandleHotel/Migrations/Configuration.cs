namespace OlwandleHotel.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using OlwandleHotel.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OlwandleHotel.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OlwandleHotel.Models.ApplicationDbContext context)
        {
            /*var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser { UserName = "cruises@paradise.com" };

            userManager.Create(user, "Cruises123@");

            userManager.AddToRole(user.Id, "Cruises");

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.*/
        }
        //Username: admin@paradise.com && Password: Admin123@
        //Username: tour@paradise.com && Password: Tours123@
        //Username: hotel@paradise.com && Password: Hotel123@
        //Username: cruises@paradise.com && Password: Paradise123@
        //Username: aviation@paradise.com && Password: Aviation123@
    }
}

