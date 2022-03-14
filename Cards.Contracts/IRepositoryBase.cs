using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cards.Contracts
{
    public interface IRepositoryBase<T>
    {
        IList<T> FindAll(string path);
        IList<T> FindByCondition(Func<T, bool> func, string path);
        void Write(string path, IList<T> entity);
    }
}
