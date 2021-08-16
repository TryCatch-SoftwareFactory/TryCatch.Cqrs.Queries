// <copyright file="GetPageQueryHandlerTests.cs" company="TryCatch Software Factory">
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

    public class GetPageQueryHandlerTests
    {
        private readonly ISpecQueryRepository<Flight> repository;

        private readonly ISpecificationFactory<Flight> factory;

        private readonly IPageResultBuilder<Flight> builder;

        private readonly IGetFlightsPageQueryHandler sut;

        public GetPageQueryHandlerTests()
        {
            this.factory = new FlightSpecFactory();
            this.repository = Substitute.For<ISpecQueryRepository<Flight>>();
            this.builder = new PageResultBuilder<Flight>();
            this.sut = new GetFlightsPageQueryHandler(this.repository, this.factory, this.builder);
        }

        [Fact]
        public void Construct_without_repository()
        {
            // Arrange
            ISpecQueryRepository<Flight> repository = null;

            // Act
            Action act = () => _ = new GetFlightsPageQueryHandler(repository, this.factory, this.builder);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_without_factory()
        {
            // Arrange
            ISpecificationFactory<Flight> factory = null;

            // Act
            Action act = () => _ = new GetFlightsPageQueryHandler(this.repository, factory, this.builder);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_without_builder()
        {
            // Arrange
            IPageResultBuilder<Flight> builder = null;

            // Act
            Action act = () => _ = new GetFlightsPageQueryHandler(this.repository, this.factory, builder);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Execute_without_queryObject()
        {
            // Arrange
            GetFlightsPageQueryObject queryObject = null;

            // Act
            Func<Task> actual = async () => await this.sut.Execute(queryObject).ConfigureAwait(false);

            // Asserts
            await actual.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [Fact]
        public async Task Execute_ok()
        {
            // Arrange
            var offset = 1;
            var limit = 40;
            var matched = 0;
            var count = 1000;
            var queryObject = new GetFlightsPageQueryObject(offset, limit);
            var expected = Array.Empty<Flight>();

            this.repository.GetPageAsync(
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<ISpecification<Flight>>(),
                Arg.Any<ISortSpecification<Flight>>(),
                Arg.Any<CancellationToken>())
                .Returns(expected);

            this.repository
                .GetCountAsync(null, Arg.Any<CancellationToken>())
                .Returns(count);

            this.repository
                .GetCountAsync(
                    this.factory.GetSpecification(queryObject),
                    Arg.Any<CancellationToken>())
                .Returns(matched);

            // Act
            var actual = await this.sut.Execute(queryObject).ConfigureAwait(false);

            // Asserts
            actual.Should().NotBeNull();
            actual.Items.Should().BeEquivalentTo(expected);
            actual.Matched.Should().Be(matched);
            actual.Count.Should().Be(count);
            actual.Offset.Should().Be(offset);
            actual.Limit.Should().Be(limit);
        }
    }
}
