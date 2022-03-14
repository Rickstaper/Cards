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

        public IQueryable<T> FindAll(string content)
        {
            if(string.IsNullOrEmpty(content))
            {
                return null;
            }

            return (IQueryable<T>)Serializer.Deserialize<List<T>>(content);
        }

        public IQueryable<T> FindByCondition(Func<T, bool> func, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            return (IQueryable<T>)Serializer.Deserialize<List<T>>(content)
                .Where(func);
        }
    }
}
