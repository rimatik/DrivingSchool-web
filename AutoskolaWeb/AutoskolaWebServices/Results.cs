//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoskolaWebServices
{
    using System;
    using System.Collections.Generic;
    
    public partial class Results
    {
        public Results()
        {
            this.Quizs = new HashSet<Quizs>();
        }
    
        public int ID { get; set; }
        public string Username { get; set; }
        public System.DateTime DurationQuiz { get; set; }
        public int Score { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string CreatedByUser { get; set; }
        public string ModifiedByUser { get; set; }
    
        public virtual ICollection<Quizs> Quizs { get; set; }
    }
}
