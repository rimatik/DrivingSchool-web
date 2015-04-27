using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoskolaWeb.Model;

namespace AutoskolaWeb.DAL
{
    public class QuizManagerDbContext : IdentityDbContext<User>
    {
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public QuizManagerDbContext()
            : base("QuizManagerDbContext", throwIfV1Schema: false)
        {
        }

        public static QuizManagerDbContext Create()
        {
            return new QuizManagerDbContext();
        }
    }
}