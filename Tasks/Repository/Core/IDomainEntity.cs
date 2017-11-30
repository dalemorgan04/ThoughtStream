namespace Tasks.Repository.Core
{
    public interface IDomainEntity
    {
    }

    public interface IDomainEntity<T> :IDomainEntity
    {
        T Id { get; }
    }
}