using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SigningPortalDemo.SignApi.Sign
{
    public class SignResponse
    {
        public string AutostartToken { get; set; }
        public string RequestId { get; set; }
    }

}
