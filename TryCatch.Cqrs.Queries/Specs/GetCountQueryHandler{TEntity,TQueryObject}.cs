// <copyright file="GetCountQueryHandler{TEntity,TQueryObject}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Specs
{
    using System.Threading;
    using System.Threading.Tasks;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;
    using TryCatch.Patterns.Specifications;
    using TryCatch.Validators;

    /// <summary>
    /// Hanlder for generic count query.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TQueryObject">Type of query object.</typeparam>
    public abstract class GetCountQueryHandler<TEntity, TQueryObject> : IQueryHandler<TQueryObject, Result<long>>
        where TEntity : class
        where TQueryObject : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCountQueryHandler{TEntity, TQueryObject}"/> class.
        /// </summary>
        /// <param name="repository">A <see cref="ISpecQueryRepository{TEntity}"/> reference to the repository.</param>
        /// <param name="factory">A <see cref="ISpecificationFactory{TEntity}"/> reference to the specs factory.</param>
        /// <param name="builder">A <see cref="IResultBuilder{long}"/> reference to the result builder.</param>
        protected GetCountQueryHandler(
            ISpecQueryRepository<TEntity> repository,
            ISpecificationFactory<TEntity> factory,
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
        protected ISpecQueryRepository<TEntity> Repository { get; }

        /// <summary>
        /// Gets the current specifications factory.
        /// </summary>
        protected ISpecificationFactory<TEntity> Factory { get; }

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

            var specs = this.Factory.GetSpecification(queryObject);

            var count = await this.Repository
                .GetCountAsync(specs, cancellationToken)
                .ConfigureAwait(false);

            return this.Builder
                .Build()
                .WithPayload(count)
                .Create();
        }
    }
}
