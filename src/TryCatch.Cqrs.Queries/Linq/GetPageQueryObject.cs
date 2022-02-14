// <copyright file="GetPageQueryObject.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Linq
{
    /// <summary>
    /// Represent the abstract class used as a base class to implement entity query objects in paginated listings.
    /// </summary>
    public abstract class GetPageQueryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPageQueryObject"/> class.
        /// </summary>
        /// <param name="offset">The offset tobe used in the query.</param>
        /// <param name="limit">The size of page to be used in the query.</param>
        protected GetPageQueryObject(int offset, int limit)
        {
            this.Offset = offset;
            this.Limit = limit;
        }

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
    }
}
