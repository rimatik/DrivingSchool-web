using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoskolaWeb.Model
{
    public class Quiz : EntityBase
    {
        [Required(ErrorMessage = "Polje 'Naziv kivza' je obavezno za unos.")]
        [Display(Name = "Naziv kviza")]
        public string QuizName { get; set; }
        public DateTime? StartTime { get; set; }

        [Required(ErrorMessage = "Polje 'Trajanje kviza' je obavezno za unos i odnosi se na minute brojčano, piše se samo brojka npr. 120 označava 120 min")]
        [Display(Name = "Trajanje kviza")]
        public int Duration { get; set; }

        public DateTime? EndTime { get; set; }

        [Required(ErrorMessage = "Polje 'Kviz bodovi' je obavezno za unos.")]
        [Display(Name="Kviz bodovi")]
        public int QuizPoints { get; set ; }

        [ForeignKey("Result")]
        public int? ResultsID { get; set; }

        public virtual Results Result { get; set; }

        public virtual ICollection<Question> Questions { get; set; }



    }
}