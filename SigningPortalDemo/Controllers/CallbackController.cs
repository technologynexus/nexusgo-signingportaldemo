using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SigningPortalDemo.DB;
using SigningPortalDemo.SignApi;
using SigningPortalDemo.Utilities;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SigningPortalDemo
{
    [Route("api/callback")]
    public class CallbackController : Controller
    {
 
        // POST api/<controller>
        [HttpPost]
        public async System.Threading.Tasks.Task PostAsync([FromBody]RequestInfo requestInfo)
        {
            if (requestInfo.State.Equals("COMPLETED")) {
                // Fetch the signed document if the request is completed
                HttpClient client = await HttpUtil.GetAuthorizedHttpClientAsync();
                var requestInfoResponse = client.GetAsync($"{Startup.Configuration["Api:SignApi:Url"]}/request/{requestInfo.Id}?includeDocs=true").Result;
                if (requestInfoResponse.IsSuccessStatusCode)
                {
                    string requestInfoBody = await requestInfoResponse.Content.ReadAsStringAsync();
                    RequestStorage.Upsert(JsonConvert.DeserializeObject<RequestInfo>(requestInfoBody));
                }
            } else {
                RequestStorage.Upsert(requestInfo);
            }
        }
    }
}
