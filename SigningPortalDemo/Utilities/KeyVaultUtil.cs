using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.KeyVault.WebKey;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SigningPortalDemo.Utilities
{
    public class KeyVaultUtil
    {
        static readonly string KEYVAULT_BASE_URL = Environment.GetEnvironmentVariable("APPSETTING_KEYVAULT_BASE_URL"); 
        static string KEY = Environment.GetEnvironmentVariable("APPSETTING_KEY");

        static readonly string APPLICATION_ID = Environment.GetEnvironmentVariable("APPSETTING_APPLICATION_ID");
        static readonly string PASSWORD = Environment.GetEnvironmentVariable("APPSETTING_PASSWORD");

        private static string SignKey()
        {
            return KEYVAULT_BASE_URL + "/keys/" + KEY;
        }

        //the method that will be provided to the KeyVaultClient
        public static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(APPLICATION_ID, PASSWORD);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }

        public static string Sign (byte[] inputBytes)
        {
            // signature
            SHA256 sha256 = SHA256Managed.Create();

            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(
                   (authority, resource, scope) => KeyVaultUtil.GetToken(authority, resource, scope)));

            KeyOperationResult resultSign = Task.Run(() => keyVaultClient.SignAsync(SignKey(),
                                                                                    JsonWebKeySignatureAlgorithm.RS256, sha256.ComputeHash(inputBytes)))
                                                                                    .ConfigureAwait(false).GetAwaiter().GetResult();
            var signatureEncoded = System.Convert.ToBase64String(resultSign.Result);

            return signatureEncoded;
        }

        public static JsonWebKeySet GetJwks ()
        {
            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(
                   (authority, resource, scope) => KeyVaultUtil.GetToken(authority, resource, scope)));

            var key = Task.Run(() => keyVaultClient.GetKeyAsync(SignKey())).ConfigureAwait(false).GetAwaiter().GetResult();

            var e = Base64UrlEncoder.Encode(key.Key.E);
            var n = Base64UrlEncoder.Encode(key.Key.E);

            var jsonWebKey = new Microsoft.IdentityModel.Tokens.JsonWebKey()
            {
                Kid = KEY,
                Kty = "RSA",
                E = Base64UrlEncoder.Encode(key.Key.E),
                N = Base64UrlEncoder.Encode(key.Key.N),
                Alg = "RS256"
            };
            JsonWebKeySet jsonWebKeySet = new JsonWebKeySet();
            jsonWebKeySet.Keys.Add(jsonWebKey);

            return jsonWebKeySet;
        }

    }
}
