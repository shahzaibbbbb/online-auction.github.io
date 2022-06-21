using eBidWEBSITE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBidWEBSITE.Controllers
{
    public class BIDcategoryController : Controller
    {

        ebidEntities db = new ebidEntities();
        // GET: BIDcategory











        public ActionResult displayBIDCategory()
        {
            var query = db.tblbidcategories.ToList();

            return View(query);
        }






        public ActionResult CreateBIDCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBIDCategory(tblbidcategory c)
        {
            if (ModelState.IsValid)
            {

                tblbidcategory cat = new tblbidcategory();
                cat.Bidcatname = c.Bidcatname;
                db.tblbidcategories.Add(cat);
                db.SaveChanges();
                return RedirectToAction("displayBIDCategory");

            }
            else
            {

                TempData["msg"] = "Not Inserted Category";

            }

            return View();
        }





        public ActionResult Edit(int id)
        {

            var query = db.tblbidcategories.SingleOrDefault(m => m.BidcatId == id);
            return View(query);
        }

        [HttpPost]
        public ActionResult Edit(tblbidcategory c)

        {
            try
            {

                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("displayBIDCategory", "BIDcategory");
            }

            catch (Exception ex)

            {
                TempData["msg"] = ex;
            }

            return RedirectToAction("displayBIDCategory" ,"BIDcategory");
        }









        public ActionResult Delete(int id)

        {

            var query = db.tblbidcategories.SingleOrDefault(m => m.BidcatId == id);
            db.tblbidcategories.Remove(query);
            db.SaveChanges();
            return RedirectToAction("displayCategory");

        }





    }
}
























