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
    public class PassengerController : Controller
    {
        public List<PassengerInfo> GetPassengers()
        {
            List<PassengerInfo> passengers = new List<PassengerInfo>();
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

            string cmdString = "SELECT * FROM Passenger INNER JOIN Person ON Passenger.PassengerTCKN = Person.TCKN";
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            SqlDataReader nwReader = cmd.ExecuteReader();
            while (nwReader.Read())
            {
                PassengerInfo passenger = new PassengerInfo();
                passenger.TCKN = (string)nwReader["PassengerTCKN"].ToString();
                passenger.gender = (string)nwReader["Gender"].ToString();
                passenger.luggageWeight = (int)nwReader["LuggageWeight"];
                passenger.name = (string)nwReader["name"];
                passenger.city = (string)nwReader["City"];
                passengers.Add(passenger);
            }

            return passengers;
        }

        [HttpGet]
        public ActionResult SearchTicket()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchTicket(PassengerInfo passenger)
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

            if (passenger.TCKN != null)
            {
                string cmdString1 = "TicketInfo";
                SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@TCKN", passenger.TCKN);
                SqlDataReader sqlDataReader = cmd1.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    ViewBag.TCKN = sqlDataReader["PassengerTCKN"];
                    ViewBag.gender = (string)sqlDataReader["Gender"].ToString();
                    ViewBag.luggageWeight = (int)sqlDataReader["LuggageWeight"];
                    ViewBag.FlightID = (string)sqlDataReader["PFlightID"];
                    ViewBag.TicketID = (string)sqlDataReader["TicketID"];
                    ViewBag.Name = (string)sqlDataReader["name"];
                    ViewBag.price = sqlDataReader["price"];
                    ViewBag.type = (string)sqlDataReader["TicketType"];
                }
            }


            return View();
        }
        // GET: Passenger
        public ActionResult Passengers()
        {
            return View(GetPassengers().ToList());
        }
    }
}