// <copyright file="FilterFlightsSpec.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Specs
{
    using TryCatch.Patterns.Specifications;
    using TryCatch.Patterns.Specifications.InMemory;

    public class FilterFlightsSpec : CompositeSpecification<Flight>, ISpecification<Flight>
    {
        public override bool IsSatisfiedBy(Flight candidate) => true;
    }
}
