using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AutoskolaWeb.Model;

namespace AutoskolaWeb.DAL
{
    public class QuizRepository : RepositoryBase<Quiz>
    {
        public QuizRepository(QuizManagerDbContext dbContext, IIdentity currentUser)
            :base(dbContext, currentUser)
        {
        }

        
    }
}
