using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCMusicStore.Models;

namespace MVCMusicStore.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class StoreManagerController : Controller
    {
        private MusicStoreEntities db = new MusicStoreEntities();

		// GET: StoreManager
		// The index view retrieves a list of Albums, including each album’s referenced Genre and Artist information
		public ActionResult Index()
        {
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre);
            return View(albums.ToList());
        }

		// GET: StoreManager/Details/5
		// The StoreManager Controller’s Details controller action works exactly the same as the Store Controller Details
		// action we wrote previously
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: StoreManager/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            return View();
        }

        // POST: StoreManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // POST: StoreManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
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
