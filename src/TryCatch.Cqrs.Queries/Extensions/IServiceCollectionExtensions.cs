// <copyright file="IServiceCollectionExtensions.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Cqrs.Queries.Extensions
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;
    using TryCatch.Patterns.Results;

    [ExcludeFromCodeCoverage]
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddResultBuilder(this IServiceCollection services) =>
            services.AddTransient<IResultBuilder<long>, ResultBuilder<long>>();

        public static IServiceCollection AddPageResultBuilder<T>(this IServiceCollection services) =>
            services.AddTransient<IPageResultBuilder<T>, PageResultBuilder<T>>();
    }
}
