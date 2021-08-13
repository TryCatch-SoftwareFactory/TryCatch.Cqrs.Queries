// <copyright file="GetEntityQueryHandler{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.GetEntity
{
    using System.Threading;
    using System.Threading.Tasks;
    using TryCatch.Exceptions;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;
    using TryCatch.Validators;

    /// <summary>
    /// Standard get entity query handler.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public abstract class GetEntityQueryHandler<TEntity> : IQueryHandler<QueryObjectBase<TEntity>, Result<TEntity>>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEntityQueryHandler{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">A <see cref="ILinqQueryRepository{TEntity}"/> reference to the repository.</param>
        /// <param name="builder">A <see cref="IResultBuilder{TEntity}"/> reference to the result builder.</param>
        protected GetEntityQueryHandler(ILinqQueryRepository<TEntity> repository, IResultBuilder<TEntity> builder)
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
        protected IResultBuilder<TEntity> Builder { get; }

        /// <inheritdoc/>
        public async virtual Task<Result<TEntity>> Execute(
            QueryObjectBase<TEntity> queryObject,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(queryObject, nameof(queryObject));

            var where = queryObject.GetQuery();

            var entity = await this.Repository
                .GetAsync(where, cancellationToken)
                .ConfigureAwait(false);

            if (entity is default(TEntity))
            {
                throw new EntityNotFoundException($"Not found entity with criterias: {where}");
            }

            return this.Builder
                .Build()
                .WithPayload(entity)
                .Create();
        }
    }
}
