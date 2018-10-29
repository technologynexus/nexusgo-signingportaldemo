using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SigningPortalDemo.Models;
using SigningPortalDemo.SignApi;
using SigningPortalDemo.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SigningPortalDemo.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
   }
}