// <copyright file="GetPageQueryHandler{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Specs
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
    public abstract class GetPageQueryHandler<TEntity> : IQueryHandler<GetPageQueryObject, PageResult<TEntity>>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPageQueryHandler{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">A <see cref="ISpecQueryRepository{TEntity}"/> reference to the repository.</param>
        /// <param name="factory">A <see cref="ISpecFactory{TEntity}"/> reference to the specs factory.</param>
        /// <param name="builder">A <see cref="IPageResultBuilder{TEntity}"/> reference to the result builder.</param>
        protected GetPageQueryHandler(
            ISpecQueryRepository<TEntity> repository,
            ISpecFactory<TEntity> factory,
            IPageResultBuilder<TEntity> builder)
        {
            ArgumentsValidator.ThrowIfIsNull(repository, nameof(repository));
            ArgumentsValidator.ThrowIfIsNull(factory, nameof(factory));
            ArgumentsValidator.ThrowIfIsNull(builder, nameof(builder));

            this.Repository = repository;
            this.Factory = factory;
            this.Builder = builder;
        }

        /// <summary>
        /// Gets the current reference to the repository.
        /// </summary>
        protected ISpecQueryRepository<TEntity> Repository { get; }

        /// <summary>
        /// Gets the current specifications factory.
        /// </summary>
        protected ISpecFactory<TEntity> Factory { get; }

        /// <summary>
        /// Gets the current reference to the result builder.
        /// </summary>
        protected IPageResultBuilder<TEntity> Builder { get; }

        /// <inheritdoc/>
        public async Task<PageResult<TEntity>> Execute(
            GetPageQueryObject queryObject,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(queryObject, nameof(queryObject));

            var where = this.Factory.GetSpec(queryObject);
            var orderBy = this.Factory.GetSortSpec(queryObject);

            var countTask = this.Repository.GetCountAsync(cancellationToken: cancellationToken);
            var matchedTask = this.Repository.GetCountAsync(where, cancellationToken);
            var listTask = this.Repository.GetPageAsync(
                    offset: queryObject.Offset,
                    limit: queryObject.Limit,
                    where: where,
                    orderBy: orderBy,
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
