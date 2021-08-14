// <copyright file="GetNextQueryHandler{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Specs
{
    using System.Threading;
    using System.Threading.Tasks;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Validators;

    /// <summary>
    /// Standard get next page query handler.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public class GetNextQueryHandler<TEntity> : IQueryHandler<GetPageQueryObject, GetNextResult<TEntity>>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetNextQueryHandler{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">A <see cref="ISpecQueryRepository{TEntity}"/> reference to the current repository.</param>
        /// <param name="factory">A <see cref="ISpecFactory{TEntity}"/> reference to the specs factory.</param>
        public GetNextQueryHandler(ISpecQueryRepository<TEntity> repository, ISpecFactory<TEntity> factory)
        {
            ArgumentsValidator.ThrowIfIsNull(repository, nameof(repository));
            ArgumentsValidator.ThrowIfIsNull(factory, nameof(factory));

            this.Repository = repository;
            this.Factory = factory;
        }

        /// <summary>
        /// Gets the current reference to the repository.
        /// </summary>
        public ISpecQueryRepository<TEntity> Repository { get; }

        /// <summary>
        /// Gets the current specifications factory.
        /// </summary>
        protected ISpecFactory<TEntity> Factory { get; }

        /// <inheritdoc/>
        public async Task<GetNextResult<TEntity>> Execute(
            GetPageQueryObject queryObject,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(queryObject, nameof(queryObject));

            var where = this.Factory.GetSpec(queryObject);
            var orderBy = this.Factory.GetSortSpec(queryObject);

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
