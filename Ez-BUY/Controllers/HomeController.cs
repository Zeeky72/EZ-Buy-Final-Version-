using OnlineMovieV04.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineMovieV04.Controllers
{

    //Type= "hidden"
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }
        private OnlineMovieTicketEntities db = new OnlineMovieTicketEntities();
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult LogIn(UserDetail user)
        {
            if (db.UserDetails.Any(x => x.UserName == user.UserName))
            {
                if (db.UserDetails.Any(x => x.Password == user.Password && x.UserName==user.UserName))
                {
                    Session["Username"] = user.UserName;
                    if(db.UserDetails.Any(x => x.Role == "admin" ))
                    {
                        var userDetails = db.UserDetails.ToList().Find(obj => obj.UserName.Equals(user.UserName));
                        int id = userDetails.ID;
                        Session["Role"] = userDetails.Role;
                        Session["UID"] = id;
                        FormsAuthentication.SetAuthCookie(user.UserName, false);
                       
                    }
                    else
                    {
                        Session["Role"] = "User";
                        FormsAuthentication.SetAuthCookie(user.UserName, false);
                    }


                        return RedirectToAction("Index","MovieDetails");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Password");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName");
                return View();
            }
        }
        public ActionResult SignUp()
        {
            //ViewBag.Message = "SignUp";

            return View();
        }
        [HttpPost]

        public ActionResult SignUp([Bind(Include = "ID,Name,UserName,PhoneNumber,Email,DOB,Password,Role")] UserDetail user)
        {
            if (ModelState.IsValid)
            {
               if(user.Name.Contains("1"))
                {
                    ViewBag.Message = "Only alphabets";
                    return View(user);
                }else if (user.Name.Contains("2"))
                {
                    ViewBag.Message = "Only alphabets";
                    return View(user);
                }
                else if(user.Name.Contains("3"))
                {
                    ViewBag.Message = "Only alphabets";
                    return View(user);
                }
                else if(user.Name.Contains("4"))
                {
                    ViewBag.Message = "Only alphabets";
                    return View(user);
                }
                else if(user.Name.Contains("5"))
                {
                    ViewBag.Message = "Only alphabets";
                    return View(user);
                }else if (user.Name.Contains("6"))
                {
                    ViewBag.Message = "Only alphabets";
                    return View(user);
                }
                else if (user.Name.Contains("7"))
                {
                    ViewBag.Message = "Only alphabets";
                    return View(user);
                }
                else if (user.Name.Contains("8"))
                {
                    ViewBag.Message = "Only alphabets";
                    return View(user);
                }
                else if (user.Name.Contains("9"))
                {
                    ViewBag.Message = "Only alphabets";
                    return View(user);
                }
                else if (user.Name.Contains("0"))
                {
                    ViewBag.Message = "Only alphabets";
                    return View(user);
                }
                else

                if (db.UserDetails.Any(x => x.UserName == user.UserName))
                {
                    ViewBag.Message = "User Already exist";
                    return View(user);
                }
                if (ModelState.IsValid)
                {
                    db.UserDetails.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("LogIn");
                }

                return View(user);
            }
            else
            {
                ViewBag.Message = "Only alphabets";
                return View(user);
            }

        }

        public ActionResult ForgotPassword()
        {
            Session["success"] = "bc";
            ViewBag.Message = "Reset your password here";

            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(UserDetail user)
        {
            
            if (db.UserDetails.Any(x => x.UserName == user.UserName))
            {
                Session["success"] = "abc";

                var userDetails = db.UserDetails.ToList().Find(obj=> obj.UserName.Equals(user.UserName));
                int id = userDetails.ID;
               // ViewBag.Message = userDetails.Password;

                Session["Password"] = userDetails.Password;
                int password =Convert.ToInt32( userDetails.Password);



                return View(user);
            }
            else
            {
                ViewBag.Message = "No User Found!!!!!";
                return View();
            }
            
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }
        public ActionResult TermsAndCondition()
        {
            

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "you are logged out";

            return View();
        }


        [Authorize]

        // GET: UserDetails/Edit/5
        public ActionResult UserProfile()
        {
           
            var id = Session["UID"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }
        [Authorize]

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,UserName,PhoneNumber,Email,DOB,Password,Role")] UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserProfile");
            }
            return View(userDetail);
        }

        public ActionResult ErrorControl()
        {
            return View();
        }
    }
}