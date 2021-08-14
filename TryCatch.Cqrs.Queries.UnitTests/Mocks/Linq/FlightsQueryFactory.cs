// <copyright file="FlightsQueryFactory.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Mocks.Linq
{
    using System;
    using System.Linq.Expressions;

    public class FlightsQueryFactory : IFlightsQueryFactory
    {
        public Expression<Func<Flight, object>> GetSortSpec<TQueryObject>(TQueryObject queryObject)
        {
            Expression<Func<Flight, object>> sortAs = x => x.Reference;

            if (queryObject is GetFlightsCountQueryObject op1)
            {
                sortAs = x => x.Reference;
            }
            else if (queryObject is GetFlightQueryObject op2)
            {
                sortAs = x => x.Reference;
            }
            else if (queryObject is GetFlightsPageQueryObject op3)
            {
                sortAs = x => x.Reference;
            }

            return sortAs;
        }

        public Expression<Func<Flight, bool>> GetSpec<TQueryObject>(TQueryObject queryObject)
        {
            Expression<Func<Flight, bool>> where = x => true;

            if (queryObject is GetFlightsCountQueryObject op1)
            {
                where = x => x.Reference.Contains(op1.Reference);
            }
            else if (queryObject is GetFlightQueryObject op2)
            {
                where = x => x.Reference.Contains(op2.Reference);
            }
            else if (queryObject is GetFlightsPageQueryObject op3)
            {
                where = x => x.Reference.Contains(op3.Reference);
            }

            return where;
        }
    }
}
