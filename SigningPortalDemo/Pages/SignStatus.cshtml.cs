using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SigningPortalDemo.Pages
{
    public class SignStatusModel : PageModel
    {

        public string RequestId { get; set; }

        public void OnGet()
        {
            RequestId = (string)TempData["RequestId"];
        }
    }
}