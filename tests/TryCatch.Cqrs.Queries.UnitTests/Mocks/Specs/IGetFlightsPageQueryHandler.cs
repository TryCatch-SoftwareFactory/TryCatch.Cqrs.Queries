// <copyright file="IGetFlightsPageQueryHandler.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Specs
{
    using TryCatch.Patterns.Results;

    public interface IGetFlightsPageQueryHandler : IQueryHandler<GetFlightsPageQueryObject, PageResult<Flight>>
    {
    }
}
