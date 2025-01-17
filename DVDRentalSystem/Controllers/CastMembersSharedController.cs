﻿using System;
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
    public class CastMembersSharedController : Controller
    {
        private DVDRentalSystemContext db = new DVDRentalSystemContext();

        // GET: CastMembersShared
        public ActionResult Index()
        {
            var castMembers = db.CastMembers.Include(c => c.CastDetails).Include(c => c.DVDDetails);
            return View(castMembers.ToList());
        }

        // GET: CastMembersShared/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CastMembers castMembers = db.CastMembers.Find(id);
            if (castMembers == null)
            {
                return HttpNotFound();
            }
            return View(castMembers);
        }

        // GET: CastMembersShared/Create
        public ActionResult Create()
        {
            ViewBag.CastDetailsId = new SelectList(db.CastDetails, "CastDetailsId", "FirstName");
            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails, "DVDDetailsId", "Name");
            return View();
        }

        // POST: CastMembersShared/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CastMemberId,CastDetailsId,DVDDetailsId")] CastMembers castMembers)
        {
            if (ModelState.IsValid)
            {
                db.CastMembers.Add(castMembers);
                db.SaveChanges();
                return RedirectToAction("Master", "DVDEnter");
            }

            ViewBag.CastDetailsId = new SelectList(db.CastDetails, "CastDetailsId", "FirstName", castMembers.CastDetailsId);
            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails, "DVDDetailsId", "Name", castMembers.DVDDetailsId);
            return View(castMembers);
        }

        // GET: CastMembersShared/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CastMembers castMembers = db.CastMembers.Find(id);
            if (castMembers == null)
            {
                return HttpNotFound();
            }
            ViewBag.CastDetailsId = new SelectList(db.CastDetails, "CastDetailsId", "FirstName", castMembers.CastDetailsId);
            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails, "DVDDetailsId", "Name", castMembers.DVDDetailsId);
            return View(castMembers);
        }

        // POST: CastMembersShared/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CastMemberId,CastDetailsId,DVDDetailsId")] CastMembers castMembers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(castMembers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CastDetailsId = new SelectList(db.CastDetails, "CastDetailsId", "FirstName", castMembers.CastDetailsId);
            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails, "DVDDetailsId", "Name", castMembers.DVDDetailsId);
            return View(castMembers);
        }

        // GET: CastMembersShared/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CastMembers castMembers = db.CastMembers.Find(id);
            if (castMembers == null)
            {
                return HttpNotFound();
            }
            return View(castMembers);
        }

        // POST: CastMembersShared/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CastMembers castMembers = db.CastMembers.Find(id);
            db.CastMembers.Remove(castMembers);
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
