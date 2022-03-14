using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cards.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(string content);
        IQueryable<T> FindByCondition(Func<T, bool> func, string content);
        void Write(string path, T entity);
    }
}
