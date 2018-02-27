using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Tasks.Repository.Core;

namespace Tasks.NHibernate
{
    public class NHRepository<TEntity, TIdentity> : NHRepository<TEntity>, ISpecificationRepository<TEntity, TIdentity> //removed abstract, maybe put it in 
        where TEntity : class, IDomainEntity<TIdentity>
    {
        public NHRepository(NHUnitOfWork nhUnitOfWork) 
            : base(nhUnitOfWork)
        {
        }

        public TEntity Get(TIdentity id)
        {
            return this.session.Get<TEntity>(id);
        }

        public List<TEntity> Get(IEnumerable<TIdentity> ids)
        {
            return this.session
                            .CreateCriteria(typeof(TEntity))
                            .Add(Restrictions.In("Id", ids.ToArray()))
                            .List<TEntity>()
                            .ToList();
        }

        public void Remove(TIdentity id)
        {
            this.session.Delete(session.Load<TEntity>(id));
            //TODO this.session.Flush(); see the effect of this
        }
    }
}