﻿using ca2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

<<<<<<< HEAD
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Net;
using System.Net.Http;

=======
>>>>>>> 30c066fafa3b636dade4ba731a5ee39abf35641d
namespace ca2.Controllers
{
    public class HomeController : Controller
    {
        MovieDb db = new MovieDb();

        //
        // GET: /Home/

<<<<<<< HEAD
        public ActionResult Index(string sortOrder)
        {
            if (sortOrder == null) 
                sortOrder = "ascNumber";
            ViewBag.numberOrder = (sortOrder == "ascNumber") ? "descNumber" : "ascNumber";
            ViewBag.dateOrder = (sortOrder == "ascDate") ? "descDate" : "ascDate";

            IQueryable<Movie> movies = db.Movies;
            switch (sortOrder)
            {
                case "descDate":
                    ViewBag.dateOrder = "ascDate";
                    movies = movies.OrderByDescending(m => m.ReleaseDate);
                    break;
                case "descNumber":
                    ViewBag.numberOrder = "ascNumber";
                    movies = movies.OrderByDescending(m => m.Actors.Count);
                    break;
                case "ascDate":
                    ViewBag.dateOrder = "descDate";
                    movies = movies.OrderBy(m => m.ReleaseDate);
                    break;
                case "ascNumber":
                    ViewBag.numberOrder = "descNumber";
                    movies = movies.OrderBy(m => m.Actors.Count);
                    break;
                default:
                    ViewBag.numberOrder = "ascNumber";
                    movies = movies.OrderBy(m => m.Actors.Count);
                    break;
            }
            return View(movies.ToList());
=======
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
>>>>>>> 30c066fafa3b636dade4ba731a5ee39abf35641d
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            var m = db.Movies.Find(id);
            if (m != null)//if movie is found
            {
                ViewBag.SexStatsMale = m.Actors.Count(act => act.Sex == Sex.Male);
                ViewBag.SexStatsFemale = m.Actors.Count(act => act.Sex == Sex.Female);
                return View(m);
            }
            else//not found
                return View();
        }

        public PartialViewResult ActorsById(int id)
        {
            var m = db.Movies.Find(id);
            @ViewBag.movieId = id;
            @ViewBag.movieTitle = m.Title;
            return PartialView("_ActorsInMovie", m.Actors);
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            ViewBag.directors = db.Directors.ToList();
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(Movie incomingMovie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Movies.Add(incomingMovie);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public PartialViewResult CreateActor(Actor incomingActor)
        {
            if (ModelState.IsValid)
            {
                db.Actors.Add(incomingActor);
                db.SaveChanges();
                return ActorsById(incomingActor.selectedMovieId);
            }
            return null;
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            var mov = db.Movies.Find(id);
            if (mov != null)
            {
                ViewBag.directors = db.Directors.ToList();
                return View(mov);
            }
            else
                return View();
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
<<<<<<< HEAD
        public ActionResult Edit(Movie movieToEdit)
        {
            try
            {
                //db.Entry(movieToEdit).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
=======
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

>>>>>>> 30c066fafa3b636dade4ba731a5ee39abf35641d
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id)
        {
<<<<<<< HEAD
            return View(db.Movies.Find(id));
=======
            return View();
>>>>>>> 30c066fafa3b636dade4ba731a5ee39abf35641d
        }

        //
        // POST: /Home/Delete/5

<<<<<<< HEAD
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Movies.Remove(db.Movies.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
=======
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
>>>>>>> 30c066fafa3b636dade4ba731a5ee39abf35641d
        }
    }
}
