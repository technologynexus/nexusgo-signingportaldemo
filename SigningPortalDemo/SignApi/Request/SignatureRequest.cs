using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SigningPortalDemo.SignApi.Request
{
    public class SignatureRequest
    {
        public IList<DocumentInfo> Documents { get; set; }

        public string OnBehalfOf { get; set; }

        public string Name { get; set; }

        public string ResponseUrl { get; set; }

        public long SignBefore { get; set; }

        public IList<Signer> Signers { get; set; }
    }
}
