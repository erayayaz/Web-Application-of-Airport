using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtaturkAirport.Models
{
    public class CarPark
    {
        public int SpaceNumber { get; set; }
        public string ArrivalTime { get; set; }
        public string LeavingTime { get; set; }
        public string ParkingLotAirportID { get; set; }
    }
}