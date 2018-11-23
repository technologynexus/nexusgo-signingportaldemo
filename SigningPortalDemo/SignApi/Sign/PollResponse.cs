using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SigningPortalDemo.SignApi.Sign
{
    public class PollResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long Created { get; set; }
        public string State { get; set; }
    }

}
