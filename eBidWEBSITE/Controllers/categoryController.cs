using eBidWEBSITE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBidWEBSITE.Controllers
{
    public class categoryController : Controller
    {
        ebidEntities db = new ebidEntities();
        // GET: category



        public ActionResult displayCategory()
        {
            var query = db.tblCategories.ToList();

            return View(query);
        }




       

        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(tblCategory c)
        {
            if (ModelState.IsValid)
            {

                tblCategory cat = new tblCategory();
                cat.CategoryName = c.CategoryName;
                db.tblCategories.Add(cat);
                db.SaveChanges();
                return RedirectToAction("displayCategory");

            }
            else
            {

                TempData["msg"] = "Not Inserted Category";

            }

            return View();
        }











        public ActionResult Edit(int id)
        {

            var query = db.tblCategories.SingleOrDefault(m => m.CatId == id);
            return View(query);
        }

        [HttpPost]
        public ActionResult Edit(tblCategory c)

        {
            try
            {

                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("displayCategory");
            }

            catch (Exception ex)

            {
                TempData["msg"] = ex;
            }

            return RedirectToAction("displayCategory");
        }

        

        public ActionResult Delete(int id)

        {

            var query = db.tblCategories.SingleOrDefault(m => m.CatId == id);
            db.tblCategories.Remove(query);
            db.SaveChanges();
            return RedirectToAction("displayCategory");

        }



  

    }
}