using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SigningPortalDemo.Utilities;

namespace SigningPortalDemo.Controllers
{
    public class Jwk
    {
        public string Kid { get; set; }
        public string Kty { get; set; }
        public string Alg { get; set; }
        public string E { get; set; }
        public string N { get; set; }

    }

    public class Jwks
    {
        public List<Jwk> Keys { get; set; }
    }

    [Produces("application/json")]
    [Route("api/Jwks")]
    public class JwksController : Controller
    {
        // GET: api/Jwks
        [HttpGet]
        public Jwks Get()
        {
            var jwkList = new List<Jwk>();
            foreach (JsonWebKey k in KeyVaultUtil.GetJwks().Keys)
            {
                var jwk = new Jwk()
                {
                    Kid = k.Kid,
                    Kty = k.Kty,
                    Alg = k.Alg,
                    E = k.E,
                    N = k.N
                };

                jwkList.Add(jwk);
            }
            return new Jwks
            {
                Keys = jwkList
            };
        }

    }
}
