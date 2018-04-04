namespace ConferenceFormSubmittal.DAL.SecurityMigrations
{
    using ConferenceFormSubmittal.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ConferenceFormSubmittal.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\SecurityMigrations";
        }

        protected override void Seed(ConferenceFormSubmittal.DAL.ApplicationDbContext context)
        {
            //------------------------------Create a Role Manager---------------------------------
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Create Role Admin if it does not exist
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Admin"));
            }

            //Create Role Staff if it does not exist
            if (!context.Roles.Any(r => r.Name == "Staff"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Staff"));
            }

            //-------------------------------Create a User Manager-------------------------------
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Now the Admin user named admin with password password
            var adminuser = new ApplicationUser
            {
                UserName = "admin@NCDSB.com",
                Email = "admin@NCDSB.com"
            };
            //Assign admin user to role
            if (!context.Users.Any(u => u.UserName == "admin@NCDSB.com"))
            {
                manager.Create(adminuser, "password");
                manager.AddToRole(adminuser.Id, "Admin");
            }

            //-----------------------------------------------------
            //Now the Staff user named Steward with password password
            var staffuser = new ApplicationUser
            {
                UserName = "staff@NCDSB.com",
                Email = "staff@NCDSB.com"
            };
            //Assign steward user to role
            if (!context.Users.Any(u => u.UserName == "staff@NCDSB.com"))
            {
                manager.Create(staffuser, "password");
                manager.AddToRole(staffuser.Id, "Staff");
            }

            //-----------------------------------------------------
            //Now the user named user with password password
            var user = new ApplicationUser
            {
                UserName = "user@NCDSB.com",
                Email = "user@NCSDB.com"
            };
            //Assign user to role
            if (!context.Users.Any(u => u.UserName == "user@NCDSB.com"))
            {
                manager.Create(user, "password");
            }


        }
    }
}
