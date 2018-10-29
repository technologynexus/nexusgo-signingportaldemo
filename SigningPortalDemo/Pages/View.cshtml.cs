using Microsoft.AspNetCore.Mvc.RazorPages;
using SigningPortalDemo.DB;
using SigningPortalDemo.SignApi;
using System.Linq;

namespace SigningPortalDemo.Pages
{
    public class ViewModel : PageModel
    {
        public RequestInfo RequestInfo { get; set; }
        public void OnGet(string id)
        {
            RequestInfo = RequestStorage.FindById(id);
            var downloadName = RequestInfo.Name.Replace(" ", "_") + "-levepo-terms.pdf";
            TempData["Embed"] = "<object id=\"pdf\" type=\"application/pdf\" data=\"data:application/pdf;base64," 
                + RequestInfo.Documents.First().Data
                + "\" width=\"100%\" height=\"100%\">alt : <a href=\"" + downloadName + "\">test.pdf</a></object>";
        }
    }
}