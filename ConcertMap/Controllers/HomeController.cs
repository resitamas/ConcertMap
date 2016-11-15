using ConcertMap.Models;
using ConcertService;
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
        
        public ActionResult Index(string artist = null, DateTime? fromDate = null, DateTime? toDate = null, bool isPast = true, bool isUpcoming = true, bool dates = false)
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

            if (isPast && !isUpcoming)
            {
                model.toDate = DateTime.Now;
            }
            else
            {
                if (!isPast && isUpcoming)
                {
                    model.fromDate = DateTime.Now;
                }
                else
                {
                    if (isPast && isUpcoming)
                    {
                        model.fromDate = Convert.ToDateTime("1900-01-01 00:00:01");
                        model.toDate = Convert.ToDateTime("2100-01-01 00:00:01");
                    }
                }
            }

            List<Event> eventList = new List<Event>(); ;

            try
            {
                 eventList = service.GetEvents(artist, model.fromDate, model.toDate);
            }
            catch (ConcertException ex)
            {
                //Nincs ilyen előadó
            }
            catch (Exception)
            {
                //Más kívétel
            }

            model.events = eventList;
            model.ArtistName = artist;
            model.fromDate = fromDate;
            model.toDate = toDate;
            model.isPast = isPast;
            model.isUpcoming = isUpcoming;

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
            return RedirectToAction("Index", "Home", new { artist = m.ArtistName, fromDate = m.fromDate, toDate = m.toDate, isPast = m.isPast, isUpcoming = m.isUpcoming, dates = m.dates });
        }
    }
    
}