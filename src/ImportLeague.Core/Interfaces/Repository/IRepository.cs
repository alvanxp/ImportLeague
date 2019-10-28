using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ImportLeague.Core.Interfaces.Repository
{
    public interface IRepository<TModel> where TModel : class
    {
        // Get records by it's primary key
        Task<TModel> Get(int id);

        // Get all records
        Task<IEnumerable<TModel>> GetAll();

        // Get all records matching a lambda expression
        Task<IEnumerable<TModel>> Find(Expression<Func<TModel, bool>> predicate);

        // Get the a single matching record or null
        Task<TModel> SingleOrDefault(Expression<Func<TModel, bool>> predicate);

        // Add single record
        Task Add(TModel entity);

        // Add multiple records
        Task AddRange(IEnumerable<TModel> entities);

        // Remove records
        void Remove(TModel entity);

        // remove multiple records
        void RemoveRange(IEnumerable<TModel> entities);

        Task<int> Count(Expression<Func<TModel, bool>> predicate);
    }
}
