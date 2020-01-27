using AtaturkAirport.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtaturkAirport.Controllers
{
    public class FlightController : Controller
    {

        public List<Flight> GetFlights()
        {
            List<Flight> flights = new List<Flight>();
            string conString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(conString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Response.Write("error" + ex.ToString());
                connection.Close();
                return null;
            }

            string cmdString = "SELECT * FROM Flight";
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            SqlDataReader nwReader = cmd.ExecuteReader();
            while (nwReader.Read())
            {
                Flight flight = new Flight();
                flight.FlightID = (string)nwReader["FlightID"];
                flight.FlightDate = (string)nwReader["Flight_date"].ToString();
                flight.FlightTime = (string)nwReader["FlightTime"].ToString();
                flight.GoesTo = (string)nwReader["GoesTo"].ToString();
                flight.ComesFrom = (string)nwReader["ComesFrom"].ToString();
                flight.FAirportID = (string)nwReader["FAirportID"];
                flight.FPilotID = (int)nwReader["FPilotID"];
                flights.Add(flight);
            }

            return flights;
        }
        [HttpPost]
        public ActionResult SearchFlight(Flight flight)
        {
            string conString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(conString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Response.Write("error" + ex.ToString());
                connection.Close();
                return null;
            }

            if (flight.FlightID != null)
            {
                string cmdString1 = "SearchFlight";
                SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@FlightID", flight.FlightID);
                SqlDataReader sqlDataReader = cmd1.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    ViewBag.FlightID = (string)sqlDataReader["FlightID"];
                    ViewBag.FlightDate = (string)sqlDataReader["Flight_date"].ToString();
                    ViewBag.FlightTime = (string)sqlDataReader["FlightTime"].ToString();
                    ViewBag.GoesTo = (string)sqlDataReader["GoesTo"].ToString();
                    ViewBag.ComesFrom = (string)sqlDataReader["ComesFrom"].ToString();
                }
            }


            return View();
        }

        [HttpGet]
        public ActionResult SearchFlight()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ManagerSearchFlight(Flight flight)
        {
            string conString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(conString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Response.Write("error" + ex.ToString());
                connection.Close();
                return null;
            }

            if (flight.FlightID != null)
            {
                string cmdString1 = "SearchFlight";
                SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@FlightID", flight.FlightID);
                SqlDataReader sqlDataReader = cmd1.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    ViewBag.FlightID = (string)sqlDataReader["FlightID"];
                    ViewBag.FlightDate = (string)sqlDataReader["Flight_date"].ToString();
                    ViewBag.FlightTime = (string)sqlDataReader["FlightTime"].ToString();
                    ViewBag.GoesTo = (string)sqlDataReader["GoesTo"].ToString();
                    ViewBag.ComesFrom = (string)sqlDataReader["ComesFrom"].ToString();
                }
            }


            return View();
        }

        [HttpGet]
        public ActionResult ManagerSearchFlight()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PilotSearch(Flight flight)
        {
            string conString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(conString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Response.Write("error" + ex.ToString());
                connection.Close();
                return null;
            }

            if (flight.FlightID != null)
            {
                string cmdString1 = "PilotInfo";
                SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@flightID", flight.FlightID);
                SqlDataReader sqlDataReader = cmd1.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    ViewBag.FlightID = (string)sqlDataReader["FlightID"].ToString();
                    ViewBag.PilotID = (int)sqlDataReader["PilotID"];
                    ViewBag.TCKN = (string)sqlDataReader["PTCKN"].ToString();
                    ViewBag.Name = (string)sqlDataReader["name"];
                    ViewBag.FlightDate = (string)sqlDataReader["Flight_date"].ToString();
                    ViewBag.FlightTime = (string)sqlDataReader["FlightTime"].ToString();
                    ViewBag.GoesTo = (string)sqlDataReader["GoesTo"].ToString();
                    ViewBag.ComesFrom = (string)sqlDataReader["ComesFrom"].ToString();
                }
            }


            return View();
        }

        [HttpGet]
        public ActionResult PilotSearch()
        {
            return View();
        }


        // GET: Flight

        public ActionResult Flights()
        {
            return View(GetFlights().ToList());
        }

        public ActionResult ManagerFlights()
        {
            return View(GetFlights().ToList());
        }
    }
}