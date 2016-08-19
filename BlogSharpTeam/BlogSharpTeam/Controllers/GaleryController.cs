using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSharpTeam.Controllers
{
    public class GaleryController : Controller
    {
        // GET: Galery
        public ActionResult GaleryIndex()
        {
            return View();
        }
    }
}