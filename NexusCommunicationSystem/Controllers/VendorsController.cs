﻿using System;
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
    public class VendorsController : Controller
    {
        private NexusCommunicationSystemContext db = new NexusCommunicationSystemContext();

        // GET: Vendors
        public ActionResult Index(String keyword, int? page, int? limit)
        {
            
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                if (page == null)
                {
                    page = 1;
                }

                if (limit == null)
                {
                    limit = 10;
                }
                var predicate = PredicateBuilder.New<Vendor>(true);
                if (!keyword.IsNullOrWhiteSpace())
                {
                    predicate = predicate.Or(f => f.Name.Contains(keyword));
                    predicate = predicate.Or(f => f.Address.Contains(keyword));
                    ViewBag.Keyword = keyword;
                }
                var data = db.Vendors.AsExpandable().Where(predicate).OrderByDescending(a => a.Id).ToPagedList(page.Value, limit.Value);
                return View(data);
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
        }

        // GET: Vendors/Details/5
        public ActionResult Details(int? id)
        {
            
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vendor vendor = db.Vendors.Find(id);
                if (vendor == null)
                {
                    return HttpNotFound();
                }
                return View(vendor);
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
        }

        // GET: Vendors/Create
        public ActionResult Create()
        {
            
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                return View();
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }

        }

        // POST: Vendors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                db.Vendors.Add(vendor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendor);
        }

        // GET: Vendors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vendor vendor = db.Vendors.Find(id);
                if (vendor == null)
                {
                    return HttpNotFound();
                }
                return View(vendor);
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
            
        }

        // POST: Vendors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendor);
        }

        // GET: Vendors/Delete/5
        public ActionResult Delete(int? id)
        {
            
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vendor vendor = db.Vendors.Find(id);
                if (vendor == null)
                {
                    return HttpNotFound();
                }
                return View(vendor);
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendor vendor = db.Vendors.Find(id);
            db.Vendors.Remove(vendor);
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
