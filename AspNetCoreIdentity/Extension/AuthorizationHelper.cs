using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Extension
{
    public class PermissaoNecessaria : IAuthorizationRequirement
    {
        public string Permissao { get; }

        public PermissaoNecessaria(string permissao)
        {
            this.Permissao = permissao; 
        }
    }

    public class PermissaoNecessariaHandler : AuthorizationHandler<PermissaoNecessaria>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissaoNecessaria requirement)
        {
            //example ClaimValue: PodeLer,PodeEscrever
            if (context.User.HasClaim(c=>c.Type == "Permissao" && c.Value.Contains(requirement.Permissao))){
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
