// <copyright file="GetCountQueryHandlerTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Specs
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NSubstitute;
    using TryCatch.Cqrs.Queries.UnitTests.Mocks;
    using TryCatch.Cqrs.Queries.UnitTests.Mocks.Specs;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;
    using TryCatch.Patterns.Specifications;
    using Xunit;

    public class GetCountQueryHandlerTests
    {
        private readonly ISpecQueryRepository<Flight> repository;

        private readonly ISpecificationFactory<Flight> factory;

        private readonly IResultBuilder<long> builder;

        private readonly IGetFlightsCountQueryHandler sut;

        public GetCountQueryHandlerTests()
        {
            this.factory = new FlightSpecFactory();
            this.repository = Substitute.For<ISpecQueryRepository<Flight>>();
            this.builder = new ResultBuilder<long>();
            this.sut = new GetFlightsCountQueryHandler(this.repository, this.factory, this.builder);
        }

        [Fact]
        public void Construct_without_repository()
        {
            // Arrange
            ISpecQueryRepository<Flight> repository = null;

            // Act
            Action act = () => _ = new GetFlightsCountQueryHandler(repository, this.factory, this.builder);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_without_factory()
        {
            // Arrange
            ISpecificationFactory<Flight> factory = null;

            // Act
            Action act = () => _ = new GetFlightsCountQueryHandler(this.repository, factory, this.builder);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_without_builder()
        {
            // Arrange
            IResultBuilder<long> builder = null;

            // Act
            Action act = () => _ = new GetFlightsCountQueryHandler(this.repository, this.factory, builder);

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
        public async Task Execute_ok()
        {
            // Arrange
            var expected = 10L;
            var queryObject = new GetFlightQueryObject("some-reference");
            this.repository.GetCountAsync(
                Arg.Any<ISpecification<Flight>>(),
                Arg.Any<CancellationToken>())
                .Returns(expected);

            // Act
            var actual = await this.sut.Execute(queryObject).ConfigureAwait(false);

            // Asserts
            actual.Should().NotBeNull();
            actual.Payload.Should().Be(expected);
            actual.IsSucceeded.Should().BeTrue();
            actual.Errors.Should().BeEmpty();
        }
    }
}
