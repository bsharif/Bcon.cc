using System;
using System.Linq;
using System.Linq.Expressions;
using FluentNHibernate.Data;
using NHibernate;
using NHibernate.Linq;

namespace Gecko.Bcon.DataAccess.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Internal session
        /// </summary>
        protected readonly ISession _session;

        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="session">nHibernate session</param>
        public RepositoryBase(ISession session)
        {
            _session = session;
        }

        /// <summary>
        /// Saves an entity
        /// </summary>
        public bool Save(TEntity entity)
        {
            _session.Save(entity);
            return true;
        }

        /// <summary>
        /// Deletes an entity
        /// </summary>
        public bool Delete(TEntity entity) {
            throw new NotImplementedException("Please implement on repository by repository case");
        }

        /// <summary>
        /// Gets an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(long id)
        {
            return _session.Get<TEntity>(id);
        }

        /// <summary>
        /// Gets a list of entities
        /// </summary>
        public IQueryable<TEntity> List()
        {
            return _session.Query<TEntity>();
        }

        /// <summary>
        /// Gets the first entity, can return Null
        /// </summary>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _session.Query<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Gets the first entity, throws exception if not found
        /// </summary>
        public TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return _session.Query<TEntity>().First(predicate);
        }
    }
}