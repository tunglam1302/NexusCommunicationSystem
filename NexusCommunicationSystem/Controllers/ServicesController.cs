using NexusCommunicationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NexusCommunicationSystem.Controllers
{
    public class ServicesController : Controller
    {
        private NexusCommunicationSystemContext db = new NexusCommunicationSystemContext();

        // GET: Services
        public ActionResult Index()
        {
            var services = db.Services.Include(s => s.ServicePackage);
            return View(services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            var id = db.Services.OrderByDescending(s => s.Id).FirstOrDefault().Id + 1;
            ViewBag.Id = id;

            var myEquipments = db.Equipments.ToList();
            var myEquipmentJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(myEquipments.ToDictionary(x => x.Id, x => x.Name));
            ViewBag.MyEquipmentJsonString = myEquipmentJsonString;

            ViewBag.ServicePackageId = new SelectList(db.ServicePackages, "Id", "Name");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Image,Description,TotalAmount,ServicePackageId")] Service service)
        {
            var equipmentCookie = Request.Cookies["example"].Value.ToString();
            var equipments = equipmentCookie
                .Replace("%5B", "[")
                .Replace("%7B", "{")
                .Replace("%22", "\"")
                .Replace("%3A", ":")
                .Replace("%2C", ",")
                .Replace("%7D", "}")
                .Replace("%5D", "]").ToString().ToLower();

            if (ModelState.IsValid)
            {
                db.Services.Add(service);
                db.SaveChanges();
                InsertService_Equipment(equipments, service);
                return RedirectToAction("Index");
            }
            
            ViewBag.ServicePackageId = new SelectList(db.ServicePackages, "Id", "Name", service.ServicePackageId);
            return View(service);
        }

        private void InsertService_Equipment(string equipments, Service service)
        {
            var jss = new JavaScriptSerializer();
            dynamic myEquipments = jss.DeserializeObject(equipments);
            foreach (Dictionary<string, object> equipment in myEquipments)
            {
                object equipmentFromDictionary;
                object quantityFromDictionary;
                equipment.TryGetValue("key", out equipmentFromDictionary);
                equipment.TryGetValue("value", out quantityFromDictionary);

                int quantity = Convert.ToInt32(quantityFromDictionary);

                var myEquipment = db.Equipments.Where(e => e.Id == quantity).First();

                var serviceEquipment = new Service_Equipment(quantity, service, myEquipment);
                db.Service_Equipments.Add(serviceEquipment);
            }

        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServicePackageId = new SelectList(db.ServicePackages, "Id", "Name", service.ServicePackageId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Image,Description,TotalAmount,ServicePackageId")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServicePackageId = new SelectList(db.ServicePackages, "Id", "Name", service.ServicePackageId);
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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
