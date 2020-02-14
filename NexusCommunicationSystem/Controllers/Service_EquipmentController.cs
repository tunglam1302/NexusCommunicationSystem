using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using NexusCommunicationSystem.Models;

namespace NexusCommunicationSystem.Controllers
{
    public class Service_EquipmentController : Controller
    {
        private NexusCommunicationSystemContext db = new NexusCommunicationSystemContext();

        // GET: Service_Equipment
        public ActionResult Index()
        {
            return View(db.Service_Equipments.ToList());
        }

        // GET: Service_Equipment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service_Equipment service_Equipment = db.Service_Equipments.Find(id);
            if (service_Equipment == null)
            {
                return HttpNotFound();
            }
            return View(service_Equipment);
        }

        // GET: Service_Equipment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Service_Equipment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Quantity")] Service_Equipment service_Equipment)
        {
            if (ModelState.IsValid)
            {
                db.Service_Equipments.Add(service_Equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service_Equipment);
        }

        // GET: Service_Equipment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service_Equipment service_Equipment = db.Service_Equipments.Find(id);
            if (service_Equipment == null)
            {
                return HttpNotFound();
            }
            return View(service_Equipment);
        }

        // POST: Service_Equipment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantity")] Service_Equipment service_Equipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service_Equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service_Equipment);
        }

        // GET: Service_Equipment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service_Equipment service_Equipment = db.Service_Equipments.Find(id);
            if (service_Equipment == null)
            {
                return HttpNotFound();
            }
            return View(service_Equipment);
        }

        // POST: Service_Equipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service_Equipment service_Equipment = db.Service_Equipments.Find(id);
            db.Service_Equipments.Remove(service_Equipment);
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
