// <copyright file="ISpecFactory.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Specs
{
    using TryCatch.Patterns.Specifications;

    public interface ISpecFactory<TEntity>
        where TEntity : class
    {
        ISpecification<TEntity> GetSpec<TQueryObject>(TQueryObject queryObject);

        ISortSpecification<TEntity> GetSortSpec<TQueryObject>(TQueryObject queryObject);
    }
}
