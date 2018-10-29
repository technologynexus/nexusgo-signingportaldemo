using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SigningPortalDemo.SignApi
{
    public class CreateSignatureRequest
    {
        public IList<DocumentInfo> Documents { get; set; }

        public string OnBehalfOf { get; set; }

        public string Name { get; set; }

        public string ResponseUrl { get; set; }

        public long SignBefore { get; set; }

        public IList<RequestSigner> Signers { get; set; }
    }
}
