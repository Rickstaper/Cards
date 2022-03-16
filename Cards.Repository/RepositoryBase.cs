using Cards.Contracts;
using Cards.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Write(string path, IList<T> entity)
        {
            string contents = Serializer.Serialize(entity);

            DataInitializer.WriteContents(path, contents);
        }

        public IList<T> FindAll(string path)
        {
            string content = DataInitializer.GetContents(path);

            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            return Serializer.Deserialize<List<T>>(content);
        }

        public IList<T> FindByCondition(Func<T, bool> func, string path)
        {
            string content = DataInitializer.GetContents(path);

            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            return Serializer.Deserialize<List<T>>(content)
                             .Where(func)
                             .ToList();
        }
    }
}
