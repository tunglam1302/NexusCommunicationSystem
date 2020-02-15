using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NexusCommunicationSystem.Models;

namespace NexusCommunicationSystem.Controllers
{
    public class ServicePackagesController : Controller
    {
        private NexusCommunicationSystemContext db = new NexusCommunicationSystemContext();

        // GET: ServicePackages
        public ActionResult Index()
        {
            return View(db.ServicePackages.ToList());
        }

        // GET: ServicePackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackage servicePackage = db.ServicePackages.Find(id);
            if (servicePackage == null)
            {
                return HttpNotFound();
            }
            return View(servicePackage);
        }

        // GET: ServicePackages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServicePackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PaymentPeriod")] ServicePackage servicePackage)
        {
            if (ModelState.IsValid)
            {
                db.ServicePackages.Add(servicePackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(servicePackage);
        }

        // GET: ServicePackages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackage servicePackage = db.ServicePackages.Find(id);
            if (servicePackage == null)
            {
                return HttpNotFound();
            }
            return View(servicePackage);
        }

        // POST: ServicePackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PaymentPeriod")] ServicePackage servicePackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicePackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servicePackage);
        }

        // GET: ServicePackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackage servicePackage = db.ServicePackages.Find(id);
            if (servicePackage == null)
            {
                return HttpNotFound();
            }
            return View(servicePackage);
        }

        // POST: ServicePackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServicePackage servicePackage = db.ServicePackages.Find(id);
            db.ServicePackages.Remove(servicePackage);
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
