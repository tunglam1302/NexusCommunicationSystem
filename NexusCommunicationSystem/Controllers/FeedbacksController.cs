using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LinqKit;
using Microsoft.Ajax.Utilities;
using NexusCommunicationSystem.Models;
using PagedList;

namespace NexusCommunicationSystem.Controllers
{
    public class FeedbacksController : Controller
    {
        private NexusCommunicationSystemContext db = new NexusCommunicationSystemContext();

        // GET: Feedbacks
        public ActionResult Index(String keyword, int? page, int? limit)
        {
            if (page == null)
            {
                page = 1;
            }

            if (limit == null)
            {
                limit = 10;
            }
            var predicate = PredicateBuilder.New<Feedback>(true);
            if (!keyword.IsNullOrWhiteSpace())
            {
                predicate = predicate.Or(f => f.Customer.Email.Contains(keyword));
                ViewBag.Keyword = keyword;
            }
            var data = db.Feedbacks.AsExpandable().Where(predicate).OrderByDescending(a => a.Id).ToPagedList(page.Value, limit.Value);
            return View(data);
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName");
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,CustomerId")] Feedback feedback)
        {
            feedback.CreatedAt = DateTime.Now;
            feedback.UpdatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feedback);
        }

        public class FeedbackView {
            public string Content { get; set; }
            public string AccountId { get; set; }
        }
        [HttpPost]
        public Boolean AjaxCreate(FeedbackView feedbackview)
        {
            if (ModelState.IsValid)
            {
                Customer FoundCustomer = db.Customers.Where(c => c.AccountId == feedbackview.AccountId).FirstOrDefault();
                if(FoundCustomer == null)
                {
                    return false;
                }

                Feedback Feedback = new Feedback();
                Feedback.CreatedAt = DateTime.Now;
                Feedback.UpdatedAt = DateTime.Now;
                Feedback.Content = feedbackview.Content;
                Feedback.Customer = FoundCustomer;
                Feedback.CustomerId = FoundCustomer.Id;

                db.Feedbacks.Add(Feedback);
                db.SaveChanges();
                return true;
            }

            return false;
        }

        // GET: Feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", feedback.CustomerId);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,CreatedAt,CustomerId")] Feedback feedback)
        {
            feedback.UpdatedAt = DateTime.Now;
            feedback.Customer = db.Customers.Find(feedback.CustomerId);
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void DeleteAllSelectedFeedback(List<string> feedbacks)
        {
            foreach (var feedbackId in feedbacks)
            {
                Feedback feedback = db.Feedbacks.Find(Int32.Parse(feedbackId));
                db.Feedbacks.Remove(feedback);
                db.SaveChanges();
            }
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
