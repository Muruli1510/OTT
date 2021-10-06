using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MVCUploadController : Controller
    {
        // GET: MVCUpload
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
            while(srd.Read())
            {
                UploadClass1 uc = new UploadClass1();
                uc.Movie_name = srd["Movie_name"].ToString();
                uc.Video_path = srd["Video_path"].ToString();
                videolist.Add(uc);
            }

            return View(videolist);
        }
        //[HttpPost]
        //public ActionResult Index(HttpPostedFileBase videofile, int no)
        //{
        //    if (videofile != null)
        //    {
        //        string filename = Path.GetFileName(videofile.FileName);
        //        if (videofile.ContentLength < 104857600)
        //        {
        //            videofile.SaveAs(Server.MapPath("/VideoFiles/"+filename));
        //            string mainconn = ConfigurationManager.ConnectionStrings["defaultconnection"].ConnectionString;
        //            SqlConnection sqlconn = new SqlConnection(mainconn);
        //            string sqlquery = "insert into videofiles values(@Vno,@Vname,@Vpath)";
        //            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
        //            sqlconn.Open();
        //            sqlcomm.Parameters.AddWithValue("@Vno", no);
        //            sqlcomm.Parameters.AddWithValue("@Vname", filename);
        //            sqlcomm.Parameters.AddWithValue("@Vpath", "/VideoFiles/"+filename);
        //            int i= sqlcomm.ExecuteNonQuery();
        //            sqlconn.Close();
        //            if(i!=0)
        //            {
        //                ViewBag.message= "Record saved succesfully!";
        //            }
        //            ViewBag.message1 = "Oops error.. try again!";
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.err = "Please select a file...";
        //    }
        //    return RedirectToAction("Index");
        //}
      
    }
}