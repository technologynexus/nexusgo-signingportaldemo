using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SigningPortalDemo.AuthApi
{
    public class TokenResponse
    {
        public string Access_token { get; set; }

        public int Expires_in { get; set; }
    }
}
