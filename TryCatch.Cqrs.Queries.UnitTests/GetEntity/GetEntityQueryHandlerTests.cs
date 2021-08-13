﻿// <copyright file="GetEntityQueryHandlerTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.GetEntity
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NSubstitute;
    using TryCatch.Cqrs.Queries.UnitTests.Mocks;
    using TryCatch.Exceptions;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;
    using Xunit;

    public class GetEntityQueryHandlerTests
    {
        private readonly ILinqQueryRepository<Flight> repository;

        private readonly IResultBuilder<Flight> builder;

        private readonly GetFlightQueryHandler sut;

        public GetEntityQueryHandlerTests()
        {
            this.repository = Substitute.For<ILinqQueryRepository<Flight>>();
            this.builder = new ResultBuilder<Flight>();
            this.sut = new GetFlightQueryHandler(this.repository, this.builder);
        }

        [Fact]
        public void Construct_without_repository()
        {
            // Arrange
            ILinqQueryRepository<Flight> repository = null;

            // Act
            Action act = () => _ = new GetFlightQueryHandler(repository, this.builder);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_without_builder()
        {
            // Arrange
            IResultBuilder<Flight> builder = null;

            // Act
            Action act = () => _ = new GetFlightQueryHandler(this.repository, builder);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Execute_without_queryObject()
        {
            // Arrange
            GetFlightQueryObject queryObject = null;

            // Act
            Func<Task> actual = async () => await this.sut.Execute(queryObject).ConfigureAwait(false);

            // Asserts
            await actual.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [Fact]
        public async Task Execute_with_not_found_entity()
        {
            // Arrange
            var expected = default(Flight);
            var queryObject = new GetFlightQueryObject("some-reference");
            this.repository.GetAsync(
                Arg.Any<Expression<Func<Flight, bool>>>(),
                Arg.Any<CancellationToken>())
                .Returns(expected);

            // Act
            Func<Task> actual = async () => _ = await this.sut.Execute(queryObject).ConfigureAwait(false);

            // Asserts
            await actual.Should().ThrowAsync<EntityNotFoundException>().ConfigureAwait(false);
        }

        [Fact]
        public async Task Execute_ok()
        {
            // Arrange
            var expected = new Flight();
            var queryObject = new GetFlightQueryObject("some-reference");
            this.repository.GetAsync(
                Arg.Any<Expression<Func<Flight, bool>>>(),
                Arg.Any<CancellationToken>())
                .Returns(expected);

            // Act
            var actual = await this.sut.Execute(queryObject).ConfigureAwait(false);

            // Asserts
            actual.Should().NotBeNull();
            actual.Payload.Should().BeEquivalentTo(expected);
            actual.IsSucceeded.Should().BeTrue();
            actual.Errors.Should().BeEmpty();
        }
    }
}
