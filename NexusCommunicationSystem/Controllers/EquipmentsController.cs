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
    public class EquipmentsController : Controller
    {
        private NexusCommunicationSystemContext db = new NexusCommunicationSystemContext();

        // GET: Equipments
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
                var predicate = PredicateBuilder.New<Equipment>(true);
                if (!keyword.IsNullOrWhiteSpace())
                {
                    predicate = predicate.Or(f => f.Name.Contains(keyword));
                    ViewBag.Keyword = keyword;
                }
                var data = db.Equipments.AsExpandable().Where(predicate).OrderByDescending(a => a.Id).ToPagedList(page.Value, limit.Value);
                return View(data);
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
        }

        // GET: Equipments/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Equipment equipment = db.Equipments.Find(id);
                if (equipment == null)
                {
                    return HttpNotFound();
                }
                return View(equipment);
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
            
        }

        // GET: Equipments/Create
        public ActionResult Create()
        {
            
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name");
                return View();
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Amount,VendorId")] Equipment equipment)
        {
            equipment.Vendor = db.Vendors.Find(equipment.VendorId);
            if (ModelState.IsValid)
            {
                db.Equipments.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipment);
        }

        // GET: Equipments/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Equipment equipment = db.Equipments.Find(id);
                if (equipment == null)
                {
                    return HttpNotFound();
                }
                ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name", equipment.VendorId);
                return View(equipment);
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Amount,VendorId")] Equipment equipment)
        {
            equipment.Vendor = db.Vendors.Find(equipment.VendorId);
            if (ModelState.IsValid)
            {
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Equipment equipment = db.Equipments.Find(id);
                if (equipment == null)
                {
                    return HttpNotFound();
                }
                return View(equipment);
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
            
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipment equipment = db.Equipments.Find(id);
            db.Equipments.Remove(equipment);
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
