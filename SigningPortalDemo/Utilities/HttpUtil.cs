using Microsoft.Azure.KeyVault.WebKey;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SigningPortalDemo.AuthApi;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SigningPortalDemo.Utilities
{
    public class HttpUtil
    {
        static string KEY = Environment.GetEnvironmentVariable("APPSETTING_KEY");
        static string CLIENT_ID = Environment.GetEnvironmentVariable("APPSETTING_CLIENT_ID");
        static string ISS = Environment.GetEnvironmentVariable("APPSETTING_ISS");

        static HttpClient httpClient = new HttpClient();

        static string accessToken=null;
        static DateTime tokenExpire;
        static string clientJwt=null;

        private static string CreateJwt()
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            DateTime now = DateTime.UtcNow;
            var claimset = new
            {
                iss = ISS,
                sub = CLIENT_ID,
                aud = "https://go.nexusgroup.com",
                iat = (int)now.Subtract(UnixEpoch).TotalSeconds,
                exp = (int)now.AddMinutes(55).Subtract(UnixEpoch).TotalSeconds
            };

            // header
            var header = new { typ = "JWT", alg = JsonWebKeySignatureAlgorithm.RS256, kid = KEY };

            // encoded header
                var headerSerialized = JsonConvert.SerializeObject(header);
            var headerBytes = Encoding.UTF8.GetBytes(headerSerialized);
            var headerEncoded = System.Convert.ToBase64String(headerBytes);

            // encoded claimset
            var claimsetSerialized = JsonConvert.SerializeObject(claimset);
            var claimsetBytes = Encoding.UTF8.GetBytes(claimsetSerialized);
            var claimsetEncoded = System.Convert.ToBase64String(claimsetBytes);

            // input
            var input = String.Join(".", headerEncoded, claimsetEncoded);
            var inputBytes = Encoding.UTF8.GetBytes(input);

            var signatureEncoded = KeyVaultUtil.Sign(inputBytes);

            // jwt
            return String.Join(".", headerEncoded, claimsetEncoded, signatureEncoded);
        }

        private static async System.Threading.Tasks.Task GetNewAccessTokenAsync()
        {
            HttpClient client = new HttpClient();

            if (clientJwt == null) {
                clientJwt = CreateJwt();
            }

            var tokenRequest = new TokenRequest()
            {
                Assertion = clientJwt
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            String requestBody = JsonConvert.SerializeObject(tokenRequest, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Startup.Configuration["Api:AuthApi:Url"]}", content);
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);
                accessToken = tokenResponse.Access_token;
                tokenExpire = DateTime.UtcNow.AddSeconds(tokenResponse.Expires_in);

                Console.WriteLine("Get token: " + accessToken);
            }
            else
            {
                accessToken = null;
                Console.WriteLine("Cannot get token");
            }

        }

        public static async System.Threading.Tasks.Task<HttpClient> GetAuthorizedHttpClientAsync()
        {
            if (accessToken == null || DateTime.UtcNow.AddSeconds(60).CompareTo(tokenExpire) >0 )
            {
                // Renew Token
                await GetNewAccessTokenAsync();
            }

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return httpClient;
        }

    }
}
