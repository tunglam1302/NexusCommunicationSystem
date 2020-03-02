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
    public class AccountsController : Controller
    {
        private NexusCommunicationSystemContext db = new NexusCommunicationSystemContext();

        // GET: Accounts
        public ActionResult Index(String keyword, int? page, int? limit)
        {
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                var accounts = db.Accounts.Include(a => a.RetailStore);
                if (page == null)
                {
                    page = 1;
                }

                if (limit == null)
                {
                    limit = 10;
                }
                var predicate = PredicateBuilder.New<Account>(true);
                if (!keyword.IsNullOrWhiteSpace())
                {
                    predicate = predicate.Or(f => f.Email.Contains(keyword));
                    predicate = predicate.Or(f => f.FirstName.Contains(keyword));
                    predicate = predicate.Or(f => f.LastName.Contains(keyword));
                    ViewBag.Keyword = keyword;
                }
                var data = db.Accounts.AsExpandable().Where(predicate).OrderByDescending(a => a.Id).ToPagedList(page.Value, limit.Value);
                return View(data);
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
            
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Account account = db.Accounts.Find(id);
                if (account == null)
                {
                    return HttpNotFound();
                }
                return View(account);
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                ViewBag.RetailStoreId = new SelectList(db.RetailStores, "Id", "Name");
                return View();
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,UserPassword,UserRole,RetailStoreId,AccountId")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RetailStoreId = new SelectList(db.RetailStores, "Id", "Name", account.RetailStoreId);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Account account = db.Accounts.Find(id);
                if (account == null)
                {
                    return HttpNotFound();
                }
                ViewBag.RetailStoreId = new SelectList(db.RetailStores, "Id", "Name", account.RetailStoreId);
                return View(account);
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,UserPassword,UserRole,RetailStoreId,AccountId")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RetailStoreId = new SelectList(db.RetailStores, "Id", "Name", account.RetailStoreId);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            
            if (Session["AccountRole"] is AccountRole.Admin || Session["AccountRole"] is AccountRole.AccountDepartment || Session["AccountRole"] is AccountRole.EmployeeOfRetailOutlet || Session["AccountRole"] is AccountRole.TechnicalPeople)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Account account = db.Accounts.Find(id);
                if (account == null)
                {
                    return HttpNotFound();
                }
                return View(account);
            }
            else
            {
                Session.Clear();
                return Redirect("~/Accounts/Login");
            }
        }

        
        public ActionResult DeleteConfirmed(string id)
        {
            Account account = db.Accounts.Find(Int32.Parse(id));
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessRegister(string firstName, string lastName, string email, string userPassword)
        {
            var account = new Account()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserPassword = userPassword,
            };
            db.Accounts.Add(account);
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
            var account = db.Accounts.Where(c => c.Email == email && c.UserPassword == userPassword).Single();
            if (account == null)
            {
                return HttpNotFound();
            }
            Session["Accounts"] = account;
            Session["AccountName"] = account.FirstName;
            Session["AccountId"] = account.Id;
            var firstName = Session["AccountName"];
            Session["AccountRole"] = account.UserRole;
            if(Session["AccountRole"] is AccountRole.User)
            {
                return Redirect("~/Home/Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public void Logout()
        {
            Session.Clear();
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
