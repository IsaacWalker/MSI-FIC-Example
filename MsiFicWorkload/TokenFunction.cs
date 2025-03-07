using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
namespace MsiFicWorkload
{
    public class TokenFunction
    {
        private readonly AADTokenResolver m_tokenResolver;

        public TokenFunction(AADTokenResolver tokenResolver)
        {
            m_tokenResolver = tokenResolver;
        }

        [Function("ResolveTokens")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            await m_tokenResolver.ResolveTokenWithManagedIdentity();
            return new OkObjectResult("Token Resolved with Managed Identity");
        }
    }
}
