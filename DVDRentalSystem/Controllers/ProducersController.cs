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
    [Authorize(Roles = "Manager,Assistant")]
    public class ProducersController : Controller
    {
        private DVDRentalSystemContext db = new DVDRentalSystemContext();

        // GET: Producers
        public ActionResult Index()
        {
            var producers = db.Producers.Include(p => p.CastDetails).Include(p => p.DVDDetails);
            return View(producers.ToList());
        }

        // GET: Producers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer producer = db.Producers.Find(id);
            if (producer == null)
            {
                return HttpNotFound();
            }
            return View(producer);
        }

        // GET: Producers/Create
        public ActionResult Create()
        {
            ViewBag.CastDetailsId = new SelectList(db.CastDetails, "CastDetailsId", "FirstName");
            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails, "DVDDetailsId", "Name");
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProducerId,FirstName,LastName,Address,Studio_name,Email,Gender,DVDDetailsId,CastDetailsId")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                db.Producers.Add(producer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CastDetailsId = new SelectList(db.CastDetails, "CastDetailsId", "FirstName", producer.CastDetailsId);
            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails, "DVDDetailsId", "Name", producer.DVDDetailsId);
            return View(producer);
        }

        // GET: Producers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer producer = db.Producers.Find(id);
            if (producer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CastDetailsId = new SelectList(db.CastDetails, "CastDetailsId", "FirstName", producer.CastDetailsId);
            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails, "DVDDetailsId", "Name", producer.DVDDetailsId);
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProducerId,FirstName,LastName,Address,Studio_name,Email,Gender,DVDDetailsId,CastDetailsId")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CastDetailsId = new SelectList(db.CastDetails, "CastDetailsId", "FirstName", producer.CastDetailsId);
            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails, "DVDDetailsId", "Name", producer.DVDDetailsId);
            return View(producer);
        }

        // GET: Producers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer producer = db.Producers.Find(id);
            if (producer == null)
            {
                return HttpNotFound();
            }
            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producer producer = db.Producers.Find(id);
            db.Producers.Remove(producer);
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
