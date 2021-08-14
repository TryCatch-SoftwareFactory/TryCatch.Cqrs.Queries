// <copyright file="GetFlightsCountQueryHandler.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Specs
{
    using TryCatch.Cqrs.Queries.Specs;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;
    using TryCatch.Patterns.Specifications;

    public class GetFlightsCountQueryHandler : GetCountQueryHandler<Flight, GetFlightQueryObject>
    {
        public GetFlightsCountQueryHandler(
            ISpecQueryRepository<Flight> repository,
            ISpecificationFactory<Flight> factory,
            IResultBuilder<long> builder)
            : base(repository, factory, builder)
        {
        }
    }
}
