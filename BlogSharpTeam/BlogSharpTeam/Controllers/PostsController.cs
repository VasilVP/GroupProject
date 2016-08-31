using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogSharpTeam.Models;
using Microsoft.AspNet.Identity;

namespace BlogSharpTeam.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var post1 = from p in db.Posts.Include(p => p.Author) select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                post1 = post1.Where(p => p.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Title_desc":
                    post1 = post1.OrderByDescending(p => p.Title);
                    break;
                case "Date":
                    post1 = post1.OrderBy(p => p.Date);
                    break;
                case "date_desc":
                    post1 = post1.OrderByDescending(p => p.Date);
                    break;
                default:
                    post1 = post1.OrderBy(p => p.Title);
                    break;
            }

            return View(post1.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Find(id);

            //this.Session.Add("lastPost", id);
            if (post == null)
            {
                return HttpNotFound();
            }

            var postId = db.Posts.Include(p => p.Comments).Single(a => a.Id == id);
            
            return View(postId);
        }
        [Authorize]
        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body")] Post post)
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser ttt = db.Users.Single(a => a.Id == userId);
            post.Author = ttt;
            post.Author_Id = userId;

            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }
        [Authorize]
        [Authorize(Roles = "Administrators")]
        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }
        [Authorize(Roles = "Administrators")]
        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Date,Author_Id")] Post post)

        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }
        [Authorize(Roles = "Administrators")]
        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }
        [Authorize(Roles = "Administrators")]
        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
            
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
