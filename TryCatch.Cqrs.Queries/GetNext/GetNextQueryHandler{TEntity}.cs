// <copyright file="GetNextQueryHandler{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.GetNext
{
    using System.Threading;
    using System.Threading.Tasks;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Validators;

    /// <summary>
    /// Standard get next page query handler.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public class GetNextQueryHandler<TEntity> : IQueryHandler<GetPageQueryObject<TEntity>, GetNextResult<TEntity>>
        where TEntity : class
    {
        private readonly ILinqQueryRepository<TEntity> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNextQueryHandler{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">Query repository reference. See more in <see cref="IQueryRepository"/>.</param>
        public GetNextQueryHandler(ILinqQueryRepository<TEntity> repository)
        {
            ArgumentsValidator.ThrowIfIsNull(repository, nameof(repository));

            this.repository = repository;
        }

        /// <inheritdoc/>
        public async Task<GetNextResult<TEntity>> Execute(
            GetPageQueryObject<TEntity> queryObject,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(queryObject, nameof(queryObject));

            var where = queryObject.GetQuery();
            var orderBy = queryObject.GetOrderBy();

            var items = await this.repository
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
