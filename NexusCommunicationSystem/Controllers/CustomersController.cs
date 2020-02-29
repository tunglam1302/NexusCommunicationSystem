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
using Newtonsoft.Json;
using NexusCommunicationSystem.Models;
using PagedList;

namespace NexusCommunicationSystem.Controllers
{
    public class CustomersController : Controller
    {
        private NexusCommunicationSystemContext db = new NexusCommunicationSystemContext();

        // GET: Customers
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
            var predicate = PredicateBuilder.New<Customer>(true);
            if (!keyword.IsNullOrWhiteSpace())
            {
                predicate = predicate.Or(f => f.Email.Contains(keyword));
                ViewBag.Keyword = keyword;
            }
            var data = db.Customers.AsExpandable().Where(predicate).OrderByDescending(a => a.Id).ToPagedList(page.Value, limit.Value);
            return View(data);
        }

        public ActionResult Track(){
            return View();
        }
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,UserPassword,FeedBack,UserRole")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Passcode = customer.RandomDigits();
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        [HttpPost]
        public string AjaxCreate([Bind(Include = "Id,FirstName,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Passcode = customer.RandomDigits();
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            var Obj = new
            {
                Id = customer.Id
            };
            return JsonConvert.SerializeObject(Obj);
        }
        [HttpPost]
        public string AjaxUpdate([Bind(Include = "AccountId, Id")] Customer customer)
        {
            if (customer.Id == null)
            {
                return JsonConvert.SerializeObject(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
            }

            if (ModelState.IsValid)
            {
                Customer foundCustomer = db.Customers.Find(customer.Id);
            if (foundCustomer == null)
            {
                    return JsonConvert.SerializeObject(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
                }

                foundCustomer.AccountId = customer.AccountId;
                db.SaveChanges();
                var Obj = new
                {
                    AccountId = foundCustomer.AccountId
                };
                return JsonConvert.SerializeObject(Obj);
            }
            return JsonConvert.SerializeObject(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
        }
        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,UserPassword,FeedBack,UserRole")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessRegister(string firstName, string lastName, string email, string userPassword)
        {
            var customer = new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserPassword = userPassword,
            };
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string userPassword)
        {

                Customer customer = db.Customers.Where(c=>c.Email == email && c.UserPassword == userPassword).Single();
            if (customer == null)
            {
                return HttpNotFound();
            }
            Session["Customer"] = customer;

            return RedirectToAction("Index");
        }

        public class CustomerLogin
        {
            public string AccountId { get; set; }
            public string Passcode { get; set; }
        }
        [HttpGet]
        public string GetUserData(CustomerLogin customer)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            Customer FoundCustomer = db.Customers.Where(c => c.AccountId == customer.AccountId && c.Passcode == customer.Passcode).FirstOrDefault();

            if (FoundCustomer == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(FoundCustomer);
        }
        [HttpGet]
        public string GetAccountIdByEmail(string email)
        {
            var customerData = db.Customers.Where(c => c.Email == email).Select(item =>
            new {
                item.AccountId,
                item.FirstName
            }).ToList();

            if (customerData == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(customerData);
        }
    }
}
