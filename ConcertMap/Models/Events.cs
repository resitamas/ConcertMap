using ConcertService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcertMap.Models
{
    public class Events
    {
        [DisplayName("Artist")]
        public string ArtistName { get; set; }

        [DisplayName("From")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fromDate { get; set; }

        [DisplayName("To")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime toDate { get; set; }

        [DisplayName("All upcoming")]
        public bool isUpcoming { get; set; }

        [DisplayName("All past")]
        public bool isPast { get; set; }

        public List<Event> events { get; set; }

        [DisplayName("Choose dates")]
        public bool dates { get; set; }

    }
}