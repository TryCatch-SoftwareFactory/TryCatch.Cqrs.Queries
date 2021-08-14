// <copyright file="GetEntityQueryHandler{TEntity,TQueryObject}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Specs
{
    using System.Threading;
    using System.Threading.Tasks;
    using TryCatch.Exceptions;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;
    using TryCatch.Patterns.Specifications;
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
        /// <param name="repository">A <see cref="ISpecQueryRepository{TEntity}"/> reference to the repository.</param>
        /// <param name="factory">A <see cref="ISpecificationFactory{TEntity}"/> reference to the specs factory.</param>
        /// <param name="builder">A <see cref="IResultBuilder{TEntity}"/> reference to the result builder.</param>
        protected GetEntityQueryHandler(
            ISpecQueryRepository<TEntity> repository,
            ISpecificationFactory<TEntity> factory,
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
        protected ISpecQueryRepository<TEntity> Repository { get; }

        /// <summary>
        /// Gets the current specifications factory.
        /// </summary>
        protected ISpecificationFactory<TEntity> Factory { get; }

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

            var specs = this.Factory.GetSpecification(queryObject);

            var entity = await this.Repository
                .GetAsync(specs, cancellationToken)
                .ConfigureAwait(false);

            if (entity is default(TEntity))
            {
                throw new EntityNotFoundException($"Not found entity with criterias: {queryObject.GetType().Name}");
            }

            return this.Builder
                .Build()
                .WithPayload(entity)
                .Create();
        }
    }
}
