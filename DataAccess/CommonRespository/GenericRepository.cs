using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Db = DataAccess.Database;

namespace DataAccess.CommonRespository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ITJCMSEntities _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ITJCMSEntities context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }


        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public T GetByID(object objID)
        {
            return _dbSet.Find(objID);
        }


        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return _dbSet.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        //public virtual IEnumerable<T> Get(
        //    Expression<Func<T, bool>> filter = null,
        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //    string includeProperties = "")
        //{
        //    IQueryable<T> query = dbSet;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    foreach (var includeProperty in includeProperties.Split
        //        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        query = query.Include(includeProperty);
        //    }

        //    if (orderBy != null)
        //    {
        //        return orderBy(query).ToList();
        //    }
        //    else
        //    {
        //        return query.ToList();
        //    }
        //}

        //public virtual T GetByID(object id)
        //{
        //    return dbSet.Find(id);
        //}

        //public virtual void Insert(T entity)
        //{
        //    dbSet.Add(entity);
        //}

        //public virtual void Delete(object id)
        //{
        //    T entityToDelete = dbSet.Find(id);
        //    Delete(entityToDelete);
        //}

        //public virtual void Delete(T entityToDelete)
        //{
        //    if (context.Entry(entityToDelete).State == EntityState.Detached)
        //    {
        //        dbSet.Attach(entityToDelete);
        //    }
        //    dbSet.Remove(entityToDelete);
        //}

        //public virtual void Update(T entityToUpdate)
        //{
        //    dbSet.Attach(entityToUpdate);
        //    context.Entry(entityToUpdate).State = EntityState.Modified;
        //}


        //public T GetByID(object objID)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
