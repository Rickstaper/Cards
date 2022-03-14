using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Cards.Contracts
{
    public interface IRepositoryBase<T>
    {
        IList<T> FindAll(string content);
        IList<T> FindByCondition(Func<T, bool> func, string content);
        void Write(string path, T entity);
    }
}
