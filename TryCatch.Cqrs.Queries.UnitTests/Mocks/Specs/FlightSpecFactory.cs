// <copyright file="FlightSpecFactory.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Specs
{
    using TryCatch.Cqrs.Queries.Specs;
    using TryCatch.Patterns.Specifications;

    public class FlightSpecFactory : ISpecFactory<Flight>
    {
        public ISortSpecification<Flight> GetSortSpec<TQueryObject>(TQueryObject queryObject) => new SortFlightsSpec();

        public ISpecification<Flight> GetSpec<TQueryObject>(TQueryObject queryObject) => new FilterFlightsSpec();
    }
}
