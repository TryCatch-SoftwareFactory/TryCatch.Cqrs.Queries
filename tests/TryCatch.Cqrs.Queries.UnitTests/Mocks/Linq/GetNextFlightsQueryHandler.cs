// <copyright file="GetNextFlightsQueryHandler.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Linq
{
    using TryCatch.Cqrs.Queries.Linq;
    using TryCatch.Patterns.Repositories.Linq;

    public class GetNextFlightsQueryHandler : GetNextQueryHandler<Flight, GetFlightsPageQueryObject>, IGetNextFlightsQueryHandler
    {
        public GetNextFlightsQueryHandler(IReadingRepository<Flight> repository, IFlightsQueryFactory factory)
            : base(repository, factory)
        {
        }
    }
}
