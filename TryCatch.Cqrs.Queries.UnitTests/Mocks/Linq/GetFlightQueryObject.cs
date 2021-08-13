// <copyright file="GetFlightQueryObject.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Linq
{
    using System;
    using System.Linq.Expressions;

    public class GetFlightQueryObject : QueryObjectBase<Flight>
    {
        private readonly string reference;

        public GetFlightQueryObject(string reference)
        {
            this.reference = string.IsNullOrWhiteSpace(reference) ? string.Empty : reference;
        }

        public override Expression<Func<Flight, bool>> GetQuery() => (x) => x.Reference.Contains(this.reference);
    }
}
