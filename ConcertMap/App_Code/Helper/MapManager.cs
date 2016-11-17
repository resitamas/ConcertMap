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
            List<string> cities = new List<string>();

            if (events != null)
            {
                foreach (var e in events)
                {
                    if (!cities.Contains(e.Venue.City))
                    {
                        markers.Add(new Marker() { Name = e.Title, Lat = e.Venue.Lat, Long = e.Venue.Long });
                        cities.Add(e.Venue.City);
                    }
                }
            }

            return markers;
        }

        public static Dictionary<string, double> CreateCountryData(List<Event> events)
        {
            Dictionary<string, double> countryData = new Dictionary<string, double>();

            List<ReducedCountry> countries = CountryManager.GetCountries().ToList();

            if (events != null)
            {
                foreach (var e in events)
                {

                    var country = countries.Where(c => c.OfficalName == e.Venue.Country || c.CommonName == e.Venue.Country).FirstOrDefault();

                    if (country != null)
                    {
                        if (!countryData.ContainsKey(country.ISO3))
                        {
                            countryData.Add(country.ISO3, events.Count(ev => ev.Venue.Country == e.Venue.Country));
                        }
                    }

                }
            }

            return countryData;
        }


        public static Stat CreateStat(Dictionary<string, double> iso3Dict, List<Event> events)
        {

            Stat stat = new Stat()
            {
                CityStat = new List<ChartModel>(),
                CountryStat = new List<ChartModel>(),
                RegionStat = new List<ChartModel>()
            };

            Dictionary<string, double> countrystat = new Dictionary<string, double>();

            var countries = CountryManager.GetCountries().ToList();

            if (iso3Dict != null)
            {
                foreach (var d in iso3Dict)
                {
                    var value = Convert.ToInt32(d.Value);

                    //Create country stat
                    stat.CountryStat.Add(new ChartModel() {
                        Y = value,
                        Name = countries.Where(c => c.ISO3 == d.Key).First().CommonName,
                    }); 

                    //Create region stat
                    var region = countries.Where(c => c.ISO3 == d.Key).First().Region;

                    if (stat.RegionStat.Exists(r => r.Name == region))
                    {
                        stat.RegionStat.Find(r => r.Name == region).Y += Convert.ToInt32(d.Value);
                    }
                    else
                    {
                        stat.RegionStat.Add(new ChartModel() {
                            Y = 1,
                            Name = region,
                        });
                    }
                }

            }

            if (events != null)
            {
                if (events != null)
                {
                    foreach (var e in events)
                    {
                        var city = e.Venue.City;

                        if (stat.CityStat.Exists(c => c.Name == city))
                        {
                            stat.CityStat.Find(c => c.Name == city).Y ++;
                        }
                        else
                        {
                            stat.CityStat.Add(new ChartModel() {
                                Y = 1,
                                Name = city
                            });
                        }
                    }
                }
            }


            stat.CityStat = stat.CityStat.OrderBy(c => c.Y).ToList();
            stat.CountryStat = stat.CountryStat.OrderBy(c => c.Y).ToList();
            stat.RegionStat = stat.RegionStat.OrderBy(c => c.Y).ToList();

            return stat;
        }
    }
}