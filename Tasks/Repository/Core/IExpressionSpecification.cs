using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Tasks.Repository.Core
{
    public interface IExpressionSpecification<T> : ISpecification<T>
        where T : class
    {
        Expression<Func<T, bool>> SpecExpression { get; }
    }
}