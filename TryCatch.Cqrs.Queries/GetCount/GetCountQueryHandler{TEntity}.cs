// <copyright file="GetCountQueryHandler{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.GetCount
{
    using System.Threading;
    using System.Threading.Tasks;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;
    using TryCatch.Validators;

    /// <summary>
    /// Hanlder for generic count query.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public abstract class GetCountQueryHandler<TEntity> : IQueryHandler<QueryObjectBase<TEntity>, Result<long>>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCountQueryHandler{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">A <see cref="ILinqQueryRepository{TEntity}"/> reference to the repository.</param>
        /// <param name="builder">A <see cref="IResultBuilder{long}"/> reference to the result builder.</param>
        protected GetCountQueryHandler(ILinqQueryRepository<TEntity> repository, IResultBuilder<long> builder)
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
        protected IResultBuilder<long> Builder { get; }

        /// <inheritdoc/>
        public async virtual Task<Result<long>> Execute(
            QueryObjectBase<TEntity> queryObject,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(queryObject, nameof(queryObject));

            var where = queryObject.GetQuery();

            var count = await this.Repository
                .GetCountAsync(where, cancellationToken)
                .ConfigureAwait(false);

            return this.Builder
                .Build()
                .WithPayload(count)
                .Create();
        }
    }
}
