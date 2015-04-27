using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AutoskolaWeb.Model;

namespace AutoskolaWeb.DAL
{
    public class QuestionRepository : RepositoryBase<Question>
    {
        public QuestionRepository(QuizManagerDbContext db, IIdentity currentUser)
            : base(db, currentUser)
        {
        }
    }
}
