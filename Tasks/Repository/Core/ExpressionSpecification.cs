using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Tasks.Repository.Core
{
    public class ExpressionSpecification<T> : ExpressionSpecificationBase<T> where T : class
    {
        private readonly Expression<Func<T, bool>> specExpression;

        public ExpressionSpecification(Expression<Func<T, bool>> specExpression)
        {
            this.specExpression = specExpression;
        }

        public override Expression<Func<T, bool>> SpecExpression
        {
            get { return this.specExpression; }
        }
    }
}