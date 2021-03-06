﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogSharpTeam.Models;
using System.Web.Routing;
using BlogSharpTeam.Models.Comments;
using Microsoft.AspNet.Identity;
using System.Web.UI.WebControls;

namespace BlogSharpTeam.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = db.Comments.Find(id);

            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create(int id)
        {
            var model = new Models.Comments.AddComment();
            model.PostId = id;

            return View(model);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddComment comment)
        {
            var newComment = new Comment();
            newComment.Post = db.Posts.Find(comment.PostId);

            var newauthor = db.Users.Find(User.Identity.GetUserId());
            newComment.Author = newauthor;
            newComment.Text = comment.Text;
            newComment.Date = comment.Date;
            string curentid = comment.PostId.ToString();         
            if (ModelState.IsValid)
            {
                db.Comments.Add(newComment);
                db.SaveChanges();
                return RedirectToAction("Index", "Posts");
            }

            return View(comment);

        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Comment comment = db.Comments.Find(id);
            if (comment == null)

            {
                return HttpNotFound();
            }

            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,Date")] Comment comment)
        {


            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = db.Comments.Find(id);

            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
