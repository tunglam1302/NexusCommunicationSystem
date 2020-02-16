using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using NexusCommunicationSystem.Models;

namespace NexusCommunicationSystem.Controllers
{
    public class ServicesController : Controller
    {
        private NexusCommunicationSystemContext db = new NexusCommunicationSystemContext();

        // GET: Services
        public ActionResult Index()
        {
            return View(db.Services.ToList());
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
            var id = db.Services.OrderByDescending(s => s.Id).FirstOrDefault().Id+1;

            var equipments = db.Equipments.ToList();
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(equipments.ToDictionary(x => x.Id, x => x.Name));
            ViewBag.Equipments = jsonString;
            ViewBag.Id = id;
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(string equipments, string serviceId, string serviceName, string serviceImage, string serviceDescription)
        {
            //int myServiceId = int.Parse(serviceId);
            //var service = new Service(myServiceId,serviceName, serviceImage, serviceDescription);
            //db.Services.Add(service);
            //db.SaveChanges();

            //InsertService_Equipment(equipments, service);

            //return View("~Views/Services/Index.cshtml");
            return RedirectToAction("Index");
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
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Image,Description")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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

        private void InsertService_Equipment(string equipments, Service service)
        {
            var jss = new JavaScriptSerializer();
            dynamic myEquipments = jss.DeserializeObject(equipments);
            foreach (Dictionary<string,object> equipment in myEquipments)
            {
                object equipmentFromDictionary;
                object quantityFromDictionary; 
                equipment.TryGetValue("key",out equipmentFromDictionary);
                equipment.TryGetValue("value",out quantityFromDictionary);

                int quantity = Convert.ToInt32(quantityFromDictionary);

                var myEquipment = db.Equipments.Where(e => e.Id == quantity).First();

                var serviceEquipment = new Service_Equipment(quantity, service, myEquipment);
                db.Service_Equipments.Add(serviceEquipment);
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
