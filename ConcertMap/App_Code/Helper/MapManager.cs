using ConcertMap.Controllers;
using ConcertMap.Models;
using ConcertService.Models;
using CountriesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertMap.App_Code.Helper
{
    public class MapManager
    {

        public static List<Marker> GetMarkers(List<Event> events)
        {

            List<Marker> markers = new List<Marker>();

            if (events != null)
            {
                foreach (var e in events)
                {
                    markers.Add(new Marker() { Name = e.Title, Lat = e.Venue.Lat, Long = e.Venue.Long });
                }
            }

            markers.Add(new Marker() { Name = "Vatican City", Lat = 41.90, Long = 12.45 });

            return markers;
        }

        public static Dictionary<string, double> CreateCountryData(List<Event> events)
        {
            Dictionary<string, double> countryData = new Dictionary<string, double>();

            List<ReducedCountry> countries = CountryManager.GetCountries().ToList();

            if (events != null) {
                foreach (var e in events)
                {

                    var country = countries.Where(c => c.OfficalName == e.Venue.Country || c.CommonName == e.Venue.Country).FirstOrDefault();

                    if (country != null)
                    {
                        if (!countryData.ContainsKey(country.Code))
                        {
                            countryData.Add(country.Code, events.Count(ev => ev.Venue.Country == e.Venue.Country));
                        }
                    }

                 }
        }

            return countryData;
        }


    }
}