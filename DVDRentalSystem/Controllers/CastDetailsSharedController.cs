using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DVDRentalSystem.Models;
using DataContext.Data;

namespace DVDRentalSystem.Controllers
{
    public class CastDetailsSharedController : Controller
    {
        private DVDRentalSystemContext db = new DVDRentalSystemContext();

        // GET: CastDetailsShared
        public ActionResult Index()
        {
            return View(db.CastDetails.ToList());
        }

        // GET: CastDetailsShared/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CastDetails castDetails = db.CastDetails.Find(id);
            if (castDetails == null)
            {
                return HttpNotFound();
            }
            return View(castDetails);
        }

        // GET: CastDetailsShared/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CastDetailsShared/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CastDetailsId,FirstName,LastName,Phone,Email,BirthDate,Gender")] CastDetails castDetails)
        {
            if (ModelState.IsValid)
            {
                db.CastDetails.Add(castDetails);
                db.SaveChanges();
                return RedirectToAction("Master", "DVDEnter");
            }

            return View(castDetails);
        }

        // GET: CastDetailsShared/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CastDetails castDetails = db.CastDetails.Find(id);
            if (castDetails == null)
            {
                return HttpNotFound();
            }
            return View(castDetails);
        }

        // POST: CastDetailsShared/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CastDetailsId,FirstName,LastName,Phone,Email,BirthDate,Gender")] CastDetails castDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(castDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(castDetails);
        }

        // GET: CastDetailsShared/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CastDetails castDetails = db.CastDetails.Find(id);
            if (castDetails == null)
            {
                return HttpNotFound();
            }
            return View(castDetails);
        }

        // POST: CastDetailsShared/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CastDetails castDetails = db.CastDetails.Find(id);
            db.CastDetails.Remove(castDetails);
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
