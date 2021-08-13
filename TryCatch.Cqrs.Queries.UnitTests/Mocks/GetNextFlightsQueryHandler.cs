// <copyright file="GetNextFlightsQueryHandler.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks
{
    using TryCatch.Cqrs.Queries.GetNext;
    using TryCatch.Patterns.Repositories;

    public class GetNextFlightsQueryHandler : GetNextQueryHandler<Flight>
    {
        public GetNextFlightsQueryHandler(ILinqQueryRepository<Flight> repository)
            : base(repository)
        {
        }
    }
}
