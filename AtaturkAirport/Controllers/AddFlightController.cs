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
    public class AddFlightController : Controller
    {
        [HttpPost]
        public ActionResult AddCargoFlight(AddCargoFlight flight)
        {
            string conString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Flight VALUES('" + flight.FlightID + "', '" + flight.FlightDate + "', '" + flight.FlightTime + "', '" + flight.GoesTo + "', '" + flight.ComesFrom + "', '" + flight.FAirportID + "', " + flight.FPilotID + ")";
            cmd.Connection = connection;
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Response.Write("error" + ex.ToString());
                connection.Close();
                return null;
            }

            
            string cmdString1 = "INSERT INTO Cargo_Flight VALUES('" + flight.FlightID + "', " + flight.numberOfCargo + ")";
            SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
            try
            {
                connection.Open();
                cmd1.ExecuteNonQuery();
                connection.Close();
                ViewBag.Message = "Cargo Flight added successfully";
            }
            catch (Exception ex)
            {
                Response.Write("error" + ex.ToString());
                connection.Close();
                return null;
            }

            return View();
        }
        [HttpGet]
        public ActionResult AddCargoFlight()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPassengerFlight(AddPassengerFlight flight)
        {
            string conString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(conString);
            string cmdString = "INSERT INTO Flight VALUES('" + flight.FlightID + "', '" + flight.FlightDate + "', '" + flight.FlightTime + "', '" + flight.GoesTo + "', '" + flight.ComesFrom + "', '" + flight.FAirportID + "', " + flight.FPilotID + ")" ;
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Response.Write("error" + ex.ToString());
                connection.Close();
                return null;
            }

            string cmdString1 = "INSERT INTO Passenger_Flight VALUES('" + flight.FlightID + "', " + flight.NumberOfSeat + ", " + flight.NumberOfCabinAttendant + ")";
            SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
            try
            {
                connection.Open();
                cmd1.ExecuteNonQuery();
                connection.Close();
                ViewBag.Message = "Passenger Flight added successfully";
            }
            catch (Exception ex)
            {
                Response.Write("error" + ex.ToString());
                connection.Close();
                return null;
            }

            return View();
        }
        [HttpGet]
        public ActionResult AddPassengerFlight()
        {
            return View();
        }

        // GET: AddFlight
        public ActionResult Selection()
        {
            return View();
        }
    }
}