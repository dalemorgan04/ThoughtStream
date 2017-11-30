using System;

namespace Tasks.Repository.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Abort();
        
        //TODO bool IsActive { get; }
    }
}