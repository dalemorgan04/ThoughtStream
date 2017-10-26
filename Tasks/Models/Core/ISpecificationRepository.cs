using System.Collections.Generic;

namespace Tasks.Models.Core
{
    public interface ISpecificationRepository<TEntity, TIdentity>
        where TEntity : IDomainEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(TIdentity id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TIdentity id);
    }
}