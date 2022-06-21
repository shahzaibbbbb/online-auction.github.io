using eBidWEBSITE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBidWEBSITE.Controllers
{
    public class HomeController : Controller
    {
        ebidEntities db = new ebidEntities();

        List<Cart> li = new List<Cart>();

        // GET: Home
        public ActionResult Index()
        {
            if (TempData["cart"] != null)
            {
                int x = 0;

                List<Cart> li2 = TempData["cart"] as List<Cart>;
                foreach (var item in li2)
                {
                    x += item.bill;

                }
                TempData["total"] = x;
                TempData["item_count"] = li2.Count();
            }

            TempData.Keep();
            var query = db.tblBids.ToList();
            return View(query);
        }










        public ActionResult PRODUCTPAGE()
        {

            var query = db.tblBids.ToList();
            return View(query);

        }
        [HttpPost]
        public ActionResult PRODUCTPAGE(string searchtxt )
        {
           
            var query = db.tblBids.ToList();

            if(searchtxt != null)
            {
               query = db.tblBids.Where(x => x.bid_Name == searchtxt).ToList();
            }

           





            //return RedirectToAction("PRODUCTPAGE", "Home");
            return View(query);
        }






        public ActionResult BIDPAGE()
        {

            var query = db.tblBids.ToList();
            return View(query);

        }


        
        public ActionResult AboutPAGE()
        {

            return View();

        }


        public ActionResult contactPAGE()
        {
            return View();
        }
        [HttpPost]
        public ActionResult contactPAGE(tblContact c)
        {

            tblContact u = new tblContact();
            if (ModelState.IsValid)

            {

             
            u.Contact_name = c.Contact_name;
            u.Contact_subject = c.Contact_subject;
            u.Contact_email = c.Contact_email;
            u.Contact_phone = c.Contact_phone;
            u.Contact_message = c.Contact_message;
                db.tblContacts.Add(u);
                db.SaveChanges();

                return RedirectToAction("contactPAGE", "Home");
        }

            else

            {

            TempData["msg"] = "Message Not send!!";

            }
            return View();


}


        public ActionResult contactusdisplay()
        {
            var query = db.tblContacts.ToList();
            return View(query);
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


                    od.Qty = item.QTY;


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


































        //     public ActionResult AddtoCart(int id)
        //     {
        //         var query = db.tblProducts.Where(x => x.ProID == id).SingleOrDefault();
        //         return View(query);

        //     }

        //     [HttpPost]
        //     public ActionResult AddtoCart(int id, int qty)
        //     {

        //         tblProduct p = db.tblProducts.Where(x => x.ProID == id).SingleOrDefault();
        //         Cart c = new Cart();
        //         c.proid = id;
        //         c.proname = p.pro_Name;
        //         c.price = Convert.ToInt32(p.pro_price);
        //         c.qty = Convert.ToInt32(qty);
        //         c.bill = c.price * c.qty;

        //         if (TempData["cart"] == null)
        //         {

        //             li.Add(c);
        //             TempData["cart"] = li;

        //         }

        //         else
        //         {

        //             List<Cart> li2 = TempData["cart"] as List<Cart>;
        //             int flag = 0;
        //             foreach (var item in li2)
        //             {
        //                 if (item.proid == c.proid)
        //                 {
        //                     item.qty += c.qty;
        //                     item.bill += c.bill;
        //                     flag = 1;
        //                 }
        //             }

        //             if (flag == 0)

        //             {
        //                    li2.Add(c);

        //             }

        //             TempData["cart"] = li2;
        //         }

        //             TempData.Keep();
        //         return RedirectToAction("Index");
        //}











        //   public ActionResult removecart(int? id)
        //   {

        //           if (TempData["cart"] == null)

        //       {
        //                 TempData.Remove("total");

        //         TempData.Remove("cart");
        //       }

        //       else

        //       {
        //List<Cart> li2 = TempData["cart"] as List<Cart>;

        //          Cart c = li2.Where(x => x.proid == id).SingleOrDefault();

        //             li2.Remove(c);
        //           int s = 0;

        //                   foreach (var item in li2)

        //                   {

        //                      s += item.bill;
        //           }

        //              TempData["total"] = s;

        //       }

        //       return View();
        //   }



        // public ActionResult Checkout()
        // {
        //     TempData.Keep();
        //     return View();
        // }

        //[HttpPost]
        // public ActionResult Checkout(string contact,string address)
        // {

        //     if (ModelState.IsValid)

        //     {
        //         List<Cart> li2 = TempData["cart"] as List<Cart>;
        //         tblInvoice iv = new tblInvoice();
        //         iv.UserId = Convert.ToInt32(Session["uid"].ToString());
        //         iv.InvoiceDate = System.DateTime.Now;
        //         iv.Bill = (int)TempData["total"];
        //         iv.Payment = "cash";
        //         db.tblInvoices.Add(iv);
        //         db.SaveChanges();



        //         foreach (var item in li2)

        //         {

        //             tblOrder od = new tblOrder();
        //             od.ProID = item.proid;
        //             od.Contact = contact;
        //             od.Address = address;
        //             od.OrderDate = System.DateTime.Now;
        //             od.InvoiceId = iv.InvoiceId;
        //             od.Qty = item.qty;
        //             od.Unit = item.price;
        //             od.Total = item.bill;
        //             db.tblOrders.Add(od);
        //             db.SaveChanges();

        //         }

        //         TempData.Remove("total");
        //         TempData.Remove("cart");
        //         TempData["msg"] = "Order Book Successfully!!";
        //         return RedirectToAction("Index");
        //     }

        //     TempData.Keep();
        //     return View();
        // }











        public ActionResult GetAllOrderDetail()
        {





            var id = Convert.ToInt32(Session["uid"]);
            var bids = db.tblBids.Where(a => a.uid == id).FirstOrDefault();
            var bid_id = bids.BidID;
            var query = db.tblOrders.Where(a => a.bidid == bids.BidID).ToList();
            return View(query);


        }


        public ActionResult OrderDetail(int id)
        {
            var query = db.getallorderusers.Where(m => m.UserId == id).ToList();
            return View(query);
        }




        public ActionResult ConfirmOrder(int InvoiceId)
        {
            var query = db.getallorders.SingleOrDefault(m => m.InvoiceId == InvoiceId);
            return View(query);
        }

        [HttpPost]
        public ActionResult ConfirmOrder(getallorder o  )
        {
           
            
            tblInvoice inv = new tblInvoice()
            {


                InvoiceId = o.InvoiceId,
                UserId = o.UserId,
                Bill = o.Bill,
                Payment = o.Payment,
                InvoiceDate = o.InvoiceDate,
             
                Status = 1,

            };

            db.Entry(inv).State = EntityState.Modified;
            db.SaveChanges();
            return View();

        }



        public ActionResult Invoice(int id)

        {
            var query = db.user_invoices.Where(m => m.InvoiceId == id).ToList();
            return View(query);

        }








































        public ActionResult GetAllUser()
        {
            var query = db.tblUsers.ToList();
            return View(query);
        }

















    }
}