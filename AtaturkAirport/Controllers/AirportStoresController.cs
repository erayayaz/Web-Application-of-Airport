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
    public class AirportStoresController : Controller
    {
        // GET: AirportStores
        public List<AirportStores> GetAirportStores()
        {
            List<AirportStores> airports = new List<AirportStores>();
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

            string cmdString = "SELECT * FROM Airport_Store";
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            SqlDataReader nwReader = cmd.ExecuteReader();
            while (nwReader.Read())
            {
                AirportStores stores = new AirportStores();
                stores.BlockNumber = (int)nwReader["BlockNumber"];
                stores.Name = (string)nwReader["name"];
                stores.ContractDate = (string)nwReader["ContractDate"].ToString();
                stores.StoreAirportID = (string)nwReader["StoreAirportID"];
                airports.Add(stores);
            }

            return airports;
        }
        [HttpGet]
        public ActionResult SearchStore()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchStore(AirportStores store)
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

            if (store.Name != null)
            {
                string cmdString1 = "SearchStore";
                SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@name", store.Name);
                SqlDataReader sqlDataReader = cmd1.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    ViewBag.BlockNumber = (int)sqlDataReader["BlockNumber"];
                    ViewBag.Name = (string)sqlDataReader["name"];
                }
            }


            return View();
        }
        [HttpGet]
        public ActionResult ManagerSearchStore()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ManagerSearchStore(AirportStores store)
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

            if (store.Name != null)
            {
                string cmdString1 = "SearchStore";
                SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@name", store.Name);
                SqlDataReader sqlDataReader = cmd1.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    ViewBag.BlockNumber = (int)sqlDataReader["BlockNumber"];
                    ViewBag.Name = (string)sqlDataReader["name"];
                }
            }


            return View();
        }
        public ActionResult ManagerAirportStores()
        {
            return View(GetAirportStores().ToList());
        }
        public ActionResult AirportStores()
        {
            return View(GetAirportStores().ToList());
        }
    }
}