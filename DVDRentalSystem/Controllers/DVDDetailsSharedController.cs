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
using System.IO;

namespace DVDRentalSystem.Controllers
{
    [Authorize(Roles = "Manager,Assistant")]
    public class DVDDetailsSharedController : Controller
    {
        private DVDRentalSystemContext db = new DVDRentalSystemContext();

        // GET: DVDDetailsShared
        public ActionResult Index()
        {
            return View(db.DVDDetails.ToList());
        }

        // GET: DVDDetailsShared/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DVDDetails dVDDetails = db.DVDDetails.Find(id);
            if (dVDDetails == null)
            {
                return HttpNotFound();
            }
            return View(dVDDetails);
        }

        // GET: DVDDetailsShared/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DVDDetailsShared/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DVDDetailsId,StockedDate,Name,Genre,AgeRestricted,NumberOfCopies,ReleaseDate,isDeleted,Length,CoverImage,CoverImagePath")] DVDDetails dVDDetails)
        {
            if (ModelState.IsValid)
            {

                string fileName = Path.GetFileNameWithoutExtension(dVDDetails.CoverImage.FileName);
                string extension = Path.GetExtension(dVDDetails.CoverImage.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                dVDDetails.CoverImagePath = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                dVDDetails.CoverImage.SaveAs(fileName);

                db.DVDDetails.Add(dVDDetails);
                db.SaveChanges();
                return RedirectToAction("Master", "DVDEnter");
            }

            return View(dVDDetails);
        }

        // GET: DVDDetailsShared/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DVDDetails dVDDetails = db.DVDDetails.Find(id);
            if (dVDDetails == null)
            {
                return HttpNotFound();
            }
            return View(dVDDetails);
        }

        // POST: DVDDetailsShared/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DVDDetailsId,StockedDate,Name,Genre,AgeRestricted,NumberOfCopies,ReleaseDate,isDeleted,Length,CoverImagePath")] DVDDetails dVDDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dVDDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dVDDetails);
        }

        // GET: DVDDetailsShared/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DVDDetails dVDDetails = db.DVDDetails.Find(id);
            if (dVDDetails == null)
            {
                return HttpNotFound();
            }
            return View(dVDDetails);
        }

        // POST: DVDDetailsShared/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DVDDetails dVDDetails = db.DVDDetails.Find(id);
            db.DVDDetails.Remove(dVDDetails);
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
