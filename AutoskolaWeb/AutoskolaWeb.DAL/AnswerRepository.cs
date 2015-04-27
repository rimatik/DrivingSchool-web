using AutoskolaWeb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AutoskolaWeb.DAL
{
    public class AnswerRepository : RepositoryBase<Answer>
    {
        public AnswerRepository(QuizManagerDbContext db, IIdentity currentUser)
            : base(db, currentUser)
        { 
        
        }
    }
}
