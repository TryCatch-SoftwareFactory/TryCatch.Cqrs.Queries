// <copyright file="ISpecFactory{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Specs
{
    using TryCatch.Patterns.Specifications;

    /// <summary>
    /// Specification factory interface. Allows getting the query specs for the query object used as an argument.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity linked with the query specifications.</typeparam>
    public interface ISpecFactory<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets a query specification for the query object.
        /// </summary>
        /// <typeparam name="TQueryObject">Type of query object.</typeparam>
        /// <param name="queryObject">A <see cref="TQueryObject"/> reference to the query object.</param>
        /// <returns>A <see cref="ISpecification{TEntity}"/> reference to the specification.</returns>
        ISpecification<TEntity> GetSpec<TQueryObject>(TQueryObject queryObject);

        /// <summary>
        /// Gets a query sort specification for the query object.
        /// </summary>
        /// <typeparam name="TQueryObject">Type of query object.</typeparam>
        /// <param name="queryObject">A <see cref="TQueryObject"/> reference to the query object.</param>
        /// <returns>A <see cref="ISortSpecification{TEntity}"/> reference to the sort specification.</returns>
        ISortSpecification<TEntity> GetSortSpec<TQueryObject>(TQueryObject queryObject);
    }
}
