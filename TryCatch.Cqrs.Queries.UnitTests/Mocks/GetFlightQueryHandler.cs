﻿// <copyright file="GetFlightQueryHandler.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks
{
    using TryCatch.Cqrs.Queries.GetEntity;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;

    public class GetFlightQueryHandler : GetEntityQueryHandler<Flight>
    {
        public GetFlightQueryHandler(ILinqQueryRepository<Flight> repository, IResultBuilder<Flight> builder)
            : base(repository, builder)
        {
        }
    }
}
