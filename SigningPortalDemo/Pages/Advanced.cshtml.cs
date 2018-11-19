using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SigningPortalDemo.Models;
using SigningPortalDemo.SignApi;
using SigningPortalDemo.SignApi.Request;
using SigningPortalDemo.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;


namespace SigningPortalDemo.Pages
{
    public class AdvancedModel : PageModel
    {
        [BindProperty]
        public NewRequest NewRequest { get; set; }

        public void OnGet()
        {
        }

        public async System.Threading.Tasks.Task OnPostAsync()
        {
            byte[] doc =
                FileHelpers.ProcessFormFile(NewRequest.UploadPdf, ModelState);


            List<DocumentInfo> documents = new List<DocumentInfo>();
            DocumentInfo document = new DocumentInfo()
            {
                Name = NewRequest.UploadPdf.FileName,
                Data = Convert.ToBase64String(doc)
            };

            documents.Add(document);

            List<Signer> signers = new List<Signer>();
            Signer s = new Signer()
            {
                Method = NewRequest.AuthenticationMethod,
                SignerEmail = NewRequest.Signer
            };

            signers.Add(s);

            Int32 signBefore = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + 14 * 24 * 3600;

            var createSignatureRequest = new SignatureRequest()
            {
                OnBehalfOf = NewRequest.OnBehalfOf,
                Name = NewRequest.Name,
                ResponseUrl = "http://google.com",
                Signers = signers,
                SignBefore = signBefore,
                Documents = documents
            };

            HttpClient client = await HttpUtil.GetAuthorizedHttpClientAsync();

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SignatureRequest));
            ser.WriteObject(ms, createSignatureRequest);
            byte[] json = ms.ToArray();
            ms.Close();

            var content = new StringContent(Encoding.UTF8.GetString(json, 0, json.Length), Encoding.UTF8, "application/json");
            var response = client.PostAsync("https://signapidev.azurewebsites.net/request", content).Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Cannot upload document");
            }
        }
    }
}