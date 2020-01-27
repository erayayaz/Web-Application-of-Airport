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
    public class ManagerPageController : Controller
    {
        public List<AirportManager> GetManagers()
        {
            List<AirportManager> managers = new List<AirportManager>();
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

            string cmdString = "SELECT * FROM Manager";
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            SqlDataReader nwReader = cmd.ExecuteReader();
           
            while (nwReader.Read())
            {
                AirportManager manager = new AirportManager();
                manager.ManagerEmployeeID = (int)nwReader["ManagerEmployeeID"];
                manager.Department = (string)nwReader["Department"];
                manager.Password = (string)nwReader["ManagerPassword"];
                managers.Add(manager);
            }

            return managers;
        }
        // GET: ManagerPage
        public ActionResult ManagerPage(int managerID)
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
            string cmdString1 = "PersonInfo";
            SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@managerID", managerID);
            SqlDataReader sqlDataReader = cmd1.ExecuteReader();

            while (sqlDataReader.Read())
            {
                ViewBag.Name = (string)sqlDataReader["Name"];
            }

            return View(GetManagers().ToList());
        }

        public ActionResult LogOut()
        {
            return View();
        }
    }
}