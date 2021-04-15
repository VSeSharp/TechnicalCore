using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TechnicalCore.Api.GraphQL
{
    public class TechnicalCoreSchema : Schema
    {
        public TechnicalCoreSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<TechnicalCoreQuery>();
        }
    }
}
