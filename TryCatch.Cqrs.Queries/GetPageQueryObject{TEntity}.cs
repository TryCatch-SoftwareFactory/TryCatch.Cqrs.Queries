// <copyright file="GetPageQueryObject{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represent the abstract class - based on Linq Expressions - used as a base class to implement entity query objects in paginated listings.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity on which it will consult.</typeparam>
    public abstract class GetPageQueryObject<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets the offset to be used in the query.
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// Gets the size of page to be used in the query.
        /// </summary>
        public int Limit { get; }

        /// <summary>
        /// Gets a value indicating whether the query must be ordered as ascending.
        /// </summary>
        /// <returns>True if must be ascending.</returns>
        public abstract bool SortAsAscending();

        /// <summary>
        /// Gets orderBy expression.
        /// </summary>
        /// <returns>A order by expression.</returns>
        public abstract Expression<Func<TEntity, object>> GetOrderBy();

        /// <summary>
        /// Gets query criteria expression.
        /// </summary>
        /// <returns>A query criteria expression.</returns>
        public abstract Expression<Func<TEntity, bool>> GetQuery();
    }
}
