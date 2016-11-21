using ConcertMap.App_Code.Helper;
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
            Dictionary<string, Position> cities = new Dictionary<string, Position>();
            Dictionary<string, List<string>> citiesConcerts = new Dictionary<string, List<string>>();

            if (events != null)
            {
                foreach (var e in events)
                {
                    if (!cities.ContainsKey(e.Venue.City))
                    {
                        cities.Add(e.Venue.City, new Position(e.Venue.Lat,e.Venue.Long));
                        List<string> concerts = new List<string>();
                        concerts.Add(e.Date.ToString("yyyy-MM-dd") + " - " + e.Title);
                        citiesConcerts.Add(e.Venue.City, concerts);
                    }
                    else
                    {
                        List<string> concerts = new List<string>();
                        concerts = citiesConcerts[e.Venue.City];
                        concerts.Add(e.Date.ToString("yyyy-MM-dd") +" - "+ e.Title);
                        citiesConcerts[e.Venue.City] = concerts;
                    }
                }

                double latitude = 0;
                double longitude = 0;

                foreach (var city in cities)
                {
                    City city1 = new City { Name = city.Key, Concerts = new List<string>() };
                    latitude = city.Value.latitude;
                    longitude = city.Value.longitude;

                    foreach (var concert in citiesConcerts[city.Key])
                    {
                        city1.Concerts.Add(concert);
                    }

                    MarkerStyle style = new MarkerStyle() { Color = "#B88A99", Border = "#805563" , Sugar = GetSugar(citiesConcerts[city.Key].Count) };

                    markers.Add(new Marker() { Name = city1, Lat = latitude, Long = longitude, Style = style });
                }
            }

            return markers;
        }

        private static double GetSugar(int count)
        {

            double sugar = 3 + count * 0.5;

            if (sugar >= 6)
            {
                return 6;
            }

            return sugar;

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