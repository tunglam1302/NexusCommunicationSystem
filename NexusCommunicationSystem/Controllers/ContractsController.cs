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
    public class ContractsController : Controller
    {
        private NexusCommunicationSystemContext db = new NexusCommunicationSystemContext();

        // GET: Contracts
        public ActionResult Index()
        {
            return View(db.Contracts.ToList());
        }

        // GET: Contracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // GET: Contracts/Create
        public ActionResult Create()
        {
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "AccountId");
            ViewBag.RetailStoreId = new SelectList(db.RetailStores, "Id", "Name");
            ViewBag.ServicePackageId = new SelectList(db.ServicePackages, "Id", "Name");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderStatus,SecurityDeposit,TotalAmount,AmountDue,Quantity,NextPaymentAt,Discounts")] Contract contract)
        {
            contract.CreatedAt = DateTime.Now;
            contract.UpdatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Contracts.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contract);
        }

        // GET: Contracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderStatus,CreatedAt,UpdatedAt,SecurityDeposit,TotalAmount,AmountDue,Amount,NextPaymentAt,ChargeForReplacementDone,Discounts")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contract contract = db.Contracts.Find(id);
            db.Contracts.Remove(contract);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public double  CalculateContractValue(string ServiceId, string ServicePackageId, string Quantity, string Discounts, string SecurityDeposit)
        {
            double totalAmount = 0;

            var servicePackageId = Int32.Parse(ServicePackageId);
            
            var quantity = Int32.Parse(Quantity);
            var discount = double.Parse(Discounts);
            var securityDeposit = Int32.Parse(SecurityDeposit);

            var serviceId = Int32.Parse(ServiceId);
            var service = db.Services.Where(s => s.Id == serviceId).Single();
            var servicePrice = service.TotalAmount;

            totalAmount = securityDeposit + servicePrice * quantity * (1 - discount);

            return totalAmount;
        }

        public int GetServiceTotalAmount(string ServiceId)
        {
            int totalPrice = 0;
            var thisServiceId = Int32.Parse(ServiceId);

            var service = db.Services.Where(s => s.Id == thisServiceId).Single();
            totalPrice = service.TotalAmount;

            return totalPrice;
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
