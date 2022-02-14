// <copyright file="GetPageQueryObject.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Specs
{
    /// <summary>
    /// Represent the abstract class used as a base class to implement entity query objects in paginated listings.
    /// </summary>
    public abstract class GetPageQueryObject
    {
        /// <summary>
        /// Gets or sets the offset to be used in the query.
        /// </summary>
        public int Offset { get; protected set; }

        /// <summary>
        /// Gets or sets the size of page to be used in the query.
        /// </summary>
        public int Limit { get; protected set; }
    }
}
