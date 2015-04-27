namespace AutoskolaWeb.DAL.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AutoskolaWeb.DAL;
    using AutoskolaWeb.Model;
    using System.Data.Entity.Validation;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<QuizManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QuizManagerDbContext context)
        {

            context.Quizzes.AddOrUpdate(
                p => p.ID,
                new Quiz { ID = 1, QuizName = "Novi kviz", DateCreated = DateTime.Now },
                new Quiz { ID = 2, QuizName = "Stari kviz", DateCreated = DateTime.Now },
                new Quiz { ID = 3, QuizName = "Najbolji kviz", DateCreated = DateTime.Now }

                );
            context.Roles.AddOrUpdate(
                p => p.Name,
                new IdentityRole("Admin"),
                new IdentityRole("User"));
            

        }



    }
}
