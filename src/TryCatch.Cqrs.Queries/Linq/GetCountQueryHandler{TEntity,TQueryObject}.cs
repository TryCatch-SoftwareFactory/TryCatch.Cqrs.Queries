// <copyright file="GetCountQueryHandler{TEntity,TQueryObject}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Linq
{
    using System.Threading;
    using System.Threading.Tasks;
    using TryCatch.Patterns;
    using TryCatch.Patterns.Repositories.Linq;
    using TryCatch.Patterns.Results;
    using TryCatch.Validators;

    /// <summary>
    /// Hanlder for generic count query.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TQueryObject">Type of query object.</typeparam>
    public abstract class GetCountQueryHandler<TEntity, TQueryObject> : IQueryHandler<TQueryObject, Result<long>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCountQueryHandler{TEntity, TQueryObject}"/> class.
        /// </summary>
        /// <param name="repository">A <see cref="IReadingRepository{TEntity}"/> reference to the repository.</param>
        /// <param name="factory">A <see cref="IExpressionFactory{TEntity}"/> reference to the expression factory.</param>
        /// <param name="builder">A <see cref="IResultBuilder{long}"/> reference to the result builder.</param>
        protected GetCountQueryHandler(
            IReadingRepository<TEntity> repository,
            IExpressionFactory<TEntity> factory,
            IResultBuilder<long> builder)
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
        protected IReadingRepository<TEntity> Repository { get; }

        /// <summary>
        /// Gets the current expression factory.
        /// </summary>
        protected IExpressionFactory<TEntity> Factory { get; }

        /// <summary>
        /// Gets the current reference to the result builder.
        /// </summary>
        protected IResultBuilder<long> Builder { get; }

        /// <inheritdoc/>
        public async virtual Task<Result<long>> Execute(
            TQueryObject queryObject,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(queryObject, nameof(queryObject));

            var where = this.Factory.GetExpression(queryObject);

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
