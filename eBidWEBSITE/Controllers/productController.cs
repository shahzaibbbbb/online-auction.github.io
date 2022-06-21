using eBidWEBSITE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBidWEBSITE.Controllers
{
    public class productController : Controller
    {
        ebidEntities db = new ebidEntities();
        // GET: product


        public ActionResult index()

        {
            return View();
        }
    }
}



//        public ActionResult displayproduct( )
//        {
//            int rt = Convert.ToInt32(Session["Roletype"]);



//                if (rt == 1)
//                {

//                    var query = db.tblProducts.ToList();
//                    return View(query);
//                }
//                else
//                {
//                    int id = Convert.ToInt32(Session["uid"]);
//                    var query = db.tblProducts.Where(x => x.uid == id).ToList();
//                    return View(query);
//                }
//            }










//        public ActionResult addproduct()
//        {
//            List<tblCategory> list = db.tblCategories.ToList();
//            ViewBag.CatList = new SelectList(list, "CatId", "CategoryName");
//            return View();
//        }



//        [HttpPost]
//        public ActionResult addproduct(tblProduct p, HttpPostedFileBase Image)
//        {

//            int id = Convert.ToInt32(Session["uid"]);

//            List<tblCategory> list = db.tblCategories.ToList();
//            ViewBag.CatList = new SelectList(list, "CatId", "CategoryName");


//            if (ModelState.IsValid)
//            {


//                tblProduct pro = new tblProduct();
//                pro.pro_Name = p.pro_Name;
//                pro.pro_Description = p.pro_Description;
//                pro.pro_price = p.pro_price;
//                pro.pro_Image = Image.FileName.ToString();
//                pro.CatId = p.CatId;
//                pro.uid = id;


//                var folder = Server.MapPath("~/productUploads/");
//                Image.SaveAs(Path.Combine(folder, Image.FileName.ToString()));

//                db.tblProducts.Add(pro);
//                db.SaveChanges();

//                return RedirectToAction("displayproduct");
//            }
//            else
//            {
//                TempData["msg"] = "Product Not Upload";
//            }
//            return View();
//        }





//        public ActionResult Edit(int id)
//        {

//            List<tblCategory> list = db.tblCategories.ToList();
//            ViewBag.CatList = new SelectList(list, "CatId", "Name");

//            var query = db.tblProducts.SingleOrDefault(m => m.ProID == id);

//            return View(query);
//        }


//        [HttpPost]
//        public ActionResult Edit(tblProduct p, HttpPostedFileBase Image)
//        {
//            List<tblCategory> list = db.tblCategories.ToList();
//            ViewBag.CatList = new SelectList(list, "CatId", "Name");

//            try
//            {

//                p.pro_Image = Image.FileName.ToString();
//                var folder = Server.MapPath("~/Uploads/");
//                Image.SaveAs(Path.Combine(folder, Image.FileName.ToString()));
//                db.Entry(p).State = EntityState.Modified;
//                db.SaveChanges();

//            }
//            catch (Exception ex)
//            {
//                TempData["msg"] = ex;
//            }

//            return RedirectToAction("displayproduct");

//        }


//        public ActionResult Delete(int id)
//        {
//            var query = db.tblProducts.SingleOrDefault(m => m.ProID == id);
//            db.tblProducts.Remove(query);

//            db.SaveChanges();


//            return RedirectToAction("displayproduct");

//        }



//    }
//}


