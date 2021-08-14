// <copyright file="FlightSpecFactory.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Specs
{
    using TryCatch.Patterns.Specifications;

    public class FlightSpecFactory : ISpecificationFactory<Flight>
    {
        public ISortSpecification<Flight> GetSortSpecification<TQueryObject>(TQueryObject filterObject) => new SortFlightsSpec();

        public ISpecification<Flight> GetSpecification<TQueryObject>(TQueryObject filterObject) => new FilterFlightsSpec();
    }
}
