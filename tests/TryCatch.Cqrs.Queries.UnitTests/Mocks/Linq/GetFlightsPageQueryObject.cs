// <copyright file="GetFlightsPageQueryObject.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Linq
{
    using TryCatch.Cqrs.Queries.Linq;

    public class GetFlightsPageQueryObject : GetPageQueryObject
    {
        public GetFlightsPageQueryObject(int offset, int limit, string reference = "")
            : base(offset, limit)
        {
            this.Reference = reference;
        }

        public string Reference { get; }

        public override bool SortAsAscending() => true;
    }
}
