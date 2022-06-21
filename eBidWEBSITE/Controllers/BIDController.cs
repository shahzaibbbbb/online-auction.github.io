using eBidWEBSITE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBidWEBSITE.Controllers
{
    public class BIDController : Controller
    {
        ebidEntities db = new ebidEntities();

        List<Cart> li = new List<Cart>();






        // GET: BID


















        public ActionResult displaybid()
        {
            int rt = Convert.ToInt32(Session["Roletype"]);

            if (rt == 1)
            {

                var query = db.tblBids.ToList();
                return View(query);
            }
            else
            {
                int id = Convert.ToInt32(Session["uid"]);
                var query = db.tblBids.Where(x => x.uid == id).ToList();
                return View(query);
            }

        }








        public ActionResult addbid()
        {
            List<tblbidcategory> list = db.tblbidcategories.ToList();
            ViewBag.CatList = new SelectList(list, "BidcatId", "Bidcatname");
            return View();
        }



        [HttpPost]
        public ActionResult addbid(tblBid b, HttpPostedFileBase Image)
        {
            List<tblbidcategory> list = db.tblbidcategories.ToList();
            ViewBag.Catlist = new SelectList(list, "BidcatId", "Bidcatname");

            int id = Convert.ToInt32(Session["uid"]);
            if (ModelState.IsValid)
            {


                tblBid bidding = new tblBid();
                bidding.bid_Name = b.bid_Name;
                bidding.bid_Description = b.bid_Description;
                bidding.bid_MINamount = b.bid_MINamount;
                bidding.bid_Image = Image.FileName.ToString();



                




                bidding.bid_endTime = b.bid_endTime;




                bidding.CatId = b.CatId;   
                bidding.uid = id;


                var folder = Server.MapPath("~/bidsUploads/");
                Image.SaveAs(Path.Combine(folder, Image.FileName.ToString()));

                db.tblBids.Add(bidding);
                db.SaveChanges();

                return RedirectToAction("displaybid");
            }
            else
            {
                TempData["msg"] = "Product Not Upload";
            }
            return View();
        }







        


           public ActionResult Delete(int id)
           {
               var query = db.tblBids.SingleOrDefault(m => m.BidID == id);
               db.tblBids.Remove(query);

               db.SaveChanges();


               return RedirectToAction("displayproduct");

           }

















        public ActionResult AddtoCart(int id)
        {
            var query = db.tblBids.Where(x => x.BidID == id).SingleOrDefault();
            return View(query);

        }

        [HttpPost]
        public ActionResult AddtoCart(int id, int bidding)
        {

            tblBid p = db.tblBids.Where(x => x.BidID == id).SingleOrDefault();
            Cart c = new Cart();
            c.BidID = id;
            c.bid_Name = p.bid_Name;
            c.bid_MINamount = Convert.ToInt32(p.bid_MINamount);
            c.bidding = Convert.ToInt32(bidding);
            c.bill = c.bidding;

            if (TempData["cart"] == null)
            {

                li.Add(c);
                TempData["cart"] = li;

            }

            else
            {

                List<Cart> li2 = TempData["cart"] as List<Cart>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.BidID == c.BidID)
                    {
                        item.bidding += c.bidding;
                        item.bill += c.bill;
                        flag = 1;
                    }
                }

                if (flag == 0)

                {
                    li2.Add(c);

                }

                TempData["cart"] = li2;
            }

            TempData.Keep();
            return RedirectToAction("Index");
        }



        public ActionResult removecart(int? id)
        {

            if (TempData["cart"] == null)

            {
                TempData.Remove("total");

                TempData.Remove("cart");
            }

            else

            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;

                Cart c = li2.Where(x => x.BidID == id).SingleOrDefault();

                li2.Remove(c);
                int s = 0;

                foreach (var item in li2)

                {

                    s += item.bill;
                }

                TempData["total"] = s;

            }

            return View();
        }



        






       public ActionResult Checkout()
       {
           TempData.Keep();
           return View();
       }

       [HttpPost]
       public ActionResult Checkout(string contact, string address)
       {

           if (ModelState.IsValid)

           {
               List<Cart> li2 = TempData["cart"] as List<Cart>;
               tblInvoice iv = new tblInvoice();
               iv.UserId = Convert.ToInt32(Session["uid"].ToString());
               iv.InvoiceDate = System.DateTime.Now;
               iv.Bill = (int)TempData["total"];
               iv.Payment = "cash";
               db.tblInvoices.Add(iv);
               db.SaveChanges();



               foreach (var item in li2)

               {

                   tblOrder od = new tblOrder();
                   od.bidid = item.BidID;
                   od.Contact = contact;
                   od.Address = address;
                   od.OrderDate = System.DateTime.Now;
                   od.InvoiceId = iv.InvoiceId;
                
                   od.Unit = item.bid_MINamount;
                   od.bidding = item.bidding;
                   od.Total = item.bill;
                   db.tblOrders.Add(od);
                   db.SaveChanges();

               }

               TempData.Remove("total");
               TempData.Remove("cart");
               TempData["msg"] = "Order Book Successfully!!";
               return RedirectToAction("Index");
           }

           TempData.Keep();
           return View();
       }

































    }
}