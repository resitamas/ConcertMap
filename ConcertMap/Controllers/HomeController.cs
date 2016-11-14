using ConcertMap.Models;
using ConcertService.Models;
using CountriesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConcertMap.Controllers
{
    public class HomeController : Controller
    {

        private ConcertService.Rest.RestHelper service;

        public HomeController()
        {
            service = new ConcertService.Rest.RestHelper();
        }
        
        public ActionResult Index()
        {

            var events = new Events();

            events.events = new List<Event>();
            events.events.Add(new Event() {Venue = new Venue() { Country = "Hungary", Lat=20, Long=20, Name = "" } });
            events.events.Add(new Event() {Venue = new Venue() { Country = "Hungary", Lat=20, Long=20, Name = "" } });
            events.events.Add(new Event() {Venue = new Venue() { Country = "Italy", Lat=20, Long=20, Name = "" } });
            events.events.Add(new Event() {Venue = new Venue() { Country = "United States", Lat=20, Long=20, Name = "" } });
            events.events.Add(new Event() {Venue = new Venue() { Country = "Italy", Lat=20, Long=20, Name = "" } });
            events.events.Add(new Event() {Venue = new Venue() { Country = "Hungary", Lat=20, Long=20, Name = "" } });

            return View(events);
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


        [HttpPost]
        public ActionResult Search(Events model)
        {
            ConcertService.EventType eventType = ConcertService.EventType.All;
            if (model.isUpcoming && !model.isPast) eventType = ConcertService.EventType.Upcoming;
            if (model.isPast && !model.isUpcoming) eventType = ConcertService.EventType.Past;

            List<ConcertService.Models.Event> eventList = service.GetEvents(model.ArtistName, eventType, model.fromDate, model.toDate);

            model.events = eventList;
            return View(model);
        }
    }
}