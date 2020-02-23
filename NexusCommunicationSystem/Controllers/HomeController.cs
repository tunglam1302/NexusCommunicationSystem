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