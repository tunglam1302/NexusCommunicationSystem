using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using NexusCommunicationSystem.Models;
namespace NexusCommunicationSystem.Controllers
{


    public class HomeController : Controller
    {
        private NexusCommunicationSystemContext db = new NexusCommunicationSystemContext();
        public class DataViewModel
        {
            public IEnumerable<Service> Services { get; set; }
            public IEnumerable<ServicePackage> ServicePackages { get; set; }
            public IEnumerable<RetailStore> RetailStores { get; set; }
            public IEnumerable<Service> LandlineService { get; set; }
            public IEnumerable<Service> DialupService { get; set; }
            public IEnumerable<Service> BroadbandService { get; set; }
            public List<IEnumerable<Service>> ServiceClassified { get; set; }
            public string GetServiceJSON()
            {
                return JsonConvert.SerializeObject(this.Services);
            }
        }
        public ActionResult Index()
        {
            var model = new DataViewModel();

            model.Services = db.Services.Take(5).ToList();
            model.ServicePackages = db.ServicePackages.ToList();
            model.RetailStores = db.RetailStores.ToList();

            var landlineService = db.Services.Where(s => s.Name.Contains("Landline")).ToList();
            var dialupService = db.Services.Where(s => s.Name.Contains("Dialup")).ToList();
            var broadbandService = db.Services.Where(s => s.Name.Contains("Broadband")).ToList();

            model.ServiceClassified = new List<IEnumerable<Service>>() { landlineService, dialupService, broadbandService };
            return View(model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}