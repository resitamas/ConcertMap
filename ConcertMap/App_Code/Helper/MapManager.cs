using ConcertMap.Models;
using ConcertService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertMap.App_Code.Helper
{
    public class MapManager
    {

        public static List<Marker> GetMatkers(List<Event> events)
        {

            List<Marker> markers = new List<Marker>();


            foreach (var e in events)
            {
                markers.Add(new Marker() { Name = e.Title, Lat = e.Venue.Lat, Long = e.Venue.Long });
            }

            markers.Add(new Marker() { Name = "Vatican City", Lat = 41.90, Long = 12.45 });

            return markers;

            //return "[{latLng: [41.90, 12.45], name: 'Vatican City'},{latLng: [43.73, 7.41], name: 'Monaco'},{latLng: [-0.52, 166.93], name: 'Nauru'}]";
        }


    }
}