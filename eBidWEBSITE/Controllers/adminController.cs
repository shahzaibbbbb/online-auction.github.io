using eBidWEBSITE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBidWEBSITE.Controllers
{
    public class adminController : Controller
    {
        ebidEntities db = new ebidEntities();
        // GET: admin


        public ActionResult Admindashboard()

        {

            return View();

        }





        public ActionResult userdashboard()

        {

            return View();

        }




    }
}