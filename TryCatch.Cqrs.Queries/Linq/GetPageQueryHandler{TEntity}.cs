﻿// <copyright file="GetPageQueryHandler{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Linq
{
    using System.Threading;
    using System.Threading.Tasks;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;
    using TryCatch.Validators;

    /// <summary>
    /// Standard Get page query handler.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public abstract class GetPageQueryHandler<TEntity> : IQueryHandler<GetPageQueryObject<TEntity>, PageResult<TEntity>>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPageQueryHandler{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">A <see cref="ILinqQueryRepository{TEntity}"/> reference to the repository.</param>
        /// <param name="builder">A <see cref="IPageResultBuilder{TEntity}"/> reference to the result builder.</param>
        protected GetPageQueryHandler(ILinqQueryRepository<TEntity> repository, IPageResultBuilder<TEntity> builder)
        {
            ArgumentsValidator.ThrowIfIsNull(repository, nameof(repository));
            ArgumentsValidator.ThrowIfIsNull(builder, nameof(builder));
            this.Repository = repository;
            this.Builder = builder;
        }

        /// <summary>
        /// Gets the current reference to the repository.
        /// </summary>
        protected ILinqQueryRepository<TEntity> Repository { get; }

        /// <summary>
        /// Gets the current reference to the result builder.
        /// </summary>
        protected IPageResultBuilder<TEntity> Builder { get; }

        /// <inheritdoc/>
        public async Task<PageResult<TEntity>> Execute(
            GetPageQueryObject<TEntity> queryObject,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(queryObject, nameof(queryObject));

            var where = queryObject.GetQuery();
            var orderBy = queryObject.GetOrderBy();

            var countTask = this.Repository.GetCountAsync(cancellationToken: cancellationToken);
            var matchedTask = this.Repository.GetCountAsync(where, cancellationToken);
            var listTask = this.Repository.GetPageAsync(
                    offset: queryObject.Offset,
                    limit: queryObject.Limit,
                    where: where,
                    orderBy: orderBy,
                    orderAsAscending: queryObject.SortAsAscending(),
                    cancellationToken: cancellationToken);

            await Task.WhenAll(countTask, matchedTask, listTask).ConfigureAwait(false);

            return this.Builder.Build()
                .WithCount(await countTask.ConfigureAwait(false))
                .WithMatched(await matchedTask.ConfigureAwait(false))
                .WithItems(await listTask.ConfigureAwait(false))
                .WithOffset(queryObject.Offset)
                .WithLimit(queryObject.Limit)
                .Create();
        }
    }
}
