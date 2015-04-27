using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;



namespace AutoskolaWeb.Model
{
    public class Question : EntityBase
    {
        public string QuestionText { get; set; }

        public string CorrectAnswer { get; set; }

        public byte[] Image { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }

       

    }
}