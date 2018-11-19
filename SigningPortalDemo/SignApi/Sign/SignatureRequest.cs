using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SigningPortalDemo.SignApi.Sign
{
    public class SignatureRequest
    {
        public IList<DocumentInfo> Documents { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public string ResponseUrl { get; set; }

        public Signer Signer { get; set; }
    }
}
