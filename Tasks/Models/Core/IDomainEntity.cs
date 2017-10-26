namespace Tasks.Models.Core
{
    public interface IDomainEntity
    {
    }

    public interface IDomainEntity<T> :IDomainEntity
    {
        T Id { get; }
    }    
}