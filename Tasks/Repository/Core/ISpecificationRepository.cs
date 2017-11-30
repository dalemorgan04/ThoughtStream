using System.Collections.Generic;

namespace Tasks.Repository.Core
{
    public interface ISpecificationRepository<TEntity>
        where TEntity : class, IDomainEntity
    {
        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);        

        IEnumerable<TEntity> Find(IExpressionSpecification<TEntity> specification);
    }

    public interface ISpecificationRepository<TEntity, TIdentity> : ISpecificationRepository<TEntity>
        where TEntity : class, IDomainEntity
    {
        TEntity Get(TIdentity id);
        List<TEntity> Get(IEnumerable<TIdentity> ids);
        void Remove(TIdentity id);
    }
}
