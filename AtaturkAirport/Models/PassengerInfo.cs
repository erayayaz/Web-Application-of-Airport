using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtaturkAirport.Models
{
    public class PassengerInfo
    {
        public string TCKN { get; set; }
        public string gender { get; set; }
        public int luggageWeight { get; set; }
        public string name { get; set; }
        public string city { get; set; }
    }
}