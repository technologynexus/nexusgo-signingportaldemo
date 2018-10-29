using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SigningPortalDemo.DB;

namespace SigningPortalDemo.Pages
{
    public class ClearRequestModel : PageModel
    {
        public int Count { get; set; }
        public void OnGet()
        {
            Count = RequestStorage.Count();
        }

        public void OnPost()
        {
            RequestStorage.Drop();
        }
    }
}