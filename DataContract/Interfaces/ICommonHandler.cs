using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Interfaces
{
    public interface ICommonHandler<T>
    {
        List<T> GetAll(Expression<Func<T, bool>> predicate = null);

        T Delete(int id);

        T FindBy(Expression<Func<T, bool>> predicate = null);

        T Edit(T obj);

        T Insert(T obj);
    }
}
