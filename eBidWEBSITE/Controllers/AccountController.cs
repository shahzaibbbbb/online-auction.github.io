using eBidWEBSITE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eBidWEBSITE.Controllers
{
    public class AccountController : Controller
    {
        ebidEntities db = new ebidEntities();
        // GET: Account



        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(tblUser t , HttpPostedFileBase img)
        {

            tblUser u = new tblUser();

            if (ModelState.IsValid)

            {

                if (img.ContentLength > 0)
                {
                    img.SaveAs(Server.MapPath("~/profile/" + img.FileName));
                    t.PROFILE = img.FileName;

                }


                u.Name = t.Name;
                u.Email = t.Email;
                u.Password = t.Password;
                u.PROFILE = t.PROFILE;
                u.RoleType = 2;
                db.tblUsers.Add(u);
                db.SaveChanges();

                return RedirectToAction("Login", "Account");
            }

            else

            {

                TempData["msg"] = "Not Register!!";

            }
            return View();
        }



     

        public ActionResult Login()

        {

            return View();

        }


        [HttpPost]

        public ActionResult Login(tblUser t)

        {

            var query = db.tblUsers.SingleOrDefault(m => m.Email == t.Email && m.Password == t.Password);
            if (query != null)

            {
                if (query.RoleType == 1)

                {
                    Session["Name"] = query.Name;
                    Session["uid"] = query.UserId;
                    Session["Roletype"] = query.RoleType;
                    Session["userpic"] = query.PROFILE;
                    FormsAuthentication.SetAuthCookie(query.Email, false);
                    Session["User"] = query.Name;
                    return RedirectToAction("Index", "Home");
                }

                else if (query.RoleType == 2)

                {

                    Session["uid"] = query.UserId;
                    Session["Roletype"] = query.RoleType;
                    Session["userpic"] = query.PROFILE;
                    FormsAuthentication.SetAuthCookie(query.Email, false);
                    Session["User"] = query.Name;
                    return RedirectToAction("Index", "Home");

                }

            }
            else

            {

                TempData["msg"] = "Invalid Username or Password";

            }

            return View();
        }

       

        public ActionResult logout()

        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }

    }
}















