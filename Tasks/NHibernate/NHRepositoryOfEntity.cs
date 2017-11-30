using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Repository.Core;

namespace Tasks.NHibernate
{
    public class NHRepository<TEntity> : ISpecificationRepository<TEntity>
        where TEntity : class, IDomainEntity
    {
        private readonly NHUnitOfWork nhUnitOfWork;
        protected ISession session { get { return this.nhUnitOfWork.Session; } }
        public NHRepository(NHUnitOfWork nhUnitOfWork)
        {
            this.nhUnitOfWork = nhUnitOfWork;
        }
        
        public void Add(TEntity entity)
        {
            this.session.Save(entity);
        }
        public void Add(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                this.session.Save(entity);
            }
        }

        public void Update(TEntity entity)
        {
            this.session.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            this.session.Delete(entity);
        }

        public IEnumerable<TEntity> Find(IExpressionSpecification<TEntity> specification)
        {
            IQueryable<TEntity> query = session.Query<TEntity>();
            query = query.Where(specification.SpecExpression);
            return query;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.session.Query<TEntity>();
        }
    }
}