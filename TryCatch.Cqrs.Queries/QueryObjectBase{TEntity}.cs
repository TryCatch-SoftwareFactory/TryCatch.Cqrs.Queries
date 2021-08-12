// <copyright file="QueryObjectBase{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// GetCount query object.
    /// </summary>
    /// <typeparam name="TEntity">Type of main entity.</typeparam>
    public abstract class QueryObjectBase<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets query filter expression.
        /// </summary>
        /// <returns>A reference to the filter.</returns>
        public abstract Expression<Func<TEntity, bool>> GetQuery();
    }
}
