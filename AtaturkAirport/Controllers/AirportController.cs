using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using AtaturkAirport.Models;

namespace AtaturkAirport.Controllers
{
    public class AirportController : Controller
    {

        public List<AirportModel> GetAirports()
        {
            List<AirportModel> airports = new List<AirportModel>();
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

            string cmdString = "SELECT * FROM Airport";
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            SqlDataReader nwReader = cmd.ExecuteReader();
            while (nwReader.Read())
            {
                AirportModel air = new AirportModel();
                air.ID = (string)nwReader["AirportID"];
                air.Name = (string)nwReader["name"];
                air.City = (string)nwReader["City"];
                airports.Add(air);
            }

            return airports;
        }

        public AirportModel Item(string ID)
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

            string cmdString = "SELECT * FROM Airport Where AirportID = '" + ID + "'";
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            SqlDataReader nwReader = cmd.ExecuteReader();
            AirportModel air = new AirportModel();
            while (nwReader.Read())
            {
                air.ID = (string)nwReader["AirportID"];
                air.Name = (string)nwReader["name"];
                air.City = (string)nwReader["City"];
            }

            return air;
        }

        public ActionResult ManagerAirport()
        {
            return View(GetAirports().ToList());
        }

        // GET: Home
        public ActionResult Airport()
        {
            return View(GetAirports().ToList());
        }

        public ActionResult Show(string ID)
        {
            if (ID == null)
                ID = "IST";
            return View(Item(ID));
        }
    }
}