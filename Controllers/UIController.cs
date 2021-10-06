using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UIController : Controller
    {
        static string accountmsg = "";
        static string accountmsg1 = "";
        static string mailid = "";
        static string message = "";
        // GET: UI
        public ActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Registration()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Registration(Registration details)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["defaultconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "insert into Customer values(@email,@firstname,@lastname,@password)";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@email", details.Email);
            sqlcomm.Parameters.AddWithValue("@firstname", details.FirstName);
            sqlcomm.Parameters.AddWithValue("@lastname", details.LastName);
            sqlcomm.Parameters.AddWithValue("@password", details.Password);
            int i = sqlcomm.ExecuteNonQuery();
            sqlconn.Close();
            if (i == 0)
            {
                ViewBag.accoutmsg = "Oops error.. try again!";

            }
            ViewBag.accountmsg1 = "Account Created. Please Login";
            sqlquery = "insert into master_table values(@email,@name,@password,@role)";
            SqlCommand sqlcomm1 = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            sqlcomm1.Parameters.AddWithValue("@email", details.Email);
            sqlcomm1.Parameters.AddWithValue("@name", details.FirstName+"."+details.LastName);
            sqlcomm1.Parameters.AddWithValue("@password", details.Password);
            sqlcomm1.Parameters.AddWithValue("@role", "user");
            sqlcomm1.ExecuteNonQuery();
            sqlconn.Close();
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login data)
        {
            ViewBag.mailid = data.email;
            string mainconn = ConfigurationManager.ConnectionStrings["defaultconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select * from dbo.Master_table where email=@email and password=@password";
            string sqladmincheck = "select role from dbo.Master_table where email=@email and role='admin' ";

            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@email", data.email);
            sqlcomm.Parameters.AddWithValue("@password", data.password);
            SqlDataReader srd = sqlcomm.ExecuteReader();
            if (srd.HasRows)
            {
                int timeout = data.rememberme ? 60 : 5; /* Timeout in minutes, 60 = 1 hour.*/

                var ticket = new FormsAuthenticationTicket(data.email, false, timeout);

                string encrypted = FormsAuthentication.Encrypt(ticket);

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);

                cookie.Expires = System.DateTime.Now.AddMinutes(timeout);

                cookie.HttpOnly = true;

                Response.Cookies.Add(cookie);
                SqlCommand sqlcommm = new SqlCommand(sqladmincheck, sqlconn);
                srd.Close();
                sqlcommm.Parameters.AddWithValue("@email", data.email);
                SqlDataReader sda = sqlcommm.ExecuteReader();
                if (sda.HasRows)
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    return RedirectToAction("Index","MVCUpload");
                }
            }
            else
            {
                ViewBag.message = "Invalid Login";
                return View();
            }

            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            //this.ControllerContext.HttpContext.Response.Cookies.Clear();
            //DateTime end = cookie.Expires;
            //DateTime start = DateTime.Now;
            //Response.Write((end - start).TotalMinutes);
            return RedirectToAction("Login");
        }
    }
}