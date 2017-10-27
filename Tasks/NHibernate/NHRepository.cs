using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.Models;
using Tasks.Models.Core;

namespace Tasks.NHibernate
{
    public class NHRepository<TEntity, TIdentity> : ISpecificationRepository<TEntity, TIdentity> //removed abstract, maybe put it in 
        where TEntity : IDomainEntity<TIdentity>
    {
        private readonly NHUnitOfWork nhUnitOfWork;
        public NHRepository(NHUnitOfWork nhUnitOfWork)
        {
            this.nhUnitOfWork = nhUnitOfWork;
        }
        protected ISession session { get { return this.nhUnitOfWork.Session; } }

        public void Add(TEntity entity)
        {
            this.session.Save(entity);
        }

        public TEntity Get(TIdentity id)
        {
            return this.session.Get<TEntity>(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return session.CreateCriteria(typeof(TEntity)).List<TEntity>();
        }

        public void Remove(TIdentity id)
        {
            session.Delete(session.Load<TEntity>(id));
            session.Flush();
        }

        public void Update(TEntity entity)
        {
            session.Update(entity);
        }
    }
}