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
                var settings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local,
                    Formatting = Newtonsoft.Json.Formatting.None,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Include,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };

                IEnumerable<dynamic> ViewServices = this.Services.Select(x => new
                {
                    Id = x.Id,
                    Service_Equipments = x.Service_Equipments.Select(k =>
                    new {
                        Equipment = new
                        {
                            Name = k.Equipment.Name,
                            Amount = k.Equipment.Amount,
                            Price = k.Equipment.Price
                        }
                    }),
                    Name = x.Name,
                    Description = x.Description,
                    TotalAmount = x.TotalAmount,

                });
                return JsonConvert.SerializeObject(ViewServices, settings);
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
            var comboService = db.Services.Where(s => s.Name.Contains("Combo")).ToList();

            model.ServiceClassified = new List<IEnumerable<Service>>() { landlineService, dialupService, broadbandService, comboService };
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