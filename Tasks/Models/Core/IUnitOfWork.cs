using System;

namespace Tasks.Models.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Abort();
        
        //TODO bool IsActive { get; }
    }
}