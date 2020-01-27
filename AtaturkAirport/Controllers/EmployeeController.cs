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
    public class EmployeeController : Controller
    {
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
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

            string cmdString = "SELECT * FROM Employee";
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            SqlDataReader nwReader = cmd.ExecuteReader();
            while (nwReader.Read())
            {
                Employee employee = new Employee();
                employee.employeeID = (int)nwReader["EmployeeID"];
                employee.salary = (string)nwReader["Salary"].ToString();
                employee.employeeTCKN = (string)nwReader["EmployeeTCKN"].ToString();
                employee.hireDate = (string)nwReader["HireDate"].ToString();
                employees.Add(employee);
            }

            return employees;
        }
        [HttpGet]
        public ActionResult SearchEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchEmployee(Employee employee)
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
            if (employee.employeeID > 0)
            {
                string cmdString1 = "SearchSalary";
                SqlCommand cmd1 = new SqlCommand(cmdString1, connection);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@employeeID", employee.employeeID);
                SqlDataReader sqlDataReader = cmd1.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    ViewBag.EmployeeID = (int)sqlDataReader["EmployeeID"];
                    ViewBag.TCKN = (string)sqlDataReader["EmployeeTCKN"].ToString();
                    ViewBag.salary = (string)sqlDataReader["Salary"].ToString();
                    ViewBag.hireDate = (string)sqlDataReader["HireDate"].ToString();

                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult SearchCertificate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchCertificate(Employee employee)
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

            string cmdString = "SELECT * FROM Employee_Certificate WHERE Employee_Certificate.CEmployeeID =" + employee.employeeID;
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            SqlDataReader nwReader = cmd.ExecuteReader();
            while (nwReader.Read())
            {
                ViewBag.EmployeeID = (int)nwReader["CEmployeeID"];
                ViewBag.CertificateNumber = (int)nwReader["CertificateNumber"];
                ViewBag.Date = (string)nwReader["DateCompleted"].ToString();
            }
            return View();
        }
        // GET: Employee
        public ActionResult Employees()
        {
            return View(GetEmployees().ToList());
        }
    }
}