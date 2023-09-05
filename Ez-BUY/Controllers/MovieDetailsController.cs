using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineMovieV04.Models;

namespace OnlineMovieV04.Controllers
{
    [Authorize]
    public class MovieDetailsController : Controller
    {
        
        public MovieDetailsController()
        {
            
        }
        private OnlineMovieTicketEntities db = new OnlineMovieTicketEntities();
        
        // GET: MovieDetails[Authorize]
        public ActionResult Index()
        {
           

            return View(db.MovieDetails.ToList());
        }
        [Authorize]

        // GET: MovieDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetail movieDetail = db.MovieDetails.Find(id);
            if (movieDetail == null)
            {
                return HttpNotFound();
            }
            return View(movieDetail);
        }

        // GET: MovieDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,StartDate,EndDate,MovieCategory,Description,ImageUrl,Trailer")] MovieDetail movieDetail)
        {
            if (ModelState.IsValid)
            {
                if(movieDetail.StartDate<movieDetail.EndDate)
                {
                    db.MovieDetails.Add(movieDetail);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "End date is greater than start date!!!!!";
                    return View(movieDetail);
                }
               
            }

            return View(movieDetail);
        }

        // GET: MovieDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetail movieDetail = db.MovieDetails.Find(id);
            if (movieDetail == null)
            {
                return HttpNotFound();
            }
            return View(movieDetail);
        }

        // POST: MovieDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,StartDate,EndDate,MovieCategory,Description,ImageUrl,Trailer")] MovieDetail movieDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieDetail);
        }

        // GET: MovieDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDetail movieDetail = db.MovieDetails.Find(id);
            if (movieDetail == null)
            {
                return HttpNotFound();
            }
            return View(movieDetail);
        }

        // POST: MovieDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieDetail movieDetail = db.MovieDetails.Find(id);
            db.MovieDetails.Remove(movieDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
