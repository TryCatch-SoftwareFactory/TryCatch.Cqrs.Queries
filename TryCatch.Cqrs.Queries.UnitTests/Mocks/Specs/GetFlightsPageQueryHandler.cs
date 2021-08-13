// <copyright file="GetFlightsPageQueryHandler.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Specs
{
    using TryCatch.Cqrs.Queries.Specs;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;

    public class GetFlightsPageQueryHandler : GetPageQueryHandler<Flight>
    {
        public GetFlightsPageQueryHandler(
            ISpecQueryRepository<Flight> repository,
            ISpecFactory<Flight> factory,
            IPageResultBuilder<Flight> builder)
            : base(repository, factory, builder)
        {
        }
    }
}
