// <copyright file="GetNextQueryHandler{TEntity,TQueryObject}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Specs
{
    using System.Threading;
    using System.Threading.Tasks;
    using TryCatch.Patterns.Repositories.Spec;
    using TryCatch.Patterns.Specifications;
    using TryCatch.Validators;

    /// <summary>
    /// Standard get next page query handler.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TQueryObject">Type of query object.</typeparam>
    public class GetNextQueryHandler<TEntity, TQueryObject> : IQueryHandler<TQueryObject, GetNextResult<TEntity>>
        where TQueryObject : GetPageQueryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetNextQueryHandler{TEntity,TQueryObject}"/> class.
        /// </summary>
        /// <param name="repository">A <see cref="IReadingRepository{TEntity}"/> reference to the current repository.</param>
        /// <param name="factory">A <see cref="ISpecificationFactory{TEntity}"/> reference to the specs factory.</param>
        public GetNextQueryHandler(IReadingRepository<TEntity> repository, ISpecificationFactory<TEntity> factory)
        {
            ArgumentsValidator.ThrowIfIsNull(repository, nameof(repository));
            ArgumentsValidator.ThrowIfIsNull(factory, nameof(factory));

            this.Repository = repository;
            this.Factory = factory;
        }

        /// <summary>
        /// Gets the current reference to the repository.
        /// </summary>
        public IReadingRepository<TEntity> Repository { get; }

        /// <summary>
        /// Gets the current specifications factory.
        /// </summary>
        protected ISpecificationFactory<TEntity> Factory { get; }

        /// <inheritdoc/>
        public async Task<GetNextResult<TEntity>> Execute(
            TQueryObject queryObject,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(queryObject, nameof(queryObject));

            var where = this.Factory.GetSpecification(queryObject);
            var orderBy = this.Factory.GetSortSpecification(queryObject);

            var items = await this.Repository
                .GetPageAsync(
                    offset: queryObject.Offset,
                    limit: queryObject.Limit,
                    where: where,
                    orderBy: orderBy,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return new GetNextResult<TEntity>(
                items: items,
                offset: queryObject.Offset,
                limit: queryObject.Limit);
        }
    }
}
