using System;
using System.Linq.Expressions;

namespace Tasks.Repository.Core
{
    /// <summary>
    /// Extension methods that implement And, Or and Not
    /// </summary>
    public static class ExpressionSpecificationExtensions
    {
        /// <summary>
        /// Performs an And operation on the 2 provided expressions
        /// </summary>
        /// <param name="left">The first expression to join (left side)</param>
        /// <param name="right">The expression to join the first one to (right side)</param>
        /// <returns>A combined And expression based on the two joined expressions</returns>
        public static IExpressionSpecification<T> And<T>(this IExpressionSpecification<T> left, IExpressionSpecification<T> right) where T : class
        {
            var objParam = Expression.Parameter(typeof(T), "obj");

            var newExpr = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    Expression.Invoke(left.SpecExpression, objParam),
                    Expression.Invoke(right.SpecExpression, objParam)
                ),
                objParam
            );

            return new ExpressionSpecification<T>(newExpr);
        }

        /// <summary>
        /// Performs an NOT operation on the expression
        /// </summary>
        /// <param name="left">The first expression to join (left side)</param>
        /// <param name="right">The expression to join the first one to (right side)</param>
        /// <returns>A combined And expression based on the two joined expressions</returns>
        public static IExpressionSpecification<T> Not<T>(this IExpressionSpecification<T> left, IExpressionSpecification<T> right) where T : class
        {
            var objParam = Expression.Parameter(typeof(T), "obj");

            var newExpr = Expression.Lambda<Func<T, bool>>(
                Expression.Not(
                    Expression.Invoke(right.SpecExpression, objParam)
                ),
                objParam
            );

            return left.And(new ExpressionSpecification<T>(newExpr));
        }

        /// <summary>
        /// Performs an NOT operation on the expression
        /// </summary>
        /// <param name="expression">The expression to negate</param>
        /// <returns>A negated expression based on the input expression</returns>
        public static IExpressionSpecification<T> Negate<T>(this IExpressionSpecification<T> expression) where T : class
        {
            var objParam = Expression.Parameter(typeof(T), "obj");

            var newExpr = Expression.Lambda<Func<T, bool>>(
                Expression.Not(
                    Expression.Invoke(expression.SpecExpression, objParam)
                ),
                objParam
            );

            return new ExpressionSpecification<T>(newExpr);
        }

        /// <summary>
        /// Performs an Or operation on the 2 provided expressions
        /// </summary>
        /// <param name="left">The first expression to join (left side)</param>
        /// <param name="right">The expression to join the first one to (right side)</param>
        /// <returns>A combined Or expression based on the two joined expressions</returns>
        public static IExpressionSpecification<T> Or<T>(this IExpressionSpecification<T> left, IExpressionSpecification<T> right) where T : class
        {
            var objParam = Expression.Parameter(typeof(T), "obj");

            var newExpr = Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(
                    Expression.Invoke(left.SpecExpression, objParam),
                    Expression.Invoke(right.SpecExpression, objParam)
                ),
                objParam
            );

            return new ExpressionSpecification<T>(newExpr);
        }

        /// <summary>
        /// Performs an And operation on the 2 provided expressions
        /// </summary>
        /// <param name="left">The first expression to join (left side)</param>
        /// <param name="right">The expression to join the first one to (right side)</param>
        /// <returns>A combined And expression based on the two joined expressions</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right) where T : class
        {
            var objParam = Expression.Parameter(typeof(T), "obj");

            var newExpr = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    Expression.Invoke(left, objParam),
                    Expression.Invoke(right, objParam)
                ),
                objParam
            );

            return newExpr;
        }

        /// <summary>
        /// Performs an Or operation on the 2 provided expressions
        /// </summary>
        /// <param name="left">The first expression to join (left side)</param>
        /// <param name="right">The expression to join the first one to (right side)</param>
        /// <returns>A combined Or expression based on the two joined expressions</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right) where T : class
        {
            var objParam = Expression.Parameter(typeof(T), "obj");

            var newExpr = Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(
                    Expression.Invoke(left, objParam),
                    Expression.Invoke(right, objParam)
                ),
                objParam
            );

            return newExpr;
        }
    }
}
