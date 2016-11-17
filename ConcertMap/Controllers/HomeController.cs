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
        
        public ActionResult Index(string artist = null, DateTime? fromDate = null, DateTime? toDate = null, bool upcoming = true, bool past = true)
        {
            Events model = new Events();

            if (artist == null)
            {
                HttpCookie cookie_request = Request.Cookies["search"];

                if (cookie_request != null)
                {
                    model.ArtistName = cookie_request.Values["artistName"];
                    model.isPast = Convert.ToBoolean(cookie_request.Values["isPast"]);
                    model.isUpcoming = Convert.ToBoolean(cookie_request.Values["isUpcoming"]);

                    if (model.isPast && model.isUpcoming)
                    {
                        model.fromDate = Convert.ToDateTime("1900-01-01 00:00:01");
                        model.toDate = Convert.ToDateTime("2100-01-01 00:00:01");
                    }
                    if (model.isPast && !model.isUpcoming)
                    {
                        model.fromDate = Convert.ToDateTime("1900-01-01 00:00:01");
                        model.toDate = DateTime.Now;
                    }
                    if (!model.isPast && model.isUpcoming)
                    {
                        model.fromDate = DateTime.Now;
                        model.toDate = Convert.ToDateTime("2100-01-01 00:00:01");
                    }
                    if (!model.isPast && !model.isUpcoming)
                    {
                        model.fromDate = DateTime.Now;
                        model.toDate = DateTime.Now;
                    }
                    
                }
                else
                {
                    model.fromDate = Convert.ToDateTime("1900-01-01 00:00:01");
                    model.toDate = Convert.ToDateTime("2100-01-01 00:00:01");
                    model.isUpcoming = true;
                    model.isPast = true;

                    return View(model);
                }  
            }
            else
            {
                model.ArtistName = artist;
                model.isUpcoming = upcoming;
                model.isPast = past;
            }

           List<Event> eventList = new List<Event>(); 

            try
            {
                eventList = service.GetEvents(model.ArtistName, model.fromDate, model.toDate);

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
           
            HttpCookie cookie = new HttpCookie("search");
            cookie.Values["artistName"] = model.ArtistName;
            cookie.Values["isPast"] = model.isPast.ToString();
            cookie.Values["isUpcoming"] = model.isUpcoming.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);
            
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
            return RedirectToAction("Index", "Home", new { artist = m.ArtistName, fromDate = m.fromDate.ToString("yyyy-MM-dd"), toDate = m.toDate.ToString("yyyy-MM-dd"), upcoming = m.isUpcoming, past = m.isPast });
        }
    }
    
}