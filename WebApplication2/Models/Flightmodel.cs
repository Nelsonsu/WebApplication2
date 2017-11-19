using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Flightmodel
    {
        public string FlightID { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string Duration { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string Price { get; set; }
        public string AircraftID { get; set; }

        public string AirlineLogo { get; set; }
    }

    public class Flights
    {
        public List<Flightmodel> Flightdetail { get; set; }
    }
}