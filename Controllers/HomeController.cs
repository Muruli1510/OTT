using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    //[Authorize(Users = "admin")]
    public class HomeController : Controller
    {
        static string messsage = "";
        static string message1 = "";
        static string err = "";

        [HttpGet]
        public ActionResult Index()
        {
            List<UploadClass1> videolist = new List<UploadClass1>();
            string mainconn = ConfigurationManager.ConnectionStrings["defaultconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select * from Movie_content";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            SqlDataReader srd = sqlcomm.ExecuteReader();
            while (srd.Read())
            {
                UploadClass1 uc = new UploadClass1();
                uc.Movie_name = srd["Movie_name"].ToString();
                uc.Video_path = srd["Video_path"].ToString();
                videolist.Add(uc);
            }

            return View(videolist);


        }
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase videofile, string video_type)
        {
            if (videofile != null)
            {
                string filename = Path.GetFileName(videofile.FileName);
                if (videofile.ContentLength < 104857600)
                {
                    videofile.SaveAs(Server.MapPath("/VideoFiles/" + filename));
                    string mainconn = ConfigurationManager.ConnectionStrings["defaultconnection"].ConnectionString;
                    SqlConnection sqlconn = new SqlConnection(mainconn);
                    string sqlquery = "insert into Movie_content values(@Vname,@Vtype,@Vpath)";
                    SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                    sqlconn.Open();
                    sqlcomm.Parameters.AddWithValue("@Vname", filename);
                    sqlcomm.Parameters.AddWithValue("@Vtype", video_type);
                    sqlcomm.Parameters.AddWithValue("@Vpath", "/VideoFiles/" + filename);
                    int i = sqlcomm.ExecuteNonQuery();
                    sqlconn.Close();
                    if (i == 0)
                    {
                        ViewBag.message1 = "Oops error.. try again!";

                    }
                    ViewBag.message = "Video uploaded succesfully!";
                }
            }
            else
            {
                ViewBag.err = "Please select a file...";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "OTT";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";

            return View();
        }
    }
}