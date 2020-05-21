using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Extensions
{
    public class PermissaoNecessariaHandler : AuthorizationHandler<PermissaoNecessaria>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissaoNecessaria requisito)
        {
            if(context.User.HasClaim(c => c.Type == "Permissao" && c.Value.Contains(requisito.Permissao)))
            {
                context.Succeed(requisito);
            }

            return Task.CompletedTask;
        }
    }
}