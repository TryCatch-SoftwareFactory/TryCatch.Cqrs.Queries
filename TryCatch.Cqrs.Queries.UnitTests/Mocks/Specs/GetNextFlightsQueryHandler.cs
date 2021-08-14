// <copyright file="GetNextFlightsQueryHandler.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Specs
{
    using TryCatch.Cqrs.Queries.Specs;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Specifications;

    public class GetNextFlightsQueryHandler : GetNextQueryHandler<Flight>
    {
        public GetNextFlightsQueryHandler(ISpecQueryRepository<Flight> repository, ISpecificationFactory<Flight> factory)
            : base(repository, factory)
        {
        }
    }
}
