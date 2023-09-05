using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using OnlineMovieV04.Models;

namespace OnlineMovieV04.Controllers
{
    [Authorize]
    public class BookingCnfsController : Controller
    {
        
        private OnlineMovieTicketEntities db = new OnlineMovieTicketEntities();
        private OnlineMovieTicketEntities bb = new OnlineMovieTicketEntities();

        // GET: BookingCnfs
        public ActionResult Index()
        {
            return View(db.BookingCnfs.ToList());
        }

        // GET: BookingCnfs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingCnf bookingCnf = db.BookingCnfs.Find(id);
            if (bookingCnf == null)
            {
                return HttpNotFound();
            }
            return View(bookingCnf);
        }

        // GET: BookingCnfs/Create
       

        // POST: BookingCnfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      
        
        public ActionResult Create([Bind(Include = "UserName,Movie,Id,Price")] BookingCnf bookingCnf)
        {
            if (ModelState.IsValid)
            {
                string mNAme= Session["MName"].ToString();
                string uNAme = Session["Username"].ToString();
               // if (db.BookingCnfs.Any(x=>x.UserName==uNAme&&x.Movie==mNAme ))
                    
                    bookingCnf.Movie = mNAme;
                    bookingCnf.UserName = uNAme;
                    bookingCnf.Price = Convert.ToInt16(Session["Price"]);
                    db.BookingCnfs.Add(bookingCnf);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                

               

            }
            RedirectToAction("Index", "MovieDetails");
            return View(bookingCnf);
        }

        // GET: BookingCnfs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingCnf bookingCnf = db.BookingCnfs.Find(id);
            if (bookingCnf == null)
            {
                return HttpNotFound();
            }
            return View(bookingCnf);
        }

        // POST: BookingCnfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,Movie,Id,Price")] BookingCnf bookingCnf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingCnf).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookingCnf);
        }

        // GET: BookingCnfs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingCnf bookingCnf = db.BookingCnfs.Find(id);
            if (bookingCnf == null)
            {
                return HttpNotFound();
            }
            return View(bookingCnf);
        }

        // POST: BookingCnfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingCnf bookingCnf = db.BookingCnfs.Find(id);
            db.BookingCnfs.Remove(bookingCnf);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Payment(int? id)
        {
           if(id ==null)
            {
                 
                return View();
            }
            var movieDetails = bb.MovieDetails.Where(x => x.ID == id).FirstOrDefault();
            Session["MName"] = movieDetails.Name;
            Session["Price"] = movieDetails.Price;
            string mNAme = Session["MName"].ToString();
            string uNAme = Session["Username"].ToString();

            if (db.BookingCnfs.Any(x => x.UserName == uNAme && x.Movie == mNAme))
            {
                //  RedirectToAction("Index", "MovieDetails");
                return RedirectToAction("PaymentCnf");
            }
            return RedirectToAction("PaymentA");


        }
        [HttpPost]
        public ActionResult Payment(string s)
        {
            return RedirectToAction("Create");


        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult PaymentCnf()
        {
 
            return View();
        }
        public ActionResult PaymentA(int? id)
        {
            if (id == null)
            {

                return View();
            }
            var movieDetails = bb.MovieDetails.Where(x => x.ID == id).FirstOrDefault();
            Session["MName"] = movieDetails.Name;
            Session["Price"] = movieDetails.Price;
          //1  BookingCnf bookingCnf=bb.BookingCnfs.Where()

            return View();
        }
        public ActionResult PaymentB()
        {

            return View();
        }
        [HttpPost]
        public ActionResult PaymentB(string s)
        {

            return RedirectToAction("Create");
        }
    }
}
