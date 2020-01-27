using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtaturkAirport.Models
{
    public class Flight
    {
        public string FlightID { get; set; }
        public string FlightDate { get; set; }
        public string FlightTime { get; set; }
        public string GoesTo { get; set; }
        public string ComesFrom { get; set; }
        public string FAirportID { get; set; }
        public int FPilotID { get; set; }

    }
}