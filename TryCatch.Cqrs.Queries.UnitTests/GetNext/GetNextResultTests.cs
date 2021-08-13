// <copyright file="GetNextResultTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.UnitTests.GetNext
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using FluentAssertions;
    using TryCatch.Cqrs.Queries.GetNext;
    using TryCatch.Cqrs.Queries.UnitTests.Mocks;
    using Xunit;

    public class GetNextResultTests
    {
        [Fact]
        public void Create_with_default_values()
        {
            // Arrange

            // Act
            var actual = new GetNextResult<Flight>();

            // Asserts
            actual.Items.Should().BeEmpty();
            actual.Offset.Should().Be(0);
            actual.Limit.Should().Be(0);
        }

        [Theory]
        [InlineData(-1, 1)]
        [InlineData(0, 1)]
        [InlineData(1, -1)]
        [InlineData(1, 0)]
        public void Create_with_invalid_offset_or_limit(int offset, int limit)
        {
            // Arrange
            var items = Array.Empty<Flight>();

            // Act
            Action actual = () => _ = new GetNextResult<Flight>(items, offset, limit);

            // Asserts
            actual.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Create_with_invalid_item_list()
        {
            // Arrange
            IEnumerable<Flight> items = null;

            // Act
            Action actual = () => _ = new GetNextResult<Flight>(items, 10, 10);

            // Asserts
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Create_Ok()
        {
            // Arrange
            var items = Array.Empty<Flight>();
            var offset = 10;
            var limit = 10;

            // Act
            var actual = new GetNextResult<Flight>(items, offset, limit);

            // Asserts
            actual.Items.Should().BeEquivalentTo(items);
            actual.Offset.Should().Be(offset);
            actual.Limit.Should().Be(limit);
        }
    }
}
