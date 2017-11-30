using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Tasks.Repository.Core
{
    public abstract class ExpressionSpecificationBase<T> : IExpressionSpecification<T> where T : class
    {
        public abstract Expression<Func<T, bool>> SpecExpression { get; }
    }
}