// <copyright file="GetFlightQueryObject.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Specs
{
    public class GetFlightQueryObject
    {
        public GetFlightQueryObject(string reference)
        {
            this.Reference = string.IsNullOrWhiteSpace(reference) ? string.Empty : reference;
        }

        public string Reference { get; }
    }
}
