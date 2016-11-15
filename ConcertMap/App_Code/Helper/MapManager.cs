﻿using ConcertMap.Controllers;
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
                CityStat = new Dictionary<string, int>(),
                CountryStat = new Dictionary<string, int>(),
                RegionStat = new Dictionary<string, int>()
            };

            Dictionary<string, double> countrystat = new Dictionary<string, double>();

            var countries = CountryManager.GetCountries().ToList();

            if (iso3Dict != null)
            {
                foreach (var d in iso3Dict)
                {
                    var value = Convert.ToInt32(d.Value);

                    //Create country stat
                    stat.CountryStat.Add(countries.Where(c => c.ISO3 == d.Key).First().CommonName, value);

                    //Create region stat
                    var region = countries.Where(c => c.ISO3 == d.Key).First().Region;

                    if (stat.RegionStat.ContainsKey(region))
                    {
                        stat.RegionStat[region] += value;
                    }
                    else
                    {
                        stat.RegionStat.Add(region, value);
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

                        if (stat.CityStat.ContainsKey(city))
                        {
                            stat.CityStat[city] ++;
                        }
                        else
                        {
                            stat.CityStat.Add(city, 1);
                        }
                    }
                }
            }


            return stat;
        }
    }
}