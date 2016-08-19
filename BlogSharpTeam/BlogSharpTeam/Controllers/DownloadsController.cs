using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSharpTeam.Controllers
{
    public class DownloadsController : Controller
    {
        // GET: Downloads
        public ActionResult DownloadsIndex()
        {
            return View();
        }
    }
}