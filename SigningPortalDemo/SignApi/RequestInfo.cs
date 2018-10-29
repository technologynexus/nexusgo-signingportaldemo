using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SigningPortalDemo.SignApi
{
    public class RequestInfo
    {
        public string Id { get; set; }
        public String Name { get; set; }
        public List<DocumentInfo> Documents { get; set; } = new List<DocumentInfo>();
        public long Created { get; set; }
        public long SignBefore { get; set; }
        public List<SignerInfo> Signatures { get; set; }
        public string State { get; set; }
        public string CreatedAsString
        {
            get
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(Created).ToString("yyMMdd HH:mm:ss");
            }
        }

        public bool ContainsDocuments()
        {
            return Documents != null && Documents.Count() > 0;
        }

    }

    public class DocumentInfo
    {
        [Newtonsoft.Json.JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
        [Newtonsoft.Json.JsonProperty(Required = Required.Always)]
        public string Data { get; set; }
    }

    public class SignerInfo
    {
        public string SignerEmail { get; set; }
        public Boolean Signed { get; set; }
        public long SignedAt { get; set; }
        public string Method { get; set; }
    }

}
