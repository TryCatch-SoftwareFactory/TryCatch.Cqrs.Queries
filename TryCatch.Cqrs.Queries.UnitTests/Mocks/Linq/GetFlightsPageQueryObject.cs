// <copyright file="GetFlightsPageQueryObject.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Linq
{
    using System;
    using System.Linq.Expressions;

    public class GetFlightsPageQueryObject : GetPageQueryObject<Flight>
    {
        public GetFlightsPageQueryObject(int offset, int limit)
        {
            this.Limit = limit;
            this.Offset = offset;
        }

        public override Expression<Func<Flight, object>> GetOrderBy() => (x) => x.Reference;

        public override Expression<Func<Flight, bool>> GetQuery() => (x) => true;

        public override bool SortAsAscending() => true;
    }
}
