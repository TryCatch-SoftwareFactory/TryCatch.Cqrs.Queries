// <copyright file="SortFlightsSpec.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Specs
{
    using System;
    using System.Linq.Expressions;
    using TryCatch.Patterns.Specifications;

    public class SortFlightsSpec : ISortSpecification<Flight>
    {
        public Expression<Func<Flight, object>> AsExpression() => x => x.Reference;

        public bool IsAscending() => true;
    }
}
