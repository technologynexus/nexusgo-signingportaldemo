using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SigningPortalDemo.SignApi.Request
{
    public class Signer
    {
        public string Method { get; set; }
        public string SignerEmail { get; set; }
        public string UserId { get; set; }
        public string AccountRef { get; set; }
    }
}
