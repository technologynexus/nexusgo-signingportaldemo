using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SigningPortalDemo.SignApi
{
    public class RequestSigner
    {
        public string Method { get; set; }
        public string SignerEmail { get; set; }
    }
}
