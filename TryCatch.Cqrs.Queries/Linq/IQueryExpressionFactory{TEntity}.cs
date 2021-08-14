// <copyright file="IQueryExpressionFactory{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Linq
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Expressions factory interface. Allows getting the query expressions for the query object used as an argument.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity linked with the query specifications.</typeparam>
    public interface IQueryExpressionFactory<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets a query expressions for the query object.
        /// </summary>
        /// <typeparam name="TQueryObject">Type of query object.</typeparam>
        /// <param name="queryObject">A <see cref="TQueryObject"/> reference to the query object.</param>
        /// <returns>A <see cref="Expression{Func{TEntity, object}}"/> reference to the expression.</returns>
        Expression<Func<TEntity, bool>> GetSpec<TQueryObject>(TQueryObject queryObject);

        /// <summary>
        /// Gets a query sort expressions for the query object.
        /// </summary>
        /// <typeparam name="TQueryObject">Type of query object.</typeparam>
        /// <param name="queryObject">A <see cref="TQueryObject"/> reference to the query object.</param>
        /// <returns>A <see cref="Expression{Func{TEntity, object}}"/> reference to the sort expression.</returns>
        Expression<Func<TEntity, object>> GetSortSpec<TQueryObject>(TQueryObject queryObject);
    }
}
