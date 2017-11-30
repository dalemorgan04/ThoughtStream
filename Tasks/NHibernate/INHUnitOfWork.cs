using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using Tasks.Models;
using Tasks.Repository.Core;

namespace Tasks.NHibernate
{
    public interface INHUnitOfWork: IUnitOfWork
    {
        ISession Session { get; }        
    }
}