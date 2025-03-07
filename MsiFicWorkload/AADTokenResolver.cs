using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using System.Security.Cryptography.X509Certificates;

namespace MsiFicWorkload
{
    public sealed class AADTokenResolver
    {
        public async Task<string?> ResolveTokenWithSecret()
        {
            Guid tenantId = new("a6901868-aa12-4259-9c25-a702cf273105");
            Guid workloadAppId = new("a1e022d1-d35d-4d99-b435-746d4d28350e");
            Uri authority = new($"https://login.microsoftonline.com/{tenantId}/v2.0");
            string scope = "api://419a6967-5e52-410c-a78c-0e00b115aeab/.default";

            string secret = "TODO_SECRET_GOES_HERE";

            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder
                .Create(workloadAppId.ToString())
                .WithAuthority(authority)
                .WithClientSecret(secret)
                .Build();

            var tokenResult = await app.AcquireTokenForClient([scope])
                .ExecuteAsync();

            return tokenResult.AccessToken;
        }

        public async Task<string?> ResolveTokenWithCertificate()
        {
            Guid tenantId = new("a6901868-aa12-4259-9c25-a702cf273105");
            Guid workloadAppId = new("a1e022d1-d35d-4d99-b435-746d4d28350e");
            Uri authority = new($"https://login.microsoftonline.com/{tenantId}/v2.0");
            string scope = "api://419a6967-5e52-410c-a78c-0e00b115aeab/.default";

            string certificateName = "TODO_CERT_NAME_GOES_HERE";
            X509Certificate2 certificate = CertUtilities.ReadCertByCommonName(certificateName);

            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder
                .Create(workloadAppId.ToString())
                .WithAuthority(authority)
                .WithCertificate(certificate)
                .Build();

            var tokenResult = await app.AcquireTokenForClient([scope])
                .ExecuteAsync();

            return tokenResult.AccessToken;
        }

        public async Task<string?> ResolveTokenWithManagedIdentity()
        {
            Guid tenantId = new("a6901868-aa12-4259-9c25-a702cf273105");
            Guid workloadAppId = new("a1e022d1-d35d-4d99-b435-746d4d28350e");
            Guid managedIdentityClientId = new("8b5dd47c-cb92-4a5f-b5ec-ac51389f08a9");
            Uri authority = new($"https://login.microsoftonline.com/{tenantId}/v2.0");
            string scope = "api://419a6967-5e52-410c-a78c-0e00b115aeab/.default";

            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder
                .Create(workloadAppId.ToString())
                .WithAuthority(authority)
                .WithClientAssertion((ct) =>
                {
                    ManagedIdentityClientAssertion managedIdentityClientAssertion = 
                        new(managedIdentityClientId.ToString());

                    return managedIdentityClientAssertion.GetSignedAssertionAsync(ct);
                })
                .Build();

            var tokenResult = await app.AcquireTokenForClient([scope])
                .ExecuteAsync();

            return tokenResult.AccessToken;
        }
    }
}
