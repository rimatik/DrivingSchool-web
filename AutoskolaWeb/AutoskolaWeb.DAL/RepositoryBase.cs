using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AutoskolaWeb.Model;
using System.Data.Entity.Validation;

namespace AutoskolaWeb.DAL
{
    public abstract class RepositoryBase<TEntity>
        where TEntity: EntityBase
    {
        protected QuizManagerDbContext DbContext { get; private set; }
        protected IIdentity CurrentUser { get; private set; }

        public IQueryable<TEntity> All
        {
            get
            {
                return DbContext.Set<TEntity>().AsQueryable();
            }
        }

        protected RepositoryBase(QuizManagerDbContext db, IIdentity currentUser)
        {
            DbContext = db;
            CurrentUser = currentUser;
        }

        public void Insert(TEntity model)
        {
            model.DateCreated = DateTime.Now;
            model.CreatedByUser = CurrentUser.Name;
            DbContext.Set<TEntity>().Add(model);
        }

        public void Update(TEntity model)
        {
            model.DateModified = DateTime.Now;
            model.ModifiedByUser = CurrentUser.Name;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public void Save()
        {
      
            DbContext.SaveChanges();
        }
    
        

        public TEntity Find(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public void Delete(int id)
        {
            var obj = DbContext.Set<TEntity>().Find(id);
            DbContext.Set<TEntity>().Remove(obj);
        }
    }
}
