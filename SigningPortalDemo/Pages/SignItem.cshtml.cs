﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SigningPortalDemo.DB;
using SigningPortalDemo.SignApi;
using SigningPortalDemo.SignApi.Sign;
using SigningPortalDemo.Utilities;

namespace SigningPortalDemo.Pages
{
    public class SignItemModel : PageModel
    {

        [BindProperty]
        public string PersonalNumber { get; set; }
        public string SelectedItem { get; set; }

        public string RequestId { get; set; }
        public ErrorResponse ErrorResponse { get; set; }

        //private Boolean ClearResult = true;

        public void OnGet()
        {
            SelectedItem = (string)TempData.Peek("SelectedItem");

            Image image = new Image(ImageDataFactory.Create(@"wwwroot\images\levepo.png"));
            image.ScaleToFit(1700, 1000);
            image.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            image.SetWidth(200);
            using (MemoryStream dest = new MemoryStream())
            {
                var writer = new PdfWriter(dest);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                document.Add(new Paragraph(new Text("Levepo Terms and Conditions (\"Terms\")")
                    .SetBold()
                    .SetFontSize(28)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetHorizontalAlignment(HorizontalAlignment.CENTER)));
                document.Add(new Paragraph(""));
                document.Add(image);
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("Please read these Terms and Conditions (\"Terms\", \"Terms and Conditions\") carefully before using the https://levepo.azurewebsites.com website (the \"Service\") operated by Levepo (\"us\", \"we\", or \"our\")."));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("\"User\", access to and use of the Service is conditioned on your acceptance of and compliance with these Terms. These Terms apply to all visitors, users and others who access or use the Service."));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(new Text("By accessing or using the Service you agree to be bound by these Terms. If you disagree with any part of the terms then you may not access the Service.").SetBold()));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(new Text("Purchases").SetBold()));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("If you wish to purchase any product or service made available through the Service (\"Purchase\"), you may be asked to supply certain information relevant to your Purchase including, without limitation, your name, email address, delivery address and social security number"));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(new Text("Subscriptions").SetBold()));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("Some parts of the Service are billed on a subscription basis (\"Subscription(s)\"). You will be billed in advance on a recurring and periodic basis (\"Billing Cycle\"). Billing cycles are set either on a monthly or annual basis, depending on the type of subscription plan you select when purchasing a Subscription."));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(new Text("Changes")).SetBold());
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("We reserve the right, at our sole discretion, to modify or replace these Terms at any time. If a revision is material we will try to provide at least 30 (change this) days' notice prior to any new terms taking effect. What constitutes a material change will be determined at our sole discretion."));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(new Text("Contact Us").SetBold()));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("If you have any questions about these Terms, please contact us."));
                document.Close();

                string base64String = Convert.ToBase64String(dest.ToArray());
                TempData["SignEmbed"] = "<object id=\"pdf\" type=\"application/pdf\" data=\"data:application/pdf;base64,"
                    + base64String
                    + "\" width=\"100%\" height=\"100%\">alt : <a href=\"Contract\">test.pdf</a></object>";
                return;
            }
        }
                

        public async Task<IActionResult> OnPostAsync()
        {

            Image image = new Image(ImageDataFactory.Create(@"wwwroot\images\levepo.png"));
            image.ScaleToFit(1700, 1000);
            image.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            image.SetWidth(200);
            using (MemoryStream dest = new MemoryStream())
            {
                var writer = new PdfWriter(dest);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                document.Add(new Paragraph(new Text("Levepo Terms and Conditions (\"Terms\")")
                    .SetBold()
                    .SetFontSize(28)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetHorizontalAlignment(HorizontalAlignment.CENTER)));
                document.Add(new Paragraph(""));
                document.Add(image);
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("Please read these Terms and Conditions (\"Terms\", \"Terms and Conditions\") carefully before using the https://levepo.azurewebsites.com website (the \"Service\") operated by Levepo (\"us\", \"we\", or \"our\")."));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("\"John doe\" access to and use of the Service is conditioned on your acceptance of and compliance with these Terms. These Terms apply to all visitors, users and others who access or use the Service."));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(new Text("By accessing or using the Service you agree to be bound by these Terms. If you disagree with any part of the terms then you may not access the Service.").SetBold()));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(new Text("Purchases").SetBold()));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("If you wish to purchase any product or service made available through the Service (\"Purchase\"), you may be asked to supply certain information relevant to your Purchase including, without limitation, your name, email address, delivery address and social security number"));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(new Text("Subscriptions").SetBold()));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("Some parts of the Service are billed on a subscription basis (\"Subscription(s)\"). You will be billed in advance on a recurring and periodic basis (\"Billing Cycle\"). Billing cycles are set either on a monthly or annual basis, depending on the type of subscription plan you select when purchasing a Subscription."));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(new Text("Changes")).SetBold());
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("We reserve the right, at our sole discretion, to modify or replace these Terms at any time. If a revision is material we will try to provide at least 30 (change this) days' notice prior to any new terms taking effect. What constitutes a material change will be determined at our sole discretion."));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(new Text("Contact Us").SetBold()));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("If you have any questions about these Terms, please contact us."));
                document.Close();

                string base64String = Convert.ToBase64String(dest.ToArray());

                List<DocumentInfo> documents = new List<DocumentInfo>
                {
                    new DocumentInfo()
                    {
                        Name = "John Doe",
                        Data = base64String
                    }
                };

                Signer signer = new Signer()
                {
                    UserId = PersonalNumber,
                    Method = "SE_BANKID"
                };

                var signatureRequest = new SignatureRequest()
                {
                    Documents = documents,
                    Name = $"Rent-a-Wood",
                    Message = "Agree to renting firewood",
                    ResponseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/callback",
                    Signer = signer
                };

                HttpClient client = await HttpUtil.GetAuthorizedHttpClientAsync();

                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SignatureRequest));
                ser.WriteObject(ms, signatureRequest);
                byte[] json = ms.ToArray();
                ms.Close();

                var content = new StringContent(Encoding.UTF8.GetString(json, 0, json.Length), Encoding.UTF8, "application/json");
                var response = client.PostAsync($"{Startup.Configuration["Api:SignApi:Url"]}/sign", content).Result;
                string responseBody = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    RequestId = JsonConvert.DeserializeObject<SignResponse>(responseBody).RequestId;
                    var requestInfoResponse = client.GetAsync($"{Startup.Configuration["Api:SignApi:Url"]}/request/{RequestId}?includeDocs=true").Result;
                    if (requestInfoResponse.IsSuccessStatusCode)
                    {
                        string requestInfoBody = await requestInfoResponse.Content.ReadAsStringAsync();
                        RequestStorage.Upsert(JsonConvert.DeserializeObject<RequestInfo>(requestInfoBody));

                        TempData["RequestId"] = RequestId;
                        return RedirectToPage("SignStatus");
                    }

                    return RedirectToPage("RentItem");
                }
                else
                {
                    ErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseBody);
                    if (ErrorResponse == null)
                    {
                        ErrorResponse = new ErrorResponse
                        {
                            ErrorCode = $"{response.StatusCode}",
                            ErrorMessage = responseBody
                        };
                    }
                }
            }

            //ClearResult = false;

            return Page();
        }
    }

}