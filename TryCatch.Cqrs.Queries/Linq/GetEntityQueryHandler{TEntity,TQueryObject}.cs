// <copyright file="GetEntityQueryHandler{TEntity,TQueryObject}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Linq
{
    using System.Threading;
    using System.Threading.Tasks;
    using TryCatch.Exceptions;
    using TryCatch.Patterns;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;
    using TryCatch.Validators;

    /// <summary>
    /// Standard get entity query handler.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TQueryObject">Type of query object.</typeparam>
    public abstract class GetEntityQueryHandler<TEntity, TQueryObject> : IQueryHandler<TQueryObject, Result<TEntity>>
        where TEntity : class
        where TQueryObject : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEntityQueryHandler{TEntity,TQueryObject}"/> class.
        /// </summary>
        /// <param name="repository">A <see cref="ILinqQueryRepository{TEntity}"/> reference to the repository.</param>
        /// <param name="factory">A <see cref="IExpressionFactory{TEntity}"/> reference to the expression factory.</param>
        /// <param name="builder">A <see cref="IResultBuilder{TEntity}"/> reference to the result builder.</param>
        protected GetEntityQueryHandler(
            ILinqQueryRepository<TEntity> repository,
            IExpressionFactory<TEntity> factory,
            IResultBuilder<TEntity> builder)
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
        protected ILinqQueryRepository<TEntity> Repository { get; }

        /// <summary>
        /// Gets the current expression factory.
        /// </summary>
        protected IExpressionFactory<TEntity> Factory { get; }

        /// <summary>
        /// Gets the current reference to the result builder.
        /// </summary>
        protected IResultBuilder<TEntity> Builder { get; }

        /// <inheritdoc/>
        public async virtual Task<Result<TEntity>> Execute(
            TQueryObject queryObject,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(queryObject, nameof(queryObject));

            var where = this.Factory.GetExpression(queryObject);

            var entity = await this.Repository
                .GetAsync(where, cancellationToken)
                .ConfigureAwait(false);

            if (entity is default(TEntity))
            {
                throw new EntityNotFoundException($"Not found entity with criterias: {typeof(TQueryObject).Name}");
            }

            return this.Builder
                .Build()
                .WithPayload(entity)
                .Create();
        }
    }
}
