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
using System.Diagnostics;

namespace DVDRentalSystem.Controllers
{
    [Authorize(Roles = "Manager,Assistant")]
    public class LoansController : Controller
    {
        private DVDRentalSystemContext db = new DVDRentalSystemContext();

        // GET: Loans
        public ActionResult Index()
        {
            var loans = db.Loans.Include(l => l.DVDDetails).Include(l => l.LoanType).Include(l => l.Members);
            return View(loans.ToList());
        }

        // GET: Loans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        public ActionResult Create()
        {
            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails.Where(x =>x.isDeleted == false), "DVDDetailsId", "Name");
            ViewBag.LoanTypeId = new SelectList(db.LoanTypes, "LoanTypeId", "LoanTypeName");
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoanId,DVDDetailsId,IssueDate,ReturnDate,LoanTypeId,LoanCharge,FineAmount,MemberId,CopyId,ActualReturnedDate")] Loan loan)
        {



            if (ModelState.IsValid)
            {
            


                //Code for Category Limit
                int memberCategoryId = 0;
                var userCategoryList = db.Members.Include("MemberCategory")
                    .Where(x => x.MemberId == loan.MemberId)
                    .Select(x => x.MemberCategoryId).ToList();
                foreach (var val in userCategoryList)
                {
                    memberCategoryId = val;
                }


                var loanedCopiesCount = int.Parse(db.Loans.Include("Members")
                            .Where(x => x.Members.MemberId == loan.MemberId)
                            .Where(x => x.ActualReturnedDate == null).Count().ToString());
                var categoryLimitList = db.Members.Include("MemberCategory")
                    .Where(x => x.MemberCategory.MemberCategoryId == memberCategoryId)
                    .Select(x => x.MemberCategory.TotalLoan).ToList();
                int totalLimit = 0;
                
                foreach (var val in categoryLimitList)
                {
                    totalLimit = int.Parse(val.ToString());
                }


                if (loanedCopiesCount >= totalLimit)
                {
                    ViewBag.Message = String.Format("Member not eligible to loan more DVD");
                    ViewBag.DVDDetailsId = new SelectList(db.DVDDetails.Where(x => x.isDeleted == false), "DVDDetailsId", "Name");
                    ViewBag.LoanTypeId = new SelectList(db.LoanTypes, "LoanTypeId", "LoanTypeName", loan.LoanTypeId);
                    ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName", loan.MemberId);
                    return View(loan);
                }

                else
                {


                    //AGE RESTRICTION CODES

                    bool ageRestrication = false;
                    var movieAgeRestrictionList = db.Loans.Include("DVDDetails").Where(x => x.DVDDetails.DVDDetailsId == loan.DVDDetailsId).Select(x => x.DVDDetails.AgeRestricted).ToList();
                    foreach (var val in movieAgeRestrictionList)
                    {
                        ageRestrication = val;
                    }


                    if (ageRestrication)
                    {
                        var dobValue = "";

                        var userDateofBirthList = db.Loans.Include("Members").Where(x => x.Members.MemberId == loan.MemberId).Select(x => x.Members.DateOfBirth).ToList();


                        foreach (var val in userDateofBirthList)
                        {
                            dobValue = val.ToString();
                        }

                        DateTime dob = Convert.ToDateTime(dobValue);
                        DateTime currentDate = DateTime.Now;
                        double days = (Convert.ToDateTime(currentDate) - Convert.ToDateTime(dob)).TotalDays;
                        if (days >= 6570)
                        {
                            Debug.WriteLine("Age restricted and user can buy");
                            db.Loans.Add(loan);
                            db.SaveChanges();



                            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails.Where(x => x.isDeleted == false), "DVDDetailsId", "Name");
                            ViewBag.LoanTypeId = new SelectList(db.LoanTypes, "LoanTypeId", "LoanTypeName", loan.LoanTypeId);
                            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName", loan.MemberId);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Debug.WriteLine("Age restricted and user not allowded");
                            ViewBag.Message = String.Format("Age restriction: User not above 18");



                            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails.Where(x => x.isDeleted == false), "DVDDetailsId", "Name");
                            ViewBag.LoanTypeId = new SelectList(db.LoanTypes, "LoanTypeId", "LoanTypeName", loan.LoanTypeId);
                            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName", loan.MemberId);
                            return View(loan);


                        }

                    }
                    else
                    {
                        db.Loans.Add(loan);
                        db.SaveChanges();
                        Debug.WriteLine("Not Age Restricted");



                        ViewBag.DVDDetailsId = new SelectList(db.DVDDetails, "DVDDetailsId", "Name", loan.DVDDetailsId);
                        ViewBag.LoanTypeId = new SelectList(db.LoanTypes, "LoanTypeId", "LoanTypeName", loan.LoanTypeId);
                        ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName", loan.MemberId);
                        return RedirectToAction("Index");

                    }
                }
            }

            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails.Where(x => x.isDeleted == false), "DVDDetailsId", "Name");
            ViewBag.LoanTypeId = new SelectList(db.LoanTypes, "LoanTypeId", "LoanTypeName", loan.LoanTypeId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName", loan.MemberId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails.Where(x => x.isDeleted == false), "DVDDetailsId", "Name");
            ViewBag.LoanTypeId = new SelectList(db.LoanTypes, "LoanTypeId", "LoanTypeName", loan.LoanTypeId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName", loan.MemberId);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoanId,DVDDetailsId,IssueDate,ReturnDate,LoanTypeId,LoanCharge,FineAmount,MemberId,CopyId,ActualReturnedDate")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DVDDetailsId = new SelectList(db.DVDDetails.Where(x => x.isDeleted == false), "DVDDetailsId", "Name");
            ViewBag.LoanTypeId = new SelectList(db.LoanTypes, "LoanTypeId", "LoanTypeName", loan.LoanTypeId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName", loan.MemberId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
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
