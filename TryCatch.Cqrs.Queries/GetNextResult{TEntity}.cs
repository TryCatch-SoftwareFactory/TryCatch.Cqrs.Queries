// <copyright file="GetNextResult{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries
{
    using System.Collections.Generic;
    using TryCatch.Validators;

    /// <summary>
    /// Represents the result of GetNext query execution.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public class GetNextResult<TEntity>
        where TEntity : class
    {
        public GetNextResult()
        {
            this.Items = new HashSet<TEntity>();
            this.Offset = 0;
            this.Limit = 0;
        }

        public GetNextResult(IEnumerable<TEntity> items, int offset, int limit)
        {
            ArgumentsValidator.ThrowIfIsNull(items, nameof(items));
            ArgumentsValidator.ThrowIfIsLessThan(1, offset);
            ArgumentsValidator.ThrowIfIsLessThan(1, limit);

            this.Items = items;
            this.Offset = offset;
            this.Limit = limit;
        }

        /// <summary>
        /// Gets the collection of matched items.
        /// </summary>
        public IEnumerable<TEntity> Items { get; }

        /// <summary>
        /// Gets the offset to be used in the query.
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// Gets the size of page to be used in the query.
        /// </summary>
        public int Limit { get; }
    }
}
