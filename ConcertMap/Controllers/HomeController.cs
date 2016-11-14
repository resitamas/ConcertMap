using ConcertMap.Models;
using ConcertService.Models;
//using CountriesService;
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
        
        public ActionResult Index(String artist = null, DateTime? fromDate = null, DateTime? toDate = null, Boolean isPast = true, Boolean isUpcoming = true)
        {
            Events model = new Events();

            if (artist == null)
            {
                model.fromDate = Convert.ToDateTime("1900-01-01 00:00:01");
                model.toDate = Convert.ToDateTime("2100-01-01 00:00:01");
                model.isUpcoming = true;
                model.isPast = true;

                return View(model);
            }

            if (isPast && !isUpcoming) model.toDate = DateTime.Now;
            if (!isPast && isUpcoming) model.fromDate = DateTime.Now;

            List<ConcertService.Models.Event> eventList = service.GetEvents(artist, model.fromDate, model.toDate);

            model.events = eventList;
            model.ArtistName = artist;
            model.fromDate = fromDate;
            model.toDate = toDate;
            model.isPast = isPast;
            model.isUpcoming = isUpcoming;



            //var events2 = new Events();

            //events2.events = new List<Event>();
            //events2.events.Add(new Event() {Venue = new Venue() { Country = "Hungary", Lat=20, Long=20, Name = "" } });
            //events2.events.Add(new Event() {Venue = new Venue() { Country = "Hungary", Lat=20, Long=20, Name = "" } });
            //events2.events.Add(new Event() {Venue = new Venue() { Country = "Italy", Lat=20, Long=20, Name = "" } });
            //events2.events.Add(new Event() {Venue = new Venue() { Country = "United States", Lat=20, Long=20, Name = "" } });
            //events2.events.Add(new Event() {Venue = new Venue() { Country = "Italy", Lat=20, Long=20, Name = "" } });
            //events2.events.Add(new Event() {Venue = new Venue() { Country = "Hungary", Lat=20, Long=20, Name = "" } });

            //model.events = events2.events;
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


        [HttpGet]
        public ActionResult Search(Events m)
        {
            return RedirectToAction("Index", "Home", new { artist = m.ArtistName, fromDate = m.fromDate, toDate = m.toDate, isPast = m.isPast, isUpcoming = m.isUpcoming });
        }
    }
    
}