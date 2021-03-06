// <copyright file="GetNextQueryHandler{TEntity,TQueryObject}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Linq
{
    using System.Threading;
    using System.Threading.Tasks;
    using TryCatch.Patterns;
    using TryCatch.Patterns.Repositories.Linq;
    using TryCatch.Validators;

    /// <summary>
    /// Standard get next page query handler.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TQueryObject">Type of query object.</typeparam>
    public class GetNextQueryHandler<TEntity, TQueryObject> : IQueryHandler<TQueryObject, GetNextResult<TEntity>>
        where TEntity : class
        where TQueryObject : GetPageQueryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetNextQueryHandler{TEntity,TQueryObject}"/> class.
        /// </summary>
        /// <param name="repository">A <see cref="IReadingRepository{TEntity}"/> reference to the current repository.</param>
        /// <param name="factory">A <see cref="IExpressionFactory{TEntity}"/> reference to the expression factory.</param>
        public GetNextQueryHandler(IReadingRepository<TEntity> repository, IExpressionFactory<TEntity> factory)
        {
            ArgumentsValidator.ThrowIfIsNull(repository, nameof(repository));
            ArgumentsValidator.ThrowIfIsNull(factory, nameof(factory));

            this.Repository = repository;
            this.Factory = factory;
        }

        /// <summary>
        /// Gets the current reference to the repository.
        /// </summary>
        protected IReadingRepository<TEntity> Repository { get; }

        /// <summary>
        /// Gets the current expression factory.
        /// </summary>
        protected IExpressionFactory<TEntity> Factory { get; }

        /// <inheritdoc/>
        public async Task<GetNextResult<TEntity>> Execute(
            TQueryObject queryObject,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(queryObject, nameof(queryObject));

            var where = this.Factory.GetExpression(queryObject);
            var orderBy = this.Factory.GetSortExpression(queryObject);

            var items = await this.Repository
                .GetPageAsync(
                    offset: queryObject.Offset,
                    limit: queryObject.Limit,
                    where: where,
                    orderBy: orderBy,
                    orderAsAscending: queryObject.SortAsAscending(),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return new GetNextResult<TEntity>(
                items: items,
                offset: queryObject.Offset,
                limit: queryObject.Limit);
        }
    }
}
