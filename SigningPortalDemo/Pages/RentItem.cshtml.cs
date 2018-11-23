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
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SigningPortalDemo.Pages
{
    public class RentItemModel : PageModel
    {
        [BindProperty]
        public SignDemo SignDemo { get; set; }

        private string base64Document { get; set; }
        public ErrorResponse ErrorResponse { get; set; }
        public string RequestId { get; set; }
        public string SigningState { get; set; }
        private Boolean ClearResult = true;
        public void OnGet()
        {
            if(ClearResult)
            {
                ErrorResponse = null;
                RequestId = null;
            }

            ClearResult = true;
        }

        public IActionResult OnPost()
        {
            TempData["PersonalNumber"] = SignDemo.PersonalNumber;
            TempData["SelectedItem"] = SignDemo.Wood;

            return RedirectToPage("/SignItem");
        }
    }

    public class SignDemo
    {
        public string Wood { get; set; }
        public string PersonalNumber { get; set; }
    }
}