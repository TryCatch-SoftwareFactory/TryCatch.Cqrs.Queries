// <copyright file="GetPageQueryHandlerTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.GetPageQueryHandler
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NSubstitute;
    using TryCatch.Cqrs.Queries.UnitTests.Mocks;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;
    using Xunit;

    public class GetPageQueryHandlerTests
    {
        private readonly ILinqQueryRepository<Flight> repository;

        private readonly IPageResultBuilder<Flight> builder;

        private readonly GetFlightsPageQueryHandler sut;

        public GetPageQueryHandlerTests()
        {
            this.repository = Substitute.For<ILinqQueryRepository<Flight>>();
            this.builder = new PageResultBuilder<Flight>();
            this.sut = new GetFlightsPageQueryHandler(this.repository, this.builder);
        }

        [Fact]
        public void Construct_without_repository()
        {
            // Arrange
            ILinqQueryRepository<Flight> repository = null;

            // Act
            Action act = () => _ = new GetFlightsPageQueryHandler(repository, this.builder);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_without_builder()
        {
            // Arrange
            IPageResultBuilder<Flight> builder = null;

            // Act
            Action act = () => _ = new GetFlightsPageQueryHandler(this.repository, builder);

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
                Arg.Any<Expression<Func<Flight, bool>>>(),
                Arg.Any<Expression<Func<Flight, object>>>(),
                Arg.Any<bool>(),
                Arg.Any<CancellationToken>())
                .Returns(expected);

            this.repository
                .GetCountAsync(null, Arg.Any<CancellationToken>())
                .Returns(count);

            this.repository
                .GetCountAsync(
                    Arg.Is(queryObject.GetQuery()),
                    Arg.Any<CancellationToken>())
                .Returns(count);

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
