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
    public class RetailStoresController : Controller
    {
        private NexusCommunicationSystemContext db = new NexusCommunicationSystemContext();

        // GET: RetailStores
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
            var predicate = PredicateBuilder.New<RetailStore>(true);
            if (!keyword.IsNullOrWhiteSpace())
            {
                predicate = predicate.Or(f => f.Name.Contains(keyword));
                predicate = predicate.Or(f => f.Address.Contains(keyword));
                ViewBag.Keyword = keyword;
            }
            var data = db.RetailStores.AsExpandable().Where(predicate).OrderByDescending(a => a.Id).ToPagedList(page.Value, limit.Value);
            return View(data);
        }

        // GET: RetailStores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RetailStore retailStore = db.RetailStores.Find(id);
            if (retailStore == null)
            {
                return HttpNotFound();
            }
            return View(retailStore);
        }

        // GET: RetailStores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RetailStores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Telephone")] RetailStore retailStore)
        {
            if (ModelState.IsValid)
            {
                db.RetailStores.Add(retailStore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(retailStore);
        }

        // GET: RetailStores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RetailStore retailStore = db.RetailStores.Find(id);
            if (retailStore == null)
            {
                return HttpNotFound();
            }
            return View(retailStore);
        }

        // POST: RetailStores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Telephone")] RetailStore retailStore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(retailStore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(retailStore);
        }

        // GET: RetailStores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RetailStore retailStore = db.RetailStores.Find(id);
            if (retailStore == null)
            {
                return HttpNotFound();
            }
            return View(retailStore);
        }

        // POST: RetailStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RetailStore retailStore = db.RetailStores.Find(id);
            db.RetailStores.Remove(retailStore);
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
