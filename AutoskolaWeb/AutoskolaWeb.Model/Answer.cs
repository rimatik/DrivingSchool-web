using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoskolaWeb.Model
{
    public class Answer : EntityBase
    {
        public string AnswerText { get; set; }
       
        public virtual ICollection<Question> Questions { get; set; }

    }
}