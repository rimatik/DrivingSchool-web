using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoskolaWeb.Model
{
    public class Results : EntityBase
    {

        public string Username { get; set; }

        public DateTime DurationQuiz { get; set; }

        public int Score { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }

    }
}