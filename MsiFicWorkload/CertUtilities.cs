using System.Security.Cryptography.X509Certificates;

namespace MsiFicWorkload
{
    internal static class CertUtilities
    {
        public static X509Certificate2 ReadCertByCommonName(string commonName)
        {
            X509Store store = new (StoreLocation.LocalMachine);

            store.Open(OpenFlags.ReadOnly);
            return store.Certificates
                .Find(X509FindType.FindBySubjectName, commonName, false)
                .First();
        }
    }
}
