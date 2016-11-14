using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcertMap.Models
{
    public class Events
    {
        public String ArtistName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? toDate { get; set; }

        public Boolean isUpcoming { get; set; }

        public Boolean isPast { get; set; }

        public List<ConcertService.Models.Event> events { get; set; }

    }
}