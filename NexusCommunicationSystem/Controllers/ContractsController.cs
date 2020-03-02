using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using LinqKit;
using Microsoft.Ajax.Utilities;
using NexusCommunicationSystem.Models;
using PagedList;

namespace NexusCommunicationSystem.Controllers
{
    public class ContractsController : Controller
    {
        private NexusCommunicationSystemContext db = new NexusCommunicationSystemContext();
        double amountPaidEachBilling = 0;

        // GET: Contracts
        public ActionResult Index(int? page, int? limit, string start, string end)
        {

            if (page == null)
            {
                page = 1;
            }

            if (limit == null)
            {
                limit = 10;
            }
            var startTime = DateTime.Now;
            startTime = startTime.AddYears(-1);
            try
            {
                startTime = DateTime.Parse(start);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var endTime = DateTime.Now;
            try
            {
                endTime = DateTime.Parse(end);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            ViewBag.limit = limit;
            var contracts = db.Contracts.OrderByDescending(s => s.CreatedAt).Where(s => s.CreatedAt >= startTime && s.CreatedAt <= endTime);
            ViewBag.TotalPage = Math.Ceiling((double)contracts.Count() / limit.Value);
            ViewBag.CurrentPage = page;
            ViewBag.Limit = limit;
            ViewBag.Start = startTime.ToString("yyyy-MM-dd");
            ViewBag.End = endTime.ToString("yyyy-MM-dd");
            var list = contracts.Skip((page.Value - 1) * limit.Value).Take(limit.Value).ToList();
            return View(list);
        }

        public ActionResult GetChartData(string start, string end)
        {
            var startTime = DateTime.Now;
            startTime = startTime.AddYears(-1);
            try
            {
                startTime = DateTime.Parse(start);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0, 0);

            var endTime = DateTime.Now;
            try
            {
                endTime = DateTime.Parse(end);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            endTime = new DateTime(endTime.Year, endTime.Month, endTime.Day, 23, 59, 59, 0);

            var data = db.Contracts.Where(s => s.CreatedAt >= startTime && s.CreatedAt <= endTime)
                .GroupBy(
                    s => new
                    {
                        Year = s.CreatedAt.Year,
                        Month = s.CreatedAt.Month,
                        Day = s.CreatedAt.Day
                    }
                ).Select(s => new
                {
                    Date = s.FirstOrDefault().CreatedAt,
                    TotalAmount = s.FirstOrDefault().TotalAmount
                }).OrderBy(s => s.Date).ToList();
            return new JsonResult()
            {
                Data = data.Select(s => new
                {
                    Date = s.Date.ToString("MM/dd/yyyy"),
                    TotalAmount = s.TotalAmount
                }),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
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
            var dateToExportBill = DatesToExportBill(contract);
            ViewBag.DateToExportBill = dateToExportBill;
            ViewBag.NumberOfBillingEachYear = (int)amountPaidEachBilling;
            return View(contract);
        }

        public List<DateTime> DatesToExportBill(Contract contract)
        {
            List<DateTime> dateToExportBill = new List<DateTime>();
            var startDate = contract.CreatedAt;
            int numberOfBillingEachYear = 0;

            switch (contract.ServicePackage.Name)
            {
                case ("Monthly"):
                    numberOfBillingEachYear = 12;
                    for (int i = 0; i < numberOfBillingEachYear; i++)
                    {
                        var billingDate = startDate.AddMonths(i);
                        dateToExportBill.Add(billingDate);
                    }
                    break;
                case ("Quaterly"):
                    numberOfBillingEachYear = 4;
                    for (int i = 0; i < numberOfBillingEachYear; i++)
                    {
                        var billingDate = startDate.AddMonths(i*3);
                        dateToExportBill.Add(billingDate);
                    }
                    break;
                case ("HalfYearly"):
                    for (int i = 0; i < numberOfBillingEachYear; i++)
                    {
                        var billingDate = startDate.AddMonths(i * 6);
                        dateToExportBill.Add(billingDate);
                    }
                    numberOfBillingEachYear = 2;
                    break;
                default:
                    numberOfBillingEachYear = 1;
                    dateToExportBill.Add(startDate);
                    break;
            }
            amountPaidEachBilling = contract.TotalAmount/ numberOfBillingEachYear;
            
            return dateToExportBill;
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
        public ActionResult Create([Bind(Include = "Id,CreatedAt,UpdatedAt,OrderStatus,CustomerId,SecurityDeposit,TotalAmount,Quantity,Discounts,RetailStoreId,ServiceId,ServicePackageId")] Contract contract)
        {
            contract.CreatedAt = DateTime.Now;
            contract.UpdatedAt = DateTime.Now;
            contract.OrderStatus = OrderStatus.Pending;
            var accountId = Session["AccountId"];
            contract.CustomerId = accountId!=null?(int)accountId:1;
            //contract.NextPaymentAt;
            //contract.AcceptedBy;

            if (ModelState.IsValid)
            {
                db.Contracts.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contract);
        }

        [HttpPost]
        public string AjaxCreate([Bind(Include = "Id,CreatedAt,UpdatedAt,OrderStatus,CustomerId,SecurityDeposit,TotalAmount,Quantity,Discounts,RetailStoreId,ServiceId,ServicePackageId")] Contract contract)
        {

            contract.CreatedAt = DateTime.Now;
            contract.UpdatedAt = DateTime.Now;
            contract.OrderStatus = OrderStatus.Pending;
            //contract.NextPaymentAt;
            //contract.AcceptedBy;

            if (ModelState.IsValid)
            {
                db.Contracts.Add(contract);
                db.SaveChanges();

                var Obj = new
                {
                    Id = contract.Id,
                    RetailStoreId = contract.RetailStoreId,
                    ServiceId = contract.ServiceId,
                    ServicePackageId = contract.ServicePackageId,
                    CustomerId = contract.CustomerId
                };

                return JsonConvert.SerializeObject(Obj);
            }

            return null;
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

            ViewBag.RetailStoreId = new SelectList(db.RetailStores, "Id", "Name", contract.RetailStoreId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", contract.ServiceId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "AccountId", contract.CustomerId);
            ViewBag.ServicePackageId = new SelectList(db.ServicePackages, "Id", "Name", contract.ServicePackageId);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedAt,NextPaymentAt,UpdatedAt,OrderStatus,CustomerId,SecurityDeposit,TotalAmount,Quantity,Discounts,RetailStoreId,ServiceId,ServicePackageId")] Contract contract)
        {
            contract.UpdatedAt = DateTime.Now;
            contract.OrderStatus = OrderStatus.DirectTransfer;
            contract.CustomerId = Session["AccountId"]==null?1: (int)Session["AccountId"];
            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RetailStoreId = new SelectList(db.RetailStores, "Id", "Name", contract.RetailStoreId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", contract.ServiceId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "AccountId", contract.CustomerId);
            ViewBag.ServicePackageId = new SelectList(db.ServicePackages, "Id", "Name", contract.ServicePackageId);
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
            contract.OrderStatus = OrderStatus.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public double CalculateContractValue(string ServiceId, string ServicePackageId, string Quantity, string Discounts, string SecurityDeposit)
        {
            double totalAmount = 0;

            var servicePackageId = Int32.Parse(ServicePackageId);
            var servicePackage = db.ServicePackages.Where(sp => sp.Id == servicePackageId).Single();

            var quantity = Int32.Parse(Quantity);
            var discount = double.Parse(Discounts);
            var securityDeposit = double.Parse(SecurityDeposit);

            var serviceId = Int32.Parse(ServiceId);
            var service = db.Services.Where(s => s.Id == serviceId).Single();
            var servicePrice = CalculateServicePriceBasedOnServicePackage(service, servicePackage);
                

            totalAmount = securityDeposit + servicePrice * quantity * (1 - discount);

            return totalAmount;
        }

        public int CalculateServicePriceBasedOnServicePackage(Service service, ServicePackage servicePackage)
        {
            var servicePrice = service.TotalAmount;

            switch (servicePackage.Name)
            {
                case ("Monthly"):
                    servicePrice += 5000;
                    break;
                case ("Quaterly"):
                    servicePrice += 4000;
                    break;
                case ("HalfYearly"):
                    servicePrice += 3000;
                    break;
                case ("Yearly"):
                    servicePrice += 2000;
                    break;
                case ("HourlyBasis10"):
                    servicePrice += 6000;
                    break;
                case ("HourlyBasis30"):
                    servicePrice += 6300;
                    break;
                case ("HourlyBasis60"):
                    servicePrice += 65000;
                    break;
                default:
                    break;
            }
             return servicePrice;
        }

        public int GetServiceTotalAmount(string ServiceId)
        {
            int totalPrice = 0;
            var thisServiceId = Int32.Parse(ServiceId);

            var service = db.Services.Where(s => s.Id == thisServiceId).Single();
            totalPrice = service.TotalAmount;

            return totalPrice;
        }

        public string AcceptContract(string contractId, string checkedStatus)
        {
            var contractIdValue = Int32.Parse(contractId);
            var checkedValue = Int32.Parse(checkedStatus);
            var contract = db.Contracts.Where(c => c.Id == contractIdValue).Single();
            
            if (checkedValue == 1)
            {
                if (!string.IsNullOrEmpty(Session["AccountName"] as string))
                contract.AcceptedBy = Session["AccountName"].ToString();
            }
            else
            {
                contract.AcceptedBy = null;
            }
            db.Entry(contract).State = EntityState.Modified;
            db.SaveChanges();
            return contract.AcceptedBy;
        }

        public class ViewContract
        {

            public int Id { get; set; }
            public OrderStatus OrderStatus { get; set; }
            public DateTime CreatedAt { get; set; }
            public double SecurityDeposit { get; set; }
            public double TotalAmount { get; set; }
            public int Quantity { get; set; }
            public int CustomerId { get; set; }
            public int ServiceId { get; set; }
            public int RetailStoreId { get; set; }
            public int ServicePackageId { get; set; }
            public string ServiceName { get; set; }
            public string RetailName { get; set; }
            public string RetailPhone { get; set; }
            public string PackageName { get; set; }
            public IEnumerable<dynamic> Billings { get; set; }
        }
        [HttpGet]
        public string GetContractData(int Id)
        {
            Contract contract = db.Contracts.Find(Id);
            if (contract == null)
            {
                return null;
            }

            ViewContract ViewContract = new ViewContract();
            ViewContract.CreatedAt = contract.CreatedAt;
            ViewContract.SecurityDeposit = contract.SecurityDeposit;
            ViewContract.TotalAmount = contract.TotalAmount;
            ViewContract.Quantity = contract.Quantity;
            ViewContract.Id = contract.Id;
            ViewContract.OrderStatus = contract.OrderStatus;
            ViewContract.CustomerId = contract.CustomerId;
            ViewContract.ServiceId = contract.ServiceId;
            ViewContract.RetailStoreId = contract.RetailStoreId;
            ViewContract.ServicePackageId = contract.ServicePackageId;
            ViewContract.ServiceName = contract.Service.Name;
            ViewContract.RetailName = contract.RetailStore.Name;
            ViewContract.RetailPhone = contract.RetailStore.Telephone;
            ViewContract.PackageName = contract.ServicePackage.Name;
            ViewContract.Billings = contract.Billings.Select(x => new { CreatedAt = x.CreatedAt, BillingAmount = x.BillingAmount });

            return JsonConvert.SerializeObject(ViewContract);
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
