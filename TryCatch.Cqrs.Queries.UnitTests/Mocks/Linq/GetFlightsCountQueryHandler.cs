// <copyright file="GetFlightsCountQueryHandler.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Linq
{
    using TryCatch.Cqrs.Queries.Linq;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;

    public class GetFlightsCountQueryHandler : GetCountQueryHandler<Flight, GetFlightsCountQueryObject>
    {
        public GetFlightsCountQueryHandler(
            ILinqQueryRepository<Flight> repository,
            IFlightsQueryFactory factory,
            IResultBuilder<long> builder)
            : base(repository, factory, builder)
        {
        }
    }
}
