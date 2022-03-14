using Cards.Contracts;
using Cards.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cards.Repository
{
    /// <summary>
    /// This is base class for repositories that contains methods for write data to file, also find all and find by conditions.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected Serializer Serializer { get; set; } 

        public RepositoryBase(Serializer serializer)
        {
            Serializer = serializer;
        }

        public void Write(string path, T entity)
        {
            string contents = Serializer.Serialize(entity);

            DataInitializer.WriteContents(path, contents);
        }

        public IList<T> FindAll(string content)
        {
            return Serializer.Deserialize<List<T>>(content);
        }

        public IList<T> FindByCondition(Func<T, bool> func, string content)
        {
            return Serializer.Deserialize<List<T>>(content)
                .Where(func)
                .ToList();
        }
    }
}
