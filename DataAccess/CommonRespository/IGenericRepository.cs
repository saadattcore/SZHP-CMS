using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CommonRespository
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();

        T Delete(T entity);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        void Edit(T entity);

        T Add(T entity);

        T GetByID(object objID);
    }
}
