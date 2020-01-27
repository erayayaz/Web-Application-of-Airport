using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtaturkAirport.Models
{
    public class AirportManager
    {
        public int ManagerEmployeeID { get; set; }
        public string Department { get; set; }

        public string Password { get; set; }
    }
}