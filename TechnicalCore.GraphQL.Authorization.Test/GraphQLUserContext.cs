using System.Collections.Generic;
using System.Security.Claims;

namespace TechnicalCore.GraphQL.Authorization.Test
{
    internal class GraphQLUserContext : Dictionary<string, object>, IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }
    }
}
