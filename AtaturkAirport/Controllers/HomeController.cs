using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using AtaturkAirport.Models;

namespace AtaturkAirport.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult ManagerHomePage()
        {
            return View();
        }
     
    }
}