using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SigningPortalDemo.DB;
using SigningPortalDemo.SignApi;
using SigningPortalDemo.Utilities;
using System.Collections.Generic;
using System.Net.Http;

namespace SigningPortalDemo.Pages
{
    public class HistoryModel : PageModel
    {
        public RequestInfo[] SignRequest { get; private set; }

        public async System.Threading.Tasks.Task OnGetAsync(string refreshall)
        {
              await UpdateRequestListAsync(refreshall != null);
        }

        private async System.Threading.Tasks.Task UpdateRequestListAsync(bool refresh)
        {
            if (refresh)
            {
                HttpClient client = await HttpUtil.GetAuthorizedHttpClientAsync();
                foreach (RequestInfo request in RequestStorage.FindAll())
                {
                    var requestInfoResponse = client.GetAsync($"{Startup.Configuration["Api:SignApi:Url"]}/request/{request.Id}?includeDocs=true").Result;
                    if (requestInfoResponse.IsSuccessStatusCode)
                    {
                        string requestInfoBody = await requestInfoResponse.Content.ReadAsStringAsync();
                        RequestStorage.Upsert(JsonConvert.DeserializeObject<RequestInfo>(requestInfoBody));
                    }
                }
            }
            SignRequest = RequestStorage.FindAll();
        }

    }
}