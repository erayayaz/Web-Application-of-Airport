using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AtaturkAirport.Models;

namespace AtaturkAirport.Controllers
{
    public class CarParkController : Controller
    {
        // GET: CarPark
        public List<CarPark> GetCar()
        {
            List<CarPark> parks = new List<CarPark>();
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

            string cmdString = "SELECT * FROM Car_Park";
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            SqlDataReader nwReader1 = cmd.ExecuteReader();
            while (nwReader1.Read())
            {
                CarPark park = new CarPark();
                park.SpaceNumber = (int)nwReader1["SpaceNumber"];
                park.ArrivalTime = (string)nwReader1["ArrivalTime"].ToString();
                park.LeavingTime = (string)nwReader1["LeavingTime"].ToString();
                park.ParkingLotAirportID = (string)nwReader1["ParkingLotAirportID"];
                parks.Add(park);
            }

            return parks;
        }
        [HttpPost]
        public ActionResult SearchForSpaceNumber(Car car)
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

           if(car.PlateNumber != null)
            {
                string cmdString1 = "CarDetail";
                SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@platenumber", car.PlateNumber);
                SqlDataReader sqlDataReader = cmd1.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    ViewBag.PlateNumber = (string)sqlDataReader["PlateNumber"];
                    ViewBag.SpaceNumber = (int)sqlDataReader["SpaceNumber"];
                    
                }
            }
             

            return View();
            

        }

        [HttpGet]
        public ActionResult SearchForSpaceNumber()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ManagerSearchForSpaceNumber(Car car)
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

            if (car.PlateNumber != null)
            {
                string cmdString1 = "CarDetail";
                SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@platenumber", car.PlateNumber);
                SqlDataReader sqlDataReader = cmd1.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    ViewBag.PlateNumber = (string)sqlDataReader["PlateNumber"];
                    ViewBag.SpaceNumber = (int)sqlDataReader["SpaceNumber"];

                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult ManagerSearchForSpaceNumber()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CalculatePrice(Car car)
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

            if (car.PlateNumber != null)
            {
                string cmdString1 = "CalculatePrice";
                SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@PlateNumber", car.PlateNumber);
                SqlDataReader sqlDataReader = cmd1.ExecuteReader();
                int i = 0;
                while (i == 0)
                {   
                    
                    string cmdString = "SELECT * FROM Car WHERE Car.PlateNumber = '" + car.PlateNumber + "'";
                    SqlCommand cmd = new SqlCommand(cmdString, connection);
                    sqlDataReader.Close();
                    SqlDataReader nwReader1 = cmd.ExecuteReader();
                    while (nwReader1.Read())
                    {
                        CarPark park = new CarPark();
                        ViewBag.Price = nwReader1["HourlyPrice"];
                        i = 1;
                    }
                }
            }


            return View();
        }
        [HttpGet]
        public ActionResult CalculatePrice()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ManagerCalculatePrice(Car car)
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

            if (car.PlateNumber != null)
            {
                string cmdString1 = "CalculatePrice";
                SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@PlateNumber", car.PlateNumber);
                SqlDataReader sqlDataReader = cmd1.ExecuteReader();
                int i = 0;
                while (i == 0)
                {

                    string cmdString = "SELECT * FROM Car WHERE Car.PlateNumber = '" + car.PlateNumber + "'";
                    SqlCommand cmd = new SqlCommand(cmdString, connection);
                    sqlDataReader.Close();
                    SqlDataReader nwReader1 = cmd.ExecuteReader();
                    while (nwReader1.Read())
                    {
                        CarPark park = new CarPark();
                        ViewBag.Price = nwReader1["HourlyPrice"];
                        i = 1;
                    }
                }
            }


            return View();
        }
        [HttpGet]
        public ActionResult ManagerCalculatePrice()
        {
            return View();
        }

        public ActionResult Features()
        {
            return View(GetCar().ToList());
        }

        public ActionResult ManagerFeatures()
        {
            return View(GetCar().ToList());
        }

    }
}