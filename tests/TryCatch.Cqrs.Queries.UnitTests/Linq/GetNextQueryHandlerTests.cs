// <copyright file="GetNextQueryHandlerTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.Linq
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NSubstitute;
    using TryCatch.Cqrs.Queries.UnitTests.Mocks;
    using TryCatch.Cqrs.Queries.UnitTests.Mocks.Linq;
    using TryCatch.Patterns.Repositories.Linq;
    using Xunit;

    public class GetNextQueryHandlerTests
    {
        private readonly IFlightsQueryFactory factory;

        private readonly IReadingRepository<Flight> repository;

        private readonly IGetNextFlightsQueryHandler sut;

        public GetNextQueryHandlerTests()
        {
            this.repository = Substitute.For<IReadingRepository<Flight>>();
            this.factory = new FlightsQueryFactory();
            this.sut = new GetNextFlightsQueryHandler(this.repository, this.factory);
        }

        [Fact]
        public void Construct_without_repository()
        {
            // Arrange
            IReadingRepository<Flight> repository = null;

            // Act
            Action act = () => _ = new GetNextFlightsQueryHandler(repository, this.factory);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_without_factory()
        {
            // Arrange
            IFlightsQueryFactory factory = null;

            // Act
            Action act = () => _ = new GetNextFlightsQueryHandler(this.repository, factory);

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

            // Act
            var actual = await this.sut.Execute(queryObject).ConfigureAwait(false);

            // Asserts
            actual.Should().NotBeNull();
            actual.Items.Should().BeEquivalentTo(expected);
            actual.Offset.Should().Be(offset);
            actual.Limit.Should().Be(limit);
        }
    }
}
