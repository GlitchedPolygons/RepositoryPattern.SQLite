using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

using GlitchedPolygons.RepositoryPattern;

namespace GlitchedPolygons.RepositoryPattern.SQLite
{
    public abstract class SQLiteRepository<T1, T2> : IRepository<T1, T2> where T1 : IEntity<T2>
    {
        public T1 this[T2 id] => throw new NotImplementedException();

        public Task<bool> Add(T1 entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddRange(IEnumerable<T1> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T1>> Find(Expression<Func<T1, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<T1> Get(T2 id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T1>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(T1 entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(T2 id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveRange(Expression<Func<T1, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveRange(IEnumerable<T1> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveRange(IEnumerable<T2> ids)
        {
            throw new NotImplementedException();
        }

        public Task<T1> SingleOrDefault(Expression<Func<T1, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T1 entity)
        {
            throw new NotImplementedException();
        }
    }
}
