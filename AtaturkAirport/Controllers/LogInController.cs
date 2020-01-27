using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AtaturkAirport.Models;

namespace AtaturkAirport.Controllers 
{ 
    public class LogInController : Controller
    {
        [HttpPost]
        public ActionResult LogIn(AirportManager manager)
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
            DataSet ds = new DataSet();
            string cmdString = "SELECT * FROM Manager WHERE ManagerEmployeeID = " + manager.ManagerEmployeeID;
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            SqlDataReader nwReader1 = cmd.ExecuteReader();
            if (nwReader1.Read())
            {
                if (manager.Password == nwReader1["ManagerPassword"].ToString())
                {
                    return RedirectToAction("ManagerPage", "ManagerPage", new { managerID = manager.ManagerEmployeeID });
                }
            }
            return View();
        }

        // GET: LogIn
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
    }
}