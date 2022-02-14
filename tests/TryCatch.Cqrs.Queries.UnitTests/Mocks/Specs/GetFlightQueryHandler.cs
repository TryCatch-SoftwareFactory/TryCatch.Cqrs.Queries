// <copyright file="GetFlightQueryHandler.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Specs
{
    using TryCatch.Cqrs.Queries.Specs;
    using TryCatch.Patterns.Repositories.Spec;
    using TryCatch.Patterns.Results;
    using TryCatch.Patterns.Specifications;

    public class GetFlightQueryHandler : GetEntityQueryHandler<Flight, GetFlightQueryObject>, IGetFlightQueryHandler
    {
        public GetFlightQueryHandler(
            IReadingRepository<Flight> repository,
            ISpecificationFactory<Flight> factory,
            IResultBuilder<Flight> builder)
            : base(repository, factory, builder)
        {
        }
    }
}
