using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BlogSharpTeam.Models;

namespace BlogSharpTeam.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult FunIndex()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Menu()
        {
            ViewBag.Message = "Your menu page.";

            return View();
        }
        public ActionResult Posts()
        {
            ViewBag.Message = "Your posts page.";

            return View();
        }
        private ApplicationDbContext db = new ApplicationDbContext();
        //Get: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Author).OrderByDescending(p => p.Date).Take(3);
            return View(posts.ToList());
        }
    }
}