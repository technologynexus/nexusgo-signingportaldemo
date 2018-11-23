using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigningPortalDemo.DB;
using SigningPortalDemo.SignApi;

namespace SigningPortalDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/Poll")]
    public class PollController : Controller
    {

        [HttpGet("{id}", Name = "PollRequest")]
        public ActionResult GetById(string id)
        {
            var request = RequestStorage.FindById(id);

            return Json(request);
        }
    }
}